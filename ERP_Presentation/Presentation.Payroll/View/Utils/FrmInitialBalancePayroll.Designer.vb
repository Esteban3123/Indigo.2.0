Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmInitialBalancePayroll
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInitialBalancePayroll))
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAddEmployee = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDEsbBills = New Presentation.Controls.ExportStructureButton()
        Me.INDgcInformation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAccrued = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalDeducted = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotal = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInformation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAccrued, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalDeducted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDbtnAddEmployee
        '
        Me.INDbtnAddEmployee.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddEmployee.Appearance.Options.UseFont = True
        Me.INDbtnAddEmployee.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddEmployee, True)
        Me.INDbtnAddEmployee.Name = "INDbtnAddEmployee"
        Me.INDbtnAddEmployee.Size = New System.Drawing.Size(1121, 32)
        Me.INDbtnAddEmployee.StyleController = Me.INDlyRoot
        Me.INDbtnAddEmployee.TabIndex = 5
        Me.INDbtnAddEmployee.Text = "Agregar Empleado"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDBtnImportFile)
        Me.INDlyRoot.Controls.Add(Me.INDEsbBills)
        Me.INDlyRoot.Controls.Add(Me.INDbtnAddEmployee)
        Me.INDlyRoot.Controls.Add(Me.INDgcInformation)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(445, 432, 522, 537)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1261, 570)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.Image = CType(resources.GetObject("INDBtnImportFile.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(1195, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(42, 32)
        Me.INDBtnImportFile.StyleController = Me.INDlyRoot
        Me.INDBtnImportFile.TabIndex = 21
        Me.INDBtnImportFile.Text = "SimpleButton1"
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDEsbBills
        '
        Me.INDEsbBills.Image = CType(resources.GetObject("INDEsbBills.Image"), System.Drawing.Image)
        Me.INDEsbBills.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbBills.Location = New System.Drawing.Point(1149, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDEsbBills, False)
        Me.INDEsbBills.Name = "INDEsbBills"
        Me.INDEsbBills.Size = New System.Drawing.Size(42, 32)
        Me.INDEsbBills.StyleController = Me.INDlyRoot
        Me.INDEsbBills.TabIndex = 16
        Me.INDEsbBills.Text = "ExportStructureButton3"
        Me.INDEsbBills.ToolTip = "Exportar Estructura"
        '
        'INDgcInformation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInformation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInformation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInformation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInformation, False)
        Me.INDgcInformation.Location = New System.Drawing.Point(24, 95)
        Me.INDgcInformation.MainView = Me.INDviewInfo
        Me.INDgcInformation.Name = "INDgcInformation"
        Me.INDgcInformation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtTotalAccrued, Me.INDrepTxtTotalDeducted, Me.INDrepTxtTotal})
        Me.INDgcInformation.Size = New System.Drawing.Size(1213, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInformation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
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
        Me.INDviewInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewInfo.GridControl = Me.INDgcInformation
        Me.INDviewInfo.Name = "INDviewInfo"
        Me.INDviewInfo.OptionsSelection.MultiSelect = True
        Me.INDviewInfo.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewInfo.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewInfo.OptionsView.ShowAutoFilterRow = True
        Me.INDviewInfo.OptionsView.ShowDetailButtons = False
        Me.INDviewInfo.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInfo, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Empleado"
        Me.GridColumn1.FieldName = "NitName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 510
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha Nómina"
        Me.GridColumn2.FieldName = "PayrollDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 116
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Días Trabajados"
        Me.GridColumn3.FieldName = "DaysWorked"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 124
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Total Devengado"
        Me.GridColumn4.ColumnEdit = Me.INDrepTxtTotalAccrued
        Me.GridColumn4.FieldName = "TotalAccrued"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 160
        '
        'INDrepTxtTotalAccrued
        '
        Me.INDrepTxtTotalAccrued.AutoHeight = False
        Me.INDrepTxtTotalAccrued.Mask.EditMask = "C0"
        Me.INDrepTxtTotalAccrued.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAccrued.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAccrued.Name = "INDrepTxtTotalAccrued"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Total Deducido"
        Me.GridColumn5.ColumnEdit = Me.INDrepTxtTotalDeducted
        Me.GridColumn5.FieldName = "TotalDeducted"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 152
        '
        'INDrepTxtTotalDeducted
        '
        Me.INDrepTxtTotalDeducted.AutoHeight = False
        Me.INDrepTxtTotalDeducted.Mask.EditMask = "C0"
        Me.INDrepTxtTotalDeducted.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalDeducted.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalDeducted.Name = "INDrepTxtTotalDeducted"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Total Pagado"
        Me.GridColumn6.ColumnEdit = Me.INDrepTxtTotal
        Me.GridColumn6.FieldName = "TotalPaid"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 133
        '
        'INDrepTxtTotal
        '
        Me.INDrepTxtTotal.AutoHeight = False
        Me.INDrepTxtTotal.Mask.EditMask = "C0"
        Me.INDrepTxtTotal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotal.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotal.Name = "INDrepTxtTotal"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygInformation})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygInformation
        '
        Me.INDlygInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygInformation, False)
        Me.INDlygInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInformation, Me.INDlyItemAddEmployee, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDlygInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygInformation.Name = "INDlygInformation"
        Me.INDlygInformation.Size = New System.Drawing.Size(1241, 550)
        Me.INDlygInformation.Text = "Información Principal"
        '
        'INDlyItemInformation
        '
        Me.INDlyItemInformation.Control = Me.INDgcInformation
        Me.INDlyItemInformation.CustomizationFormText = "Listado de Empleados"
        Me.INDlyItemInformation.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemInformation.Name = "INDlyItemInformation"
        Me.INDlyItemInformation.Size = New System.Drawing.Size(1217, 455)
        Me.INDlyItemInformation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemInformation.TextVisible = False
        '
        'INDlyItemAddEmployee
        '
        Me.INDlyItemAddEmployee.Control = Me.INDbtnAddEmployee
        Me.INDlyItemAddEmployee.CustomizationFormText = "Agregar Empleado"
        Me.INDlyItemAddEmployee.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddEmployee.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemAddEmployee.MinSize = New System.Drawing.Size(103, 36)
        Me.INDlyItemAddEmployee.Name = "INDlyItemAddEmployee"
        Me.INDlyItemAddEmployee.Size = New System.Drawing.Size(1125, 36)
        Me.INDlyItemAddEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddEmployee.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddEmployee.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDEsbBills
        Me.LayoutControlItem1.CustomizationFormText = "Exportar Estructura"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(1125, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDBtnImportFile
        Me.LayoutControlItem2.CustomizationFormText = "Importar Datos"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(1171, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
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
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmInitialBalancePayroll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInitialBalancePayroll"
        Me.Opacity = 1.0R
        Me.Tag = "1955"
        Me.Text = "Saldo Inicial"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAccrued, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalDeducted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDgcInformation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemInformation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAddEmployee As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDEsbBills As ExportStructureButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtTotalAccrued As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtTotalDeducted As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtTotal As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
