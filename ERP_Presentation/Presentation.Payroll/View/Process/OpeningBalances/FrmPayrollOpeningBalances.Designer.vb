<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPayrollOpeningBalances
    Inherits Presentation.Controls.FormBase

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnAddOpenBalance = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcEmployeeLiquidation = New DevExpress.XtraGrid.GridControl()
        Me.INDGvLiquidationEmployee = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNitEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPayrollDateLiquidated = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWorkDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTotalAccrued = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTotalDeducted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTotalPaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPensionBase = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPensionEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPensionEmployer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColHealthBase = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColHealthEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColHealthEmployer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIncentiveIBC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIncentiveProvision = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationIBC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationProvision = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnemployeedIBC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnemployeedProvision = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnemployeedInterestProvision = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProvisionDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAmbulatoryInabilityValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColHospitalityInabilityValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INCColMaternityValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBaseSena = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAporteSENA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBaseICBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDAporteICBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBaseCaja = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAporteCaja = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBaseRetention = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRetentionValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLygLiquidation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLycAddButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcEmployeeLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvLiquidationEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycAddButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 579)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDBtnAddOpenBalance)
        Me.LayoutControl1.Controls.Add(Me.INDGcEmployeeLiquidation)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(4, 9)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1000, 565)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDBtnAddOpenBalance
        '
        Me.INDBtnAddOpenBalance.Location = New System.Drawing.Point(5, 5)
        Me.INDBtnAddOpenBalance.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDBtnAddOpenBalance.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDBtnAddOpenBalance.Name = "INDBtnAddOpenBalance"
        Me.INDBtnAddOpenBalance.Size = New System.Drawing.Size(990, 36)
        Me.INDBtnAddOpenBalance.StyleController = Me.LayoutControl1
        Me.INDBtnAddOpenBalance.TabIndex = 17
        Me.INDBtnAddOpenBalance.Text = "Agregar Saldo Inicial"
        '
        'INDGcEmployeeLiquidation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcEmployeeLiquidation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcEmployeeLiquidation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcEmployeeLiquidation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcEmployeeLiquidation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcEmployeeLiquidation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcEmployeeLiquidation, False)
        Me.INDGcEmployeeLiquidation.Location = New System.Drawing.Point(9, 89)
        Me.INDGcEmployeeLiquidation.MainView = Me.INDGvLiquidationEmployee
        Me.INDGcEmployeeLiquidation.Name = "INDGcEmployeeLiquidation"
        Me.INDGcEmployeeLiquidation.Size = New System.Drawing.Size(982, 466)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcEmployeeLiquidation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcEmployeeLiquidation.TabIndex = 16
        Me.INDGcEmployeeLiquidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvLiquidationEmployee})
        '
        'INDGvLiquidationEmployee
        '
        Me.INDGvLiquidationEmployee.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvLiquidationEmployee.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvLiquidationEmployee.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvLiquidationEmployee.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvLiquidationEmployee.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvLiquidationEmployee.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLiquidationEmployee.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvLiquidationEmployee.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLiquidationEmployee.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvLiquidationEmployee.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvLiquidationEmployee.Appearance.Row.Options.UseFont = True
        Me.INDGvLiquidationEmployee.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvLiquidationEmployee.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvLiquidationEmployee.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNitEmployee, Me.INDColNameEmployee, Me.INDColPayrollDateLiquidated, Me.INDColWorkDays, Me.INDColTotalAccrued, Me.INDColTotalDeducted, Me.INDColTotalPaid, Me.INDColPensionBase, Me.INDColPensionEmployee, Me.INDColPensionEmployer, Me.INDColHealthBase, Me.INDColHealthEmployee, Me.INDColHealthEmployer, Me.INDColIncentiveIBC, Me.INDColIncentiveProvision, Me.INDColVacationIBC, Me.INDColVacationProvision, Me.INDColUnemployeedIBC, Me.INDColUnemployeedProvision, Me.INDColUnemployeedInterestProvision, Me.INDColProvisionDays, Me.INDColAmbulatoryInabilityValue, Me.INDColHospitalityInabilityValue, Me.INCColMaternityValue, Me.INDColBaseSena, Me.INDColAporteSENA, Me.INDColBaseICBF, Me.INDAporteICBF, Me.INDColBaseCaja, Me.INDColAporteCaja, Me.INDColBaseRetention, Me.INDColRetentionValue})
        Me.INDGvLiquidationEmployee.GridControl = Me.INDGcEmployeeLiquidation
        Me.INDGvLiquidationEmployee.Name = "INDGvLiquidationEmployee"
        Me.INDGvLiquidationEmployee.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvLiquidationEmployee.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvLiquidationEmployee.OptionsView.ShowAutoFilterRow = True
        Me.INDGvLiquidationEmployee.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvLiquidationEmployee, False)
        '
        'INDColNitEmployee
        '
        Me.INDColNitEmployee.Caption = "Cédula"
        Me.INDColNitEmployee.FieldName = "Employee.ThirdParty.Nit"
        Me.INDColNitEmployee.MaxWidth = 100
        Me.INDColNitEmployee.MinWidth = 100
        Me.INDColNitEmployee.Name = "INDColNitEmployee"
        Me.INDColNitEmployee.OptionsColumn.AllowEdit = False
        Me.INDColNitEmployee.Visible = True
        Me.INDColNitEmployee.VisibleIndex = 0
        Me.INDColNitEmployee.Width = 100
        '
        'INDColNameEmployee
        '
        Me.INDColNameEmployee.Caption = "Nombre Empleado"
        Me.INDColNameEmployee.FieldName = "Employee.ThirdParty.Name"
        Me.INDColNameEmployee.MaxWidth = 200
        Me.INDColNameEmployee.MinWidth = 200
        Me.INDColNameEmployee.Name = "INDColNameEmployee"
        Me.INDColNameEmployee.OptionsColumn.AllowEdit = False
        Me.INDColNameEmployee.Visible = True
        Me.INDColNameEmployee.VisibleIndex = 1
        Me.INDColNameEmployee.Width = 200
        '
        'INDColPayrollDateLiquidated
        '
        Me.INDColPayrollDateLiquidated.Caption = "Fecha Nómina"
        Me.INDColPayrollDateLiquidated.FieldName = "PayrollDateLiquidated"
        Me.INDColPayrollDateLiquidated.MaxWidth = 150
        Me.INDColPayrollDateLiquidated.MinWidth = 150
        Me.INDColPayrollDateLiquidated.Name = "INDColPayrollDateLiquidated"
        Me.INDColPayrollDateLiquidated.OptionsColumn.AllowEdit = False
        Me.INDColPayrollDateLiquidated.Visible = True
        Me.INDColPayrollDateLiquidated.VisibleIndex = 2
        Me.INDColPayrollDateLiquidated.Width = 150
        '
        'INDColWorkDays
        '
        Me.INDColWorkDays.Caption = "Dias Trabajados"
        Me.INDColWorkDays.FieldName = "DaysWorked"
        Me.INDColWorkDays.MaxWidth = 150
        Me.INDColWorkDays.MinWidth = 150
        Me.INDColWorkDays.Name = "INDColWorkDays"
        Me.INDColWorkDays.OptionsColumn.AllowEdit = False
        Me.INDColWorkDays.Visible = True
        Me.INDColWorkDays.VisibleIndex = 3
        Me.INDColWorkDays.Width = 150
        '
        'INDColTotalAccrued
        '
        Me.INDColTotalAccrued.Caption = "Total Devengado"
        Me.INDColTotalAccrued.FieldName = "TotalAccrued"
        Me.INDColTotalAccrued.MaxWidth = 190
        Me.INDColTotalAccrued.MinWidth = 150
        Me.INDColTotalAccrued.Name = "INDColTotalAccrued"
        Me.INDColTotalAccrued.OptionsColumn.AllowEdit = False
        Me.INDColTotalAccrued.Visible = True
        Me.INDColTotalAccrued.VisibleIndex = 4
        Me.INDColTotalAccrued.Width = 150
        '
        'INDColTotalDeducted
        '
        Me.INDColTotalDeducted.Caption = "Total Deducido"
        Me.INDColTotalDeducted.FieldName = "TotalDeducted"
        Me.INDColTotalDeducted.MaxWidth = 190
        Me.INDColTotalDeducted.MinWidth = 150
        Me.INDColTotalDeducted.Name = "INDColTotalDeducted"
        Me.INDColTotalDeducted.OptionsColumn.AllowEdit = False
        Me.INDColTotalDeducted.Visible = True
        Me.INDColTotalDeducted.VisibleIndex = 5
        Me.INDColTotalDeducted.Width = 150
        '
        'INDColTotalPaid
        '
        Me.INDColTotalPaid.Caption = "Total Pagado"
        Me.INDColTotalPaid.FieldName = "TotalPaid"
        Me.INDColTotalPaid.MaxWidth = 190
        Me.INDColTotalPaid.MinWidth = 150
        Me.INDColTotalPaid.Name = "INDColTotalPaid"
        Me.INDColTotalPaid.OptionsColumn.AllowEdit = False
        Me.INDColTotalPaid.Visible = True
        Me.INDColTotalPaid.VisibleIndex = 6
        Me.INDColTotalPaid.Width = 150
        '
        'INDColPensionBase
        '
        Me.INDColPensionBase.Caption = "Base Pensión"
        Me.INDColPensionBase.FieldName = "PensionJCB"
        Me.INDColPensionBase.MaxWidth = 190
        Me.INDColPensionBase.MinWidth = 150
        Me.INDColPensionBase.Name = "INDColPensionBase"
        Me.INDColPensionBase.OptionsColumn.AllowEdit = False
        Me.INDColPensionBase.Visible = True
        Me.INDColPensionBase.VisibleIndex = 7
        Me.INDColPensionBase.Width = 150
        '
        'INDColPensionEmployee
        '
        Me.INDColPensionEmployee.Caption = "Aporte Pensión Empleado"
        Me.INDColPensionEmployee.FieldName = "PensionContributionValue"
        Me.INDColPensionEmployee.MaxWidth = 190
        Me.INDColPensionEmployee.MinWidth = 150
        Me.INDColPensionEmployee.Name = "INDColPensionEmployee"
        Me.INDColPensionEmployee.OptionsColumn.AllowEdit = False
        Me.INDColPensionEmployee.Visible = True
        Me.INDColPensionEmployee.VisibleIndex = 8
        Me.INDColPensionEmployee.Width = 150
        '
        'INDColPensionEmployer
        '
        Me.INDColPensionEmployer.Caption = "Aporte Pensión Patrono"
        Me.INDColPensionEmployer.FieldName = "EmployerPensionContributionValue"
        Me.INDColPensionEmployer.MaxWidth = 190
        Me.INDColPensionEmployer.MinWidth = 150
        Me.INDColPensionEmployer.Name = "INDColPensionEmployer"
        Me.INDColPensionEmployer.OptionsColumn.AllowEdit = False
        Me.INDColPensionEmployer.Visible = True
        Me.INDColPensionEmployer.VisibleIndex = 9
        Me.INDColPensionEmployer.Width = 150
        '
        'INDColHealthBase
        '
        Me.INDColHealthBase.Caption = "Base Salud"
        Me.INDColHealthBase.FieldName = "HealthJCB"
        Me.INDColHealthBase.MaxWidth = 190
        Me.INDColHealthBase.MinWidth = 150
        Me.INDColHealthBase.Name = "INDColHealthBase"
        Me.INDColHealthBase.OptionsColumn.AllowEdit = False
        Me.INDColHealthBase.Visible = True
        Me.INDColHealthBase.VisibleIndex = 10
        Me.INDColHealthBase.Width = 150
        '
        'INDColHealthEmployee
        '
        Me.INDColHealthEmployee.Caption = "Aporte Salud Empleado"
        Me.INDColHealthEmployee.FieldName = "EmployeeHealthContributionValue"
        Me.INDColHealthEmployee.MaxWidth = 190
        Me.INDColHealthEmployee.MinWidth = 150
        Me.INDColHealthEmployee.Name = "INDColHealthEmployee"
        Me.INDColHealthEmployee.OptionsColumn.AllowEdit = False
        Me.INDColHealthEmployee.Visible = True
        Me.INDColHealthEmployee.VisibleIndex = 11
        Me.INDColHealthEmployee.Width = 150
        '
        'INDColHealthEmployer
        '
        Me.INDColHealthEmployer.Caption = "Aporte Salud Patrono"
        Me.INDColHealthEmployer.FieldName = "EmployerHealthContributionValue"
        Me.INDColHealthEmployer.MaxWidth = 190
        Me.INDColHealthEmployer.MinWidth = 150
        Me.INDColHealthEmployer.Name = "INDColHealthEmployer"
        Me.INDColHealthEmployer.OptionsColumn.AllowEdit = False
        Me.INDColHealthEmployer.Visible = True
        Me.INDColHealthEmployer.VisibleIndex = 12
        Me.INDColHealthEmployer.Width = 150
        '
        'INDColIncentiveIBC
        '
        Me.INDColIncentiveIBC.Caption = "Base Primas"
        Me.INDColIncentiveIBC.FieldName = "IBCIncentivePayment"
        Me.INDColIncentiveIBC.MaxWidth = 190
        Me.INDColIncentiveIBC.MinWidth = 150
        Me.INDColIncentiveIBC.Name = "INDColIncentiveIBC"
        Me.INDColIncentiveIBC.OptionsColumn.AllowEdit = False
        Me.INDColIncentiveIBC.Visible = True
        Me.INDColIncentiveIBC.VisibleIndex = 13
        Me.INDColIncentiveIBC.Width = 150
        '
        'INDColIncentiveProvision
        '
        Me.INDColIncentiveProvision.Caption = "Provisión Primas"
        Me.INDColIncentiveProvision.FieldName = "ProvisionIncentive"
        Me.INDColIncentiveProvision.MaxWidth = 190
        Me.INDColIncentiveProvision.MinWidth = 150
        Me.INDColIncentiveProvision.Name = "INDColIncentiveProvision"
        Me.INDColIncentiveProvision.OptionsColumn.AllowEdit = False
        Me.INDColIncentiveProvision.Visible = True
        Me.INDColIncentiveProvision.VisibleIndex = 14
        Me.INDColIncentiveProvision.Width = 150
        '
        'INDColVacationIBC
        '
        Me.INDColVacationIBC.Caption = "Base Vacaciones"
        Me.INDColVacationIBC.FieldName = "IBCVacation"
        Me.INDColVacationIBC.MaxWidth = 190
        Me.INDColVacationIBC.MinWidth = 150
        Me.INDColVacationIBC.Name = "INDColVacationIBC"
        Me.INDColVacationIBC.OptionsColumn.AllowEdit = False
        Me.INDColVacationIBC.Visible = True
        Me.INDColVacationIBC.VisibleIndex = 15
        Me.INDColVacationIBC.Width = 150
        '
        'INDColVacationProvision
        '
        Me.INDColVacationProvision.Caption = "Provisión Vacaciones"
        Me.INDColVacationProvision.FieldName = "ProvisionVacation"
        Me.INDColVacationProvision.MaxWidth = 190
        Me.INDColVacationProvision.MinWidth = 170
        Me.INDColVacationProvision.Name = "INDColVacationProvision"
        Me.INDColVacationProvision.OptionsColumn.AllowEdit = False
        Me.INDColVacationProvision.Visible = True
        Me.INDColVacationProvision.VisibleIndex = 16
        Me.INDColVacationProvision.Width = 170
        '
        'INDColUnemployeedIBC
        '
        Me.INDColUnemployeedIBC.Caption = "Base Cesantias"
        Me.INDColUnemployeedIBC.FieldName = "IBCUnemployment"
        Me.INDColUnemployeedIBC.MaxWidth = 190
        Me.INDColUnemployeedIBC.MinWidth = 150
        Me.INDColUnemployeedIBC.Name = "INDColUnemployeedIBC"
        Me.INDColUnemployeedIBC.OptionsColumn.AllowEdit = False
        Me.INDColUnemployeedIBC.Visible = True
        Me.INDColUnemployeedIBC.VisibleIndex = 17
        Me.INDColUnemployeedIBC.Width = 150
        '
        'INDColUnemployeedProvision
        '
        Me.INDColUnemployeedProvision.Caption = "Provision Cesantías"
        Me.INDColUnemployeedProvision.FieldName = "UnemploymentAccumulated"
        Me.INDColUnemployeedProvision.MaxWidth = 190
        Me.INDColUnemployeedProvision.MinWidth = 150
        Me.INDColUnemployeedProvision.Name = "INDColUnemployeedProvision"
        Me.INDColUnemployeedProvision.OptionsColumn.AllowEdit = False
        Me.INDColUnemployeedProvision.Visible = True
        Me.INDColUnemployeedProvision.VisibleIndex = 18
        Me.INDColUnemployeedProvision.Width = 150
        '
        'INDColUnemployeedInterestProvision
        '
        Me.INDColUnemployeedInterestProvision.Caption = "Provisión Int. Cesantias"
        Me.INDColUnemployeedInterestProvision.FieldName = "ProvisionInterestsUnemployment"
        Me.INDColUnemployeedInterestProvision.MaxWidth = 190
        Me.INDColUnemployeedInterestProvision.MinWidth = 150
        Me.INDColUnemployeedInterestProvision.Name = "INDColUnemployeedInterestProvision"
        Me.INDColUnemployeedInterestProvision.OptionsColumn.AllowEdit = False
        Me.INDColUnemployeedInterestProvision.Visible = True
        Me.INDColUnemployeedInterestProvision.VisibleIndex = 19
        Me.INDColUnemployeedInterestProvision.Width = 150
        '
        'INDColProvisionDays
        '
        Me.INDColProvisionDays.Caption = "Días Provisión"
        Me.INDColProvisionDays.FieldName = "ProvisionDays"
        Me.INDColProvisionDays.MaxWidth = 190
        Me.INDColProvisionDays.MinWidth = 150
        Me.INDColProvisionDays.Name = "INDColProvisionDays"
        Me.INDColProvisionDays.OptionsColumn.AllowEdit = False
        Me.INDColProvisionDays.Visible = True
        Me.INDColProvisionDays.VisibleIndex = 20
        Me.INDColProvisionDays.Width = 150
        '
        'INDColAmbulatoryInabilityValue
        '
        Me.INDColAmbulatoryInabilityValue.Caption = "Vlr. Incap. Ambulatoria"
        Me.INDColAmbulatoryInabilityValue.FieldName = "AmbulatoryDisabilityValue"
        Me.INDColAmbulatoryInabilityValue.MaxWidth = 190
        Me.INDColAmbulatoryInabilityValue.MinWidth = 150
        Me.INDColAmbulatoryInabilityValue.Name = "INDColAmbulatoryInabilityValue"
        Me.INDColAmbulatoryInabilityValue.OptionsColumn.AllowEdit = False
        Me.INDColAmbulatoryInabilityValue.Visible = True
        Me.INDColAmbulatoryInabilityValue.VisibleIndex = 21
        Me.INDColAmbulatoryInabilityValue.Width = 150
        '
        'INDColHospitalityInabilityValue
        '
        Me.INDColHospitalityInabilityValue.Caption = "Vlr. Incap. Hospitalaria"
        Me.INDColHospitalityInabilityValue.FieldName = "DisabilityHospitalValue"
        Me.INDColHospitalityInabilityValue.MaxWidth = 190
        Me.INDColHospitalityInabilityValue.MinWidth = 150
        Me.INDColHospitalityInabilityValue.Name = "INDColHospitalityInabilityValue"
        Me.INDColHospitalityInabilityValue.OptionsColumn.AllowEdit = False
        Me.INDColHospitalityInabilityValue.Visible = True
        Me.INDColHospitalityInabilityValue.VisibleIndex = 22
        Me.INDColHospitalityInabilityValue.Width = 150
        '
        'INCColMaternityValue
        '
        Me.INCColMaternityValue.Caption = "Vlr. Lic. Maternidad"
        Me.INCColMaternityValue.FieldName = "MaternityLeaveValue"
        Me.INCColMaternityValue.MaxWidth = 190
        Me.INCColMaternityValue.MinWidth = 150
        Me.INCColMaternityValue.Name = "INCColMaternityValue"
        Me.INCColMaternityValue.OptionsColumn.AllowEdit = False
        Me.INCColMaternityValue.Visible = True
        Me.INCColMaternityValue.VisibleIndex = 23
        Me.INCColMaternityValue.Width = 150
        '
        'INDColBaseSena
        '
        Me.INDColBaseSena.Caption = "Base Sena"
        Me.INDColBaseSena.FieldName = "IBCSENA"
        Me.INDColBaseSena.MaxWidth = 190
        Me.INDColBaseSena.MinWidth = 150
        Me.INDColBaseSena.Name = "INDColBaseSena"
        Me.INDColBaseSena.OptionsColumn.AllowEdit = False
        Me.INDColBaseSena.Visible = True
        Me.INDColBaseSena.VisibleIndex = 24
        Me.INDColBaseSena.Width = 150
        '
        'INDColAporteSENA
        '
        Me.INDColAporteSENA.Caption = "Aporte Sena"
        Me.INDColAporteSENA.FieldName = "SenaContributionValue"
        Me.INDColAporteSENA.MaxWidth = 190
        Me.INDColAporteSENA.MinWidth = 150
        Me.INDColAporteSENA.Name = "INDColAporteSENA"
        Me.INDColAporteSENA.OptionsColumn.AllowEdit = False
        Me.INDColAporteSENA.Visible = True
        Me.INDColAporteSENA.VisibleIndex = 25
        Me.INDColAporteSENA.Width = 150
        '
        'INDColBaseICBF
        '
        Me.INDColBaseICBF.Caption = "Base ICBF"
        Me.INDColBaseICBF.FieldName = "IBCICBF"
        Me.INDColBaseICBF.MaxWidth = 190
        Me.INDColBaseICBF.MinWidth = 150
        Me.INDColBaseICBF.Name = "INDColBaseICBF"
        Me.INDColBaseICBF.OptionsColumn.AllowEdit = False
        Me.INDColBaseICBF.Visible = True
        Me.INDColBaseICBF.VisibleIndex = 26
        Me.INDColBaseICBF.Width = 150
        '
        'INDAporteICBF
        '
        Me.INDAporteICBF.Caption = "Aporte ICBF"
        Me.INDAporteICBF.FieldName = "ICBFContributionValue"
        Me.INDAporteICBF.MaxWidth = 190
        Me.INDAporteICBF.MinWidth = 150
        Me.INDAporteICBF.Name = "INDAporteICBF"
        Me.INDAporteICBF.OptionsColumn.AllowEdit = False
        Me.INDAporteICBF.Visible = True
        Me.INDAporteICBF.VisibleIndex = 27
        Me.INDAporteICBF.Width = 150
        '
        'INDColBaseCaja
        '
        Me.INDColBaseCaja.Caption = "Base Caja Compensación"
        Me.INDColBaseCaja.FieldName = "IBCCompensationFund"
        Me.INDColBaseCaja.MaxWidth = 190
        Me.INDColBaseCaja.MinWidth = 150
        Me.INDColBaseCaja.Name = "INDColBaseCaja"
        Me.INDColBaseCaja.OptionsColumn.AllowEdit = False
        Me.INDColBaseCaja.Visible = True
        Me.INDColBaseCaja.VisibleIndex = 28
        Me.INDColBaseCaja.Width = 150
        '
        'INDColAporteCaja
        '
        Me.INDColAporteCaja.Caption = "Aporte Caja Compensación"
        Me.INDColAporteCaja.FieldName = "FamilyCompensationFundContributionValue"
        Me.INDColAporteCaja.MaxWidth = 190
        Me.INDColAporteCaja.MinWidth = 150
        Me.INDColAporteCaja.Name = "INDColAporteCaja"
        Me.INDColAporteCaja.OptionsColumn.AllowEdit = False
        Me.INDColAporteCaja.Visible = True
        Me.INDColAporteCaja.VisibleIndex = 29
        Me.INDColAporteCaja.Width = 150
        '
        'INDColBaseRetention
        '
        Me.INDColBaseRetention.Caption = "Base Retención"
        Me.INDColBaseRetention.FieldName = "TotalBaseRetention"
        Me.INDColBaseRetention.MaxWidth = 190
        Me.INDColBaseRetention.MinWidth = 150
        Me.INDColBaseRetention.Name = "INDColBaseRetention"
        Me.INDColBaseRetention.OptionsColumn.AllowEdit = False
        Me.INDColBaseRetention.Visible = True
        Me.INDColBaseRetention.VisibleIndex = 30
        Me.INDColBaseRetention.Width = 150
        '
        'INDColRetentionValue
        '
        Me.INDColRetentionValue.Caption = "Valor Retención"
        Me.INDColRetentionValue.FieldName = "CalculatedWithholdingValue"
        Me.INDColRetentionValue.MaxWidth = 190
        Me.INDColRetentionValue.MinWidth = 150
        Me.INDColRetentionValue.Name = "INDColRetentionValue"
        Me.INDColRetentionValue.OptionsColumn.AllowEdit = False
        Me.INDColRetentionValue.Visible = True
        Me.INDColRetentionValue.VisibleIndex = 31
        Me.INDColRetentionValue.Width = 190
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
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLygLiquidation, Me.INDLycAddButton})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1000, 565)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLygLiquidation
        '
        Me.INDLygLiquidation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygLiquidation.AppearanceGroup.Options.UseFont = True
        Me.INDLygLiquidation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygLiquidation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygLiquidation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygLiquidation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygLiquidation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygLiquidation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygLiquidation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygLiquidation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygLiquidation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygLiquidation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygLiquidation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygLiquidation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygLiquidation, False)
        Me.INDLygLiquidation.CustomizationFormText = "Liquidación"
        Me.INDLygLiquidation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.INDLygLiquidation.Location = New System.Drawing.Point(0, 46)
        Me.INDLygLiquidation.Name = "INDLygLiquidation"
        Me.INDLygLiquidation.Size = New System.Drawing.Size(1000, 519)
        Me.INDLygLiquidation.Text = "Liquidación"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDGcEmployeeLiquidation
        Me.LayoutControlItem2.CustomizationFormText = "Listado Liquidación"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(992, 476)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLycAddButton
        '
        Me.INDLycAddButton.Control = Me.INDBtnAddOpenBalance
        Me.INDLycAddButton.CustomizationFormText = "Agregar Saldo Inicial"
        Me.INDLycAddButton.Location = New System.Drawing.Point(0, 0)
        Me.INDLycAddButton.Name = "INDLycAddButton"
        Me.INDLycAddButton.Size = New System.Drawing.Size(1000, 46)
        Me.INDLycAddButton.Text = "Agregar"
        Me.INDLycAddButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLycAddButton.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPayrollOpeningBalances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 701)
        Me.Name = "FrmPayrollOpeningBalances"
        Me.Opacity = 1.0R
        Me.Tag = "501"
        Me.Text = "Verificación Saldo Inicial"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcEmployeeLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvLiquidationEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycAddButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDGcEmployeeLiquidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvLiquidationEmployee As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColNitEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationIBC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationProvision As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnemployeedIBC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnemployeedProvision As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDColUnemployeedInterestProvision As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIncentiveIBC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIncentiveProvision As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColHealthBase As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColHealthEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColHealthEmployer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPensionBase As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPensionEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPensionEmployer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoCheckEdit1 As Presentation.Controls.IndigoCheckEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDBtnAddOpenBalance As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLycAddButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColWorkDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTotalAccrued As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTotalDeducted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTotalPaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProvisionDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAmbulatoryInabilityValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColHospitalityInabilityValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INCColMaternityValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBaseSena As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAporteSENA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBaseICBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDAporteICBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBaseCaja As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAporteCaja As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBaseRetention As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRetentionValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPayrollDateLiquidated As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLygLiquidation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
