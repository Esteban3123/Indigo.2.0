Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEmployeeSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmployeeSchedule))
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbProcesar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcInformation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.ColCedula = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEmpleado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFechaInicio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFechaFin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColObservaciones = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepStatus = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCdnMonthYear = New Presentation.Controls.CtrDateNavigator()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCriteria = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciYearMonth = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl11 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate11 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IcEstado = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDSlFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlPosition = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlEmployee = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SearchLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYearMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IcEstado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlPosition.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlEmployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
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
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSlEmployee)
        Me.LayoutControl1.Controls.Add(Me.INDSlPosition)
        Me.LayoutControl1.Controls.Add(Me.INDSlFunctionalUnit)
        Me.LayoutControl1.Controls.Add(Me.INDSbProcesar)
        Me.LayoutControl1.Controls.Add(Me.INDgcInformation)
        Me.LayoutControl1.Controls.Add(Me.INDCdnMonthYear)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(435, 231, 421, 552)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSbProcesar
        '
        Me.INDSbProcesar.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDSbProcesar.Appearance.Options.UseFont = True
        Me.INDSbProcesar.Location = New System.Drawing.Point(24, 257)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbProcesar, False)
        Me.INDSbProcesar.Name = "INDSbProcesar"
        Me.INDSbProcesar.Size = New System.Drawing.Size(386, 32)
        Me.INDSbProcesar.StyleController = Me.LayoutControl1
        Me.INDSbProcesar.TabIndex = 23
        Me.INDSbProcesar.Text = "Procesar"
        '
        'INDgcInformation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInformation, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInformation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInformation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInformation, False)
        Me.INDgcInformation.Location = New System.Drawing.Point(438, 59)
        Me.INDgcInformation.MainView = Me.INDviewInfo
        Me.INDgcInformation.Name = "INDgcInformation"
        Me.INDgcInformation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepStatus, Me.RepositoryItemImageComboBox1})
        Me.INDgcInformation.Size = New System.Drawing.Size(996, 470)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInformation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcInformation.TabIndex = 4
        Me.INDgcInformation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewInfo})
        '
        'INDviewInfo
        '
        Me.INDviewInfo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewInfo.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewInfo.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInfo.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewInfo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInfo.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewInfo.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewInfo.Appearance.Row.Options.UseFont = True
        Me.INDviewInfo.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewInfo.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEstado, Me.ColCedula, Me.ColEmpleado, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.ColFechaInicio, Me.GridColumn5, Me.ColFechaFin, Me.GridColumn6, Me.ColObservaciones})
        Me.INDviewInfo.GridControl = Me.INDgcInformation
        Me.INDviewInfo.Name = "INDviewInfo"
        Me.INDviewInfo.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewInfo.OptionsBehavior.Editable = False
        Me.INDviewInfo.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewInfo.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewInfo.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.INDviewInfo.OptionsView.ShowAutoFilterRow = True
        Me.INDviewInfo.OptionsView.ShowDetailButtons = False
        Me.INDviewInfo.OptionsView.ShowFooter = True
        Me.INDviewInfo.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInfo, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDviewInfo, False)
        '
        'ColEstado
        '
        Me.ColEstado.Caption = "Estado"
        Me.ColEstado.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.ColEstado.FieldName = "Warning"
        Me.ColEstado.Name = "ColEstado"
        Me.ColEstado.OptionsColumn.AllowEdit = False
        Me.ColEstado.Visible = True
        Me.ColEstado.VisibleIndex = 0
        Me.ColEstado.Width = 35
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)})
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Grave", CType(2, Byte), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Advertencia", CType(1, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Informacion", CType(0, Byte), 2)})
        Me.RepositoryItemImageComboBox1.LargeImages = Me.ImageCollection1
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.InsertImage(Global.Presentation.Payroll.My.Resources.Resources.amarillo_16x16, "amarillo_16x16", GetType(Global.Presentation.Payroll.My.Resources.Resources), 0)
        Me.ImageCollection1.Images.SetKeyName(0, "amarillo_16x16")
        Me.ImageCollection1.InsertImage(Global.Presentation.Payroll.My.Resources.Resources.rojo_16x16, "rojo_16x16", GetType(Global.Presentation.Payroll.My.Resources.Resources), 1)
        Me.ImageCollection1.Images.SetKeyName(1, "rojo_16x16")
        Me.ImageCollection1.InsertImage(Global.Presentation.Payroll.My.Resources.Resources.verde_16x16, "verde_16x16", GetType(Global.Presentation.Payroll.My.Resources.Resources), 2)
        Me.ImageCollection1.Images.SetKeyName(2, "verde_16x16")
        '
        'ColCedula
        '
        Me.ColCedula.Caption = "Cédula"
        Me.ColCedula.FieldName = "Cedula"
        Me.ColCedula.Name = "ColCedula"
        Me.ColCedula.OptionsColumn.AllowEdit = False
        Me.ColCedula.Visible = True
        Me.ColCedula.VisibleIndex = 1
        Me.ColCedula.Width = 80
        '
        'ColEmpleado
        '
        Me.ColEmpleado.Caption = "Empleado"
        Me.ColEmpleado.FieldName = "Empleado"
        Me.ColEmpleado.Name = "ColEmpleado"
        Me.ColEmpleado.OptionsColumn.AllowEdit = False
        Me.ColEmpleado.Visible = True
        Me.ColEmpleado.VisibleIndex = 2
        Me.ColEmpleado.Width = 100
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Codigo Centro Costo"
        Me.GridColumn1.FieldName = "CodeCostCenter"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 3
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre Centro Costo"
        Me.GridColumn2.FieldName = "NameCostCenter"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 4
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código Unidad Funcional"
        Me.GridColumn3.FieldName = "CodeFunctionalUnit"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 5
        Me.GridColumn3.Width = 102
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre Unidad Funcional"
        Me.GridColumn4.FieldName = "NameFunctionalUnit"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 6
        '
        'ColFechaInicio
        '
        Me.ColFechaInicio.Caption = "Fecha Inicio"
        Me.ColFechaInicio.DisplayFormat.FormatString = "d"
        Me.ColFechaInicio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFechaInicio.FieldName = "InitialDate"
        Me.ColFechaInicio.Name = "ColFechaInicio"
        Me.ColFechaInicio.OptionsColumn.AllowEdit = False
        Me.ColFechaInicio.Visible = True
        Me.ColFechaInicio.VisibleIndex = 7
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Hora Inicio"
        Me.GridColumn5.FieldName = "InitialTime"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 8
        '
        'ColFechaFin
        '
        Me.ColFechaFin.Caption = "Fecha Fin"
        Me.ColFechaFin.DisplayFormat.FormatString = "d"
        Me.ColFechaFin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFechaFin.FieldName = "EndDate"
        Me.ColFechaFin.Name = "ColFechaFin"
        Me.ColFechaFin.OptionsColumn.AllowEdit = False
        Me.ColFechaFin.Visible = True
        Me.ColFechaFin.VisibleIndex = 9
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Hora Fin"
        Me.GridColumn6.FieldName = "EndTime"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 10
        '
        'ColObservaciones
        '
        Me.ColObservaciones.Caption = "Observaciones"
        Me.ColObservaciones.FieldName = "Observation"
        Me.ColObservaciones.Name = "ColObservaciones"
        Me.ColObservaciones.OptionsColumn.AllowEdit = False
        Me.ColObservaciones.Visible = True
        Me.ColObservaciones.VisibleIndex = 11
        Me.ColObservaciones.Width = 136
        '
        'INDRepStatus
        '
        Me.INDRepStatus.AutoHeight = False
        Me.INDRepStatus.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ok", 0, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Advertencia", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Error", 2, -1)})
        Me.INDRepStatus.Name = "INDRepStatus"
        '
        'INDCdnMonthYear
        '
        Me.INDCdnMonthYear.CtrCalendar = Nothing
        Me.INDCdnMonthYear.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.INDCdnMonthYear.Location = New System.Drawing.Point(24, 59)
        Me.INDCdnMonthYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDCdnMonthYear.Name = "INDCdnMonthYear"
        Me.INDCdnMonthYear.Size = New System.Drawing.Size(386, 66)
        Me.INDCdnMonthYear.TabIndex = 1
        Me.INDCdnMonthYear.WithEvent = True
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.INDLcgCriteria})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1458, 553)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1024, 533)
        Me.LayoutControlGroup2.Text = "Análisis de Turnos de Empleados"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcInformation
        Me.LayoutControlItem1.CustomizationFormText = "Listado de Empleados"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(1000, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(1000, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1000, 474)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDLcgCriteria
        '
        Me.INDLcgCriteria.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCriteria, False)
        Me.INDLcgCriteria.CustomizationFormText = "Criterios"
        Me.INDLcgCriteria.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciYearMonth, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5})
        Me.INDLcgCriteria.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCriteria.Name = "INDLcgCriteria"
        Me.INDLcgCriteria.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgCriteria.Text = "Criterios"
        '
        'INDLciYearMonth
        '
        Me.INDLciYearMonth.Control = Me.INDCdnMonthYear
        Me.INDLciYearMonth.CustomizationFormText = "INDLciYearMonth"
        Me.INDLciYearMonth.Location = New System.Drawing.Point(0, 0)
        Me.INDLciYearMonth.MaxSize = New System.Drawing.Size(390, 70)
        Me.INDLciYearMonth.MinSize = New System.Drawing.Size(390, 70)
        Me.INDLciYearMonth.Name = "INDLciYearMonth"
        Me.INDLciYearMonth.Size = New System.Drawing.Size(390, 70)
        Me.INDLciYearMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYearMonth.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciYearMonth.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbProcesar
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 198)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(64, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 234)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(390, 240)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 178)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(0, 20)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(104, 20)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(390, 20)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        '
        'IcEstado
        '
        Me.IcEstado.ImageStream = CType(resources.GetObject("IcEstado.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.IcEstado.Images.SetKeyName(0, "apply_16x16.png")
        Me.IcEstado.Images.SetKeyName(1, "cancel_16x16.png")
        Me.IcEstado.Images.SetKeyName(2, "warning_16x16.png")
        '
        'INDSlFunctionalUnit
        '
        Me.INDSlFunctionalUnit.Location = New System.Drawing.Point(186, 129)
        Me.INDSlFunctionalUnit.Name = "INDSlFunctionalUnit"
        Me.INDSlFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDSlFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSlFunctionalUnit.Properties.DisplayMember = "Descripcion"
        Me.INDSlFunctionalUnit.Properties.NullText = "Todas"
        Me.INDSlFunctionalUnit.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSlFunctionalUnit.Properties.ValueMember = "Id"
        Me.INDSlFunctionalUnit.Size = New System.Drawing.Size(224, 28)
        Me.INDSlFunctionalUnit.StyleController = Me.LayoutControl1
        Me.INDSlFunctionalUnit.TabIndex = 24
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDSlFunctionalUnit
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 70)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Unidad Funcional"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(150, 21)
        Me.LayoutControlItem3.TextToControlDistance = 12
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDSlPosition
        '
        Me.INDSlPosition.Location = New System.Drawing.Point(186, 165)
        Me.INDSlPosition.Name = "INDSlPosition"
        Me.INDSlPosition.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlPosition.Properties.Appearance.Options.UseFont = True
        Me.INDSlPosition.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSlPosition.Properties.NullText = "Todos"
        Me.INDSlPosition.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDSlPosition.Size = New System.Drawing.Size(224, 28)
        Me.INDSlPosition.StyleController = Me.LayoutControl1
        Me.INDSlPosition.TabIndex = 25
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDSlPosition
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 106)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Cargo"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(150, 21)
        Me.LayoutControlItem4.TextToControlDistance = 12
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'INDSlEmployee
        '
        Me.INDSlEmployee.Location = New System.Drawing.Point(186, 201)
        Me.INDSlEmployee.Name = "INDSlEmployee"
        Me.INDSlEmployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlEmployee.Properties.Appearance.Options.UseFont = True
        Me.INDSlEmployee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSlEmployee.Properties.NullText = "Todos"
        Me.INDSlEmployee.Properties.PopupView = Me.SearchLookUpEdit3View
        Me.INDSlEmployee.Size = New System.Drawing.Size(224, 28)
        Me.INDSlEmployee.StyleController = Me.LayoutControl1
        Me.INDSlEmployee.TabIndex = 26
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDSlEmployee
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 142)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Empleado"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(150, 21)
        Me.LayoutControlItem5.TextToControlDistance = 12
        '
        'SearchLookUpEdit3View
        '
        Me.SearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit3View.Name = "SearchLookUpEdit3View"
        Me.SearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit3View, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit3View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Codigo"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 70
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Descripción"
        Me.GridColumn8.FieldName = "Descripcion"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 314
        '
        'FrmEmployeeSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmEmployeeSchedule"
        Me.Opacity = 1.0R
        Me.Tag = "2141"
        Me.Text = "Análisis de turnos de empleados"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYearMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IcEstado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlPosition.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlEmployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcInformation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents ColEmplado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents INDCdnMonthYear As CtrDateNavigator
    Friend WithEvents IndigoGridLookUpControl11 As IndigoGridLookUpControl
    Friend WithEvents IndigoDate11 As IndigoDate
    Friend WithEvents INDLcgCriteria As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciYearMonth As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbProcesar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCedula As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEmpleado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFechaInicio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFechaFin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColObservaciones As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IcEstado As DevExpress.Utils.ImageCollection
    Friend WithEvents INDRepStatus As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSlEmployee As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlPosition As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
End Class
