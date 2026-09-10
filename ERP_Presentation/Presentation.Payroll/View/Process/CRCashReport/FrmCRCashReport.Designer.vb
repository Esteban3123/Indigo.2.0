Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCRCashReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCRCashReport))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcAutoliquidation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcExportExcell = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColumnDocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnOccupation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnCCSSCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnChangeType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnHoursDaily = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnDayClass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnChangeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnInitialDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnFinalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlPeriodDate = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlWorkCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcListEmployee = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColIdentificationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCedula = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDEmployeeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColOccupation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIngress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInsuranceClass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColChangeType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWorkHours = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWorkClass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColChangeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnProccess = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSlCompany = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnExport = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcExcel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciImportButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton2 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSimpleButton3 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcAutoliquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcAutoliquidation.SuspendLayout()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlPeriodDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlWorkCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcListEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciImportButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcAutoliquidation)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1296, 556)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1296, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1296, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLcAutoliquidation
        '
        Me.INDLcAutoliquidation.Controls.Add(Me.INDBtnImportFile)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDGcExportExcell)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDSlPeriodDate)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDSlWorkCenter)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDGcListEmployee)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDBtnProccess)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDSlCompany)
        Me.INDLcAutoliquidation.Controls.Add(Me.INDBtnExport)
        Me.INDLcAutoliquidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcAutoliquidation.Location = New System.Drawing.Point(202, 6)
        Me.INDLcAutoliquidation.Name = "INDLcAutoliquidation"
        Me.INDLcAutoliquidation.Root = Me.LayoutControlGroup1
        Me.INDLcAutoliquidation.Size = New System.Drawing.Size(1092, 548)
        Me.INDLcAutoliquidation.TabIndex = 0
        Me.INDLcAutoliquidation.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(483, 56)
        Me.IndigoSimpleButton3.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.IndigoSimpleButton2.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(41, 33)
        Me.INDBtnImportFile.StyleController = Me.INDLcAutoliquidation
        Me.INDBtnImportFile.TabIndex = 23
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDGcExportExcell
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExportExcell, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExportExcell, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExportExcell, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExportExcell, False)
        Me.INDGcExportExcell.Location = New System.Drawing.Point(24, 269)
        Me.INDGcExportExcell.MainView = Me.GridView4
        Me.INDGcExportExcell.Name = "INDGcExportExcell"
        Me.INDGcExportExcell.Size = New System.Drawing.Size(386, 238)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExportExcell, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcExportExcell.TabIndex = 18
        Me.INDGcExportExcell.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        Me.GridView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView4.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.GroupRow.Options.UseFont = True
        Me.GridView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView4.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColumnDocumentType, Me.INDColumnNit, Me.INDColumnName, Me.INDColumnOccupation, Me.INDColumnSalary, Me.INDColumnCCSSCode, Me.INDColumnChangeType, Me.INDColumnHoursDaily, Me.INDColumnDayClass, Me.INDColumnChangeCode, Me.INDColumnInitialDate, Me.INDColumnFinalDate})
        Me.GridView4.GridControl = Me.INDGcExportExcell
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView4, False)
        '
        'INDColumnDocumentType
        '
        Me.INDColumnDocumentType.Caption = "Tipo Documento"
        Me.INDColumnDocumentType.FieldName = "IdentificationType"
        Me.INDColumnDocumentType.Name = "INDColumnDocumentType"
        Me.INDColumnDocumentType.Visible = True
        Me.INDColumnDocumentType.VisibleIndex = 0
        '
        'INDColumnNit
        '
        Me.INDColumnNit.Caption = "Numero Identificación"
        Me.INDColumnNit.DisplayFormat.FormatString = "G"
        Me.INDColumnNit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColumnNit.FieldName = "Nit"
        Me.INDColumnNit.Name = "INDColumnNit"
        Me.INDColumnNit.Visible = True
        Me.INDColumnNit.VisibleIndex = 1
        '
        'INDColumnName
        '
        Me.INDColumnName.Caption = "Empleado"
        Me.INDColumnName.FieldName = "NameEmployee"
        Me.INDColumnName.Name = "INDColumnName"
        Me.INDColumnName.Visible = True
        Me.INDColumnName.VisibleIndex = 2
        '
        'INDColumnOccupation
        '
        Me.INDColumnOccupation.Caption = "Ocupacion"
        Me.INDColumnOccupation.FieldName = "CCSSCode"
        Me.INDColumnOccupation.Name = "INDColumnOccupation"
        Me.INDColumnOccupation.Visible = True
        Me.INDColumnOccupation.VisibleIndex = 3
        Me.INDColumnOccupation.Width = 50
        '
        'INDColumnSalary
        '
        Me.INDColumnSalary.Caption = "Salario Devengado"
        Me.INDColumnSalary.FieldName = "IBCHealth"
        Me.INDColumnSalary.Name = "INDColumnSalary"
        Me.INDColumnSalary.Visible = True
        Me.INDColumnSalary.VisibleIndex = 4
        '
        'INDColumnCCSSCode
        '
        Me.INDColumnCCSSCode.Caption = "Clase de seguro"
        Me.INDColumnCCSSCode.FieldName = "CCSSCode"
        Me.INDColumnCCSSCode.Name = "INDColumnCCSSCode"
        Me.INDColumnCCSSCode.Visible = True
        Me.INDColumnCCSSCode.VisibleIndex = 5
        Me.INDColumnCCSSCode.Width = 50
        '
        'INDColumnChangeType
        '
        Me.INDColumnChangeType.Caption = "Tipo de Cambio"
        Me.INDColumnChangeType.FieldName = "ChangeType"
        Me.INDColumnChangeType.Name = "INDColumnChangeType"
        Me.INDColumnChangeType.Visible = True
        Me.INDColumnChangeType.VisibleIndex = 6
        Me.INDColumnChangeType.Width = 50
        '
        'INDColumnHoursDaily
        '
        Me.INDColumnHoursDaily.Caption = "Horas de Jornada"
        Me.INDColumnHoursDaily.FieldName = "HoursDaily"
        Me.INDColumnHoursDaily.Name = "INDColumnHoursDaily"
        Me.INDColumnHoursDaily.Visible = True
        Me.INDColumnHoursDaily.VisibleIndex = 7
        Me.INDColumnHoursDaily.Width = 50
        '
        'INDColumnDayClass
        '
        Me.INDColumnDayClass.Caption = "Clase de Jornada"
        Me.INDColumnDayClass.Name = "INDColumnDayClass"
        Me.INDColumnDayClass.Visible = True
        Me.INDColumnDayClass.VisibleIndex = 8
        Me.INDColumnDayClass.Width = 50
        '
        'INDColumnChangeCode
        '
        Me.INDColumnChangeCode.Caption = "Codigo de Cambio"
        Me.INDColumnChangeCode.FieldName = "ChangeCode"
        Me.INDColumnChangeCode.Name = "INDColumnChangeCode"
        Me.INDColumnChangeCode.Visible = True
        Me.INDColumnChangeCode.VisibleIndex = 9
        Me.INDColumnChangeCode.Width = 50
        '
        'INDColumnInitialDate
        '
        Me.INDColumnInitialDate.Caption = "Fecha de Inicio"
        Me.INDColumnInitialDate.FieldName = "InitialDate"
        Me.INDColumnInitialDate.Name = "INDColumnInitialDate"
        Me.INDColumnInitialDate.Visible = True
        Me.INDColumnInitialDate.VisibleIndex = 10
        '
        'INDColumnFinalDate
        '
        Me.INDColumnFinalDate.Caption = "Fecha Final"
        Me.INDColumnFinalDate.DisplayFormat.FormatString = "yyyy-MM-dd"
        Me.INDColumnFinalDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColumnFinalDate.FieldName = "FinalDate"
        Me.INDColumnFinalDate.Name = "INDColumnFinalDate"
        Me.INDColumnFinalDate.Visible = True
        Me.INDColumnFinalDate.VisibleIndex = 11
        '
        'INDSlPeriodDate
        '
        Me.INDSlPeriodDate.Location = New System.Drawing.Point(24, 193)
        Me.INDSlPeriodDate.Name = "INDSlPeriodDate"
        Me.INDSlPeriodDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlPeriodDate.Properties.Appearance.Options.UseFont = True
        Me.INDSlPeriodDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlPeriodDate.Properties.DisplayMember = "PayrollDateLiquidated"
        Me.INDSlPeriodDate.Properties.NullText = ""
        Me.INDSlPeriodDate.Properties.PopupView = Me.GridView3
        Me.INDSlPeriodDate.Properties.ValueMember = "PayrollDateLiquidated"
        Me.INDSlPeriodDate.Size = New System.Drawing.Size(386, 28)
        Me.INDSlPeriodDate.StyleController = Me.INDLcAutoliquidation
        Me.INDSlPeriodDate.TabIndex = 9
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolDate})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'INDcolDate
        '
        Me.INDcolDate.Caption = "Fecha"
        Me.INDcolDate.Name = "INDcolDate"
        Me.INDcolDate.OptionsColumn.AllowEdit = False
        Me.INDcolDate.Visible = True
        Me.INDcolDate.VisibleIndex = 0
        '
        'INDSlWorkCenter
        '
        Me.INDSlWorkCenter.EnterMoveNextControl = True
        Me.INDSlWorkCenter.Location = New System.Drawing.Point(24, 133)
        Me.INDSlWorkCenter.Name = "INDSlWorkCenter"
        Me.INDSlWorkCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlWorkCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSlWorkCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlWorkCenter.Properties.DisplayMember = "Name"
        Me.INDSlWorkCenter.Properties.NullText = ""
        Me.INDSlWorkCenter.Properties.PopupView = Me.GridView2
        Me.INDSlWorkCenter.Properties.ValueMember = "Id"
        Me.INDSlWorkCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDSlWorkCenter.StyleController = Me.INDLcAutoliquidation
        Me.INDSlWorkCenter.TabIndex = 8
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 50
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'INDGcListEmployee
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcListEmployee, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcListEmployee, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcListEmployee, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcListEmployee, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcListEmployee, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcListEmployee, False)
        Me.INDGcListEmployee.Location = New System.Drawing.Point(438, 99)
        Me.INDGcListEmployee.MainView = Me.GridView1
        Me.INDGcListEmployee.Name = "INDGcListEmployee"
        Me.INDGcListEmployee.Size = New System.Drawing.Size(1196, 408)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcListEmployee, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcListEmployee.TabIndex = 7
        Me.INDGcListEmployee.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColIdentificationType, Me.INDColCedula, Me.INDEmployeeName, Me.INDColOccupation, Me.INDColIngress, Me.INDColInsuranceClass, Me.INDColChangeType, Me.INDColWorkHours, Me.INDColWorkClass, Me.INDColChangeCode, Me.INDColInitialDate, Me.INDColEndDate})
        Me.GridView1.GridControl = Me.INDGcListEmployee
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDColIdentificationType
        '
        Me.INDColIdentificationType.Caption = "Tipo de Identificación"
        Me.INDColIdentificationType.FieldName = "IdentificationType"
        Me.INDColIdentificationType.MinWidth = 21
        Me.INDColIdentificationType.Name = "INDColIdentificationType"
        Me.INDColIdentificationType.OptionsColumn.AllowEdit = False
        Me.INDColIdentificationType.OptionsColumn.AllowFocus = False
        Me.INDColIdentificationType.Visible = True
        Me.INDColIdentificationType.VisibleIndex = 0
        '
        'INDColCedula
        '
        Me.INDColCedula.Caption = "No. Identificación"
        Me.INDColCedula.FieldName = "IdNumber"
        Me.INDColCedula.Name = "INDColCedula"
        Me.INDColCedula.OptionsColumn.AllowEdit = False
        Me.INDColCedula.Visible = True
        Me.INDColCedula.VisibleIndex = 1
        Me.INDColCedula.Width = 129
        '
        'INDEmployeeName
        '
        Me.INDEmployeeName.Caption = "Nombre"
        Me.INDEmployeeName.FieldName = "NameEmployee"
        Me.INDEmployeeName.Name = "INDEmployeeName"
        Me.INDEmployeeName.OptionsColumn.AllowEdit = False
        Me.INDEmployeeName.Visible = True
        Me.INDEmployeeName.VisibleIndex = 2
        Me.INDEmployeeName.Width = 129
        '
        'INDColOccupation
        '
        Me.INDColOccupation.Caption = "Ocupación"
        Me.INDColOccupation.FieldName = "CCSSCode"
        Me.INDColOccupation.MinWidth = 21
        Me.INDColOccupation.Name = "INDColOccupation"
        Me.INDColOccupation.OptionsColumn.AllowEdit = False
        Me.INDColOccupation.Visible = True
        Me.INDColOccupation.VisibleIndex = 3
        '
        'INDColIngress
        '
        Me.INDColIngress.Caption = "Salario Devengado"
        Me.INDColIngress.DisplayFormat.FormatString = "c2"
        Me.INDColIngress.FieldName = "IBCPension"
        Me.INDColIngress.Name = "INDColIngress"
        Me.INDColIngress.OptionsColumn.AllowEdit = False
        Me.INDColIngress.Visible = True
        Me.INDColIngress.VisibleIndex = 4
        Me.INDColIngress.Width = 66
        '
        'INDColInsuranceClass
        '
        Me.INDColInsuranceClass.Caption = "Clase de Seguro"
        Me.INDColInsuranceClass.FieldName = "Pensionary"
        Me.INDColInsuranceClass.MinWidth = 21
        Me.INDColInsuranceClass.Name = "INDColInsuranceClass"
        Me.INDColInsuranceClass.OptionsColumn.AllowEdit = False
        Me.INDColInsuranceClass.OptionsColumn.AllowFocus = False
        Me.INDColInsuranceClass.Visible = True
        Me.INDColInsuranceClass.VisibleIndex = 5
        '
        'INDColChangeType
        '
        Me.INDColChangeType.Caption = "Tipo de Cambio"
        Me.INDColChangeType.FieldName = "ChangeType"
        Me.INDColChangeType.MinWidth = 21
        Me.INDColChangeType.Name = "INDColChangeType"
        Me.INDColChangeType.OptionsColumn.AllowEdit = False
        Me.INDColChangeType.OptionsColumn.AllowFocus = False
        Me.INDColChangeType.Visible = True
        Me.INDColChangeType.VisibleIndex = 6
        '
        'INDColWorkHours
        '
        Me.INDColWorkHours.Caption = "Horas de jornada"
        Me.INDColWorkHours.FieldName = "HoursDaily"
        Me.INDColWorkHours.MinWidth = 21
        Me.INDColWorkHours.Name = "INDColWorkHours"
        Me.INDColWorkHours.OptionsColumn.AllowEdit = False
        Me.INDColWorkHours.Visible = True
        Me.INDColWorkHours.VisibleIndex = 7
        '
        'INDColWorkClass
        '
        Me.INDColWorkClass.Caption = "Clase de Jornada"
        Me.INDColWorkClass.MinWidth = 21
        Me.INDColWorkClass.Name = "INDColWorkClass"
        Me.INDColWorkClass.OptionsColumn.AllowEdit = False
        Me.INDColWorkClass.Visible = True
        Me.INDColWorkClass.VisibleIndex = 8
        '
        'INDColChangeCode
        '
        Me.INDColChangeCode.Caption = "Código de Cambio"
        Me.INDColChangeCode.FieldName = "ChangeCode"
        Me.INDColChangeCode.MinWidth = 21
        Me.INDColChangeCode.Name = "INDColChangeCode"
        Me.INDColChangeCode.OptionsColumn.AllowEdit = False
        Me.INDColChangeCode.Visible = True
        Me.INDColChangeCode.VisibleIndex = 9
        '
        'INDColInitialDate
        '
        Me.INDColInitialDate.Caption = "Fecha de inicio"
        Me.INDColInitialDate.FieldName = "InitialDate"
        Me.INDColInitialDate.MinWidth = 21
        Me.INDColInitialDate.Name = "INDColInitialDate"
        Me.INDColInitialDate.OptionsColumn.AllowEdit = False
        Me.INDColInitialDate.Visible = True
        Me.INDColInitialDate.VisibleIndex = 10
        '
        'INDColEndDate
        '
        Me.INDColEndDate.Caption = "Fecha final"
        Me.INDColEndDate.FieldName = "FinalDate"
        Me.INDColEndDate.MinWidth = 21
        Me.INDColEndDate.Name = "INDColEndDate"
        Me.INDColEndDate.OptionsColumn.AllowEdit = False
        Me.INDColEndDate.Visible = True
        Me.INDColEndDate.VisibleIndex = 11
        '
        'INDBtnProccess
        '
        Me.INDBtnProccess.Location = New System.Drawing.Point(24, 233)
        Me.IndigoSimpleButton3.SetModernUiIndigo(Me.INDBtnProccess, False)
        Me.IndigoSimpleButton2.SetModernUiIndigo(Me.INDBtnProccess, False)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnProccess, False)
        Me.INDBtnProccess.Name = "INDBtnProccess"
        Me.INDBtnProccess.Size = New System.Drawing.Size(386, 32)
        Me.INDBtnProccess.StyleController = Me.INDLcAutoliquidation
        Me.INDBtnProccess.TabIndex = 6
        Me.INDBtnProccess.Text = "Procesar"
        '
        'INDSlCompany
        '
        Me.INDSlCompany.EnterMoveNextControl = True
        Me.INDSlCompany.Location = New System.Drawing.Point(24, 73)
        Me.INDSlCompany.Name = "INDSlCompany"
        Me.INDSlCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlCompany.Properties.Appearance.Options.UseFont = True
        Me.INDSlCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlCompany.Properties.DisplayMember = "Name"
        Me.INDSlCompany.Properties.NullText = ""
        Me.INDSlCompany.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSlCompany.Properties.ValueMember = "Id"
        Me.INDSlCompany.Size = New System.Drawing.Size(386, 28)
        Me.INDSlCompany.StyleController = Me.INDLcAutoliquidation
        Me.INDSlCompany.TabIndex = 4
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNit, Me.INDColName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDColNit
        '
        Me.INDColNit.Caption = "Código"
        Me.INDColNit.FieldName = "Nit"
        Me.INDColNit.Name = "INDColNit"
        Me.INDColNit.OptionsColumn.AllowEdit = False
        Me.INDColNit.Visible = True
        Me.INDColNit.VisibleIndex = 0
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
        '
        'INDBtnExport
        '
        Me.INDBtnExport.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.ICONO_EXCEL_021
        Me.INDBtnExport.Location = New System.Drawing.Point(438, 56)
        Me.INDBtnExport.MaximumSize = New System.Drawing.Size(36, 32)
        Me.INDBtnExport.MinimumSize = New System.Drawing.Size(38, 32)
        Me.IndigoSimpleButton3.SetModernUiIndigo(Me.INDBtnExport, False)
        Me.IndigoSimpleButton2.SetModernUiIndigo(Me.INDBtnExport, False)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnExport, False)
        Me.INDBtnExport.Name = "INDBtnExport"
        Me.INDBtnExport.Size = New System.Drawing.Size(38, 32)
        Me.INDBtnExport.StyleController = Me.INDLcAutoliquidation
        Me.INDBtnExport.TabIndex = 17
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1658, 531)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.INDLciPeriod, Me.LayoutControlItem6})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 511)
        Me.LayoutControlGroup2.Text = "Información Patronal"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSlCompany
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Empresa"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(127, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDBtnProccess
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDSlWorkCenter
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Centros de Trabajo"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(127, 17)
        '
        'INDLciPeriod
        '
        Me.INDLciPeriod.Control = Me.INDSlPeriodDate
        Me.INDLciPeriod.Location = New System.Drawing.Point(0, 120)
        Me.INDLciPeriod.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.Name = "INDLciPeriod"
        Me.INDLciPeriod.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPeriod.Text = "Periodo Liquidación"
        Me.INDLciPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPeriod.TextSize = New System.Drawing.Size(127, 17)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDGcExportExcell
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 216)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(390, 242)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        Me.LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.INDLcExcel, Me.INDLciImportButton})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1224, 511)
        Me.LayoutControlGroup3.Text = "Información Obrera"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDGcListEmployee
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 46)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(1200, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(1200, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1200, 412)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'INDLcExcel
        '
        Me.INDLcExcel.Control = Me.INDBtnExport
        Me.INDLcExcel.CustomizationFormText = "LayoutControlItem1"
        Me.INDLcExcel.Location = New System.Drawing.Point(0, 0)
        Me.INDLcExcel.MaxSize = New System.Drawing.Size(45, 46)
        Me.INDLcExcel.MinSize = New System.Drawing.Size(45, 46)
        Me.INDLcExcel.Name = "INDLcExcel"
        Me.INDLcExcel.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2)
        Me.INDLcExcel.Size = New System.Drawing.Size(45, 46)
        Me.INDLcExcel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcExcel.Text = "LayoutControlItem1"
        Me.INDLcExcel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLcExcel.TextVisible = False
        Me.INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciImportButton
        '
        Me.INDLciImportButton.Control = Me.INDBtnImportFile
        Me.INDLciImportButton.Location = New System.Drawing.Point(45, 0)
        Me.INDLciImportButton.MaxSize = New System.Drawing.Size(45, 40)
        Me.INDLciImportButton.MinSize = New System.Drawing.Size(45, 40)
        Me.INDLciImportButton.Name = "INDLciImportButton"
        Me.INDLciImportButton.Size = New System.Drawing.Size(1155, 46)
        Me.INDLciImportButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciImportButton.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0)
        Me.INDLciImportButton.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciImportButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciImportButton.TextToControlDistance = 0
        Me.INDLciImportButton.TextVisible = False
        Me.INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcAutoliquidation
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 548)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmCRCashReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1296, 692)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmCRCashReport"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "2833"
        Me.Text = "Informa Caja Costarricense"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcAutoliquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcAutoliquidation.ResumeLayout(False)
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlPeriodDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlWorkCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcListEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciImportButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcAutoliquidation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDGcListEmployee As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCedula As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDEmployeeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIngress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnProccess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDSlCompany As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents INDColNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSlWorkCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSlPeriodDate As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton3 As IndigoSimpleButton
    Friend WithEvents IndigoSimpleButton2 As IndigoSimpleButton
    Friend WithEvents INDBtnExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcExcel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcExportExcell As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColumnNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnInitialDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnFinalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColumnSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciImportButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColumnDocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIdentificationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColOccupation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInsuranceClass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColChangeType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWorkHours As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWorkClass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColChangeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColumnOccupation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnCCSSCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnChangeType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnHoursDaily As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnDayClass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnChangeCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
