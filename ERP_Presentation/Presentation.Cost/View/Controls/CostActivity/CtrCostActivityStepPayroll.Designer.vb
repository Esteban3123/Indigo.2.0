<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostActivityStepPayroll
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleCostActivityStep = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCostActivityStep = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCostActivityStepOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCostActivityStepDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleCostActivityStepPayroll = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvPayroll = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgvPayrollCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvPayrollName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseHours = New DevExpress.XtraEditors.SpinEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCostActivityStepPayroll = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCostActivityStep = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDsleCostActivityStep.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvCostActivityStep,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleCostActivityStepPayroll.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvPayroll,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDseHours.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciCostActivityStepPayroll,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciCostActivityStep,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciHours,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsleCostActivityStep)
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Controls.Add(Me.INDsleCostActivityStepPayroll)
        Me.INDlcRoot.Controls.Add(Me.INDseHours)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(411, 240)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsleCostActivityStep
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCostActivityStep, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCostActivityStep, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCostActivityStep, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.INDsleCostActivityStep.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.INDsleCostActivityStep.Location = New System.Drawing.Point(12, 37)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCostActivityStep, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCostActivityStep.Name = "INDsleCostActivityStep"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.INDsleCostActivityStep.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCostActivityStep.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleCostActivityStep.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleCostActivityStep.Properties.Appearance.Options.UseFont = true
        Me.INDsleCostActivityStep.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCostActivityStep.Properties.DisplayMember = "OrderDescription"
        Me.INDsleCostActivityStep.Properties.NullText = ""
        Me.INDsleCostActivityStep.Properties.PopupSizeable = false
        Me.INDsleCostActivityStep.Properties.ShowFooter = false
        Me.INDsleCostActivityStep.Properties.ValueMember = "Order"
        Me.INDsleCostActivityStep.Properties.View = Me.INDgvCostActivityStep
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCostActivityStep, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCostActivityStep, true)
        Me.INDsleCostActivityStep.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCostActivityStep.StyleController = Me.INDlcRoot
        Me.INDsleCostActivityStep.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCostActivityStep, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCostActivityStep, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCostActivityStep, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCostActivityStep, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCostActivityStep, false)
        '
        'INDgvCostActivityStep
        '
        Me.INDgvCostActivityStep.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvCostActivityStep.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvCostActivityStep.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvCostActivityStep.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvCostActivityStep.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvCostActivityStep.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvCostActivityStep.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvCostActivityStep.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvCostActivityStep.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCostActivityStep.Appearance.Row.Options.UseFont = true
        Me.INDgvCostActivityStep.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolCostActivityStepOrder, Me.INDcolCostActivityStepDescription})
        Me.INDgvCostActivityStep.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvCostActivityStep.Name = "INDgvCostActivityStep"
        Me.INDgvCostActivityStep.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDgvCostActivityStep.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvCostActivityStep.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvCostActivityStep.OptionsView.ShowAutoFilterRow = true
        Me.INDgvCostActivityStep.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvCostActivityStep, false)
        '
        'INDcolCostActivityStepOrder
        '
        Me.INDcolCostActivityStepOrder.Caption = "Orden"
        Me.INDcolCostActivityStepOrder.FieldName = "Order"
        Me.INDcolCostActivityStepOrder.Name = "INDcolCostActivityStepOrder"
        Me.INDcolCostActivityStepOrder.OptionsColumn.AllowEdit = false
        Me.INDcolCostActivityStepOrder.OptionsColumn.AllowFocus = false
        Me.INDcolCostActivityStepOrder.Visible = true
        Me.INDcolCostActivityStepOrder.VisibleIndex = 0
        '
        'INDcolCostActivityStepDescription
        '
        Me.INDcolCostActivityStepDescription.Caption = "Descripción"
        Me.INDcolCostActivityStepDescription.FieldName = "Description"
        Me.INDcolCostActivityStepDescription.Name = "INDcolCostActivityStepDescription"
        Me.INDcolCostActivityStepDescription.OptionsColumn.AllowEdit = false
        Me.INDcolCostActivityStepDescription.OptionsColumn.AllowFocus = false
        Me.INDcolCostActivityStepDescription.Visible = true
        Me.INDcolCostActivityStepDescription.VisibleIndex = 1
        '
        'INDsbAdd
        '
        Me.INDsbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDsbAdd.Appearance.Options.UseFont = true
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 196)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDsleCostActivityStepPayroll
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCostActivityStepPayroll, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCostActivityStepPayroll, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.INDsleCostActivityStepPayroll.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.INDsleCostActivityStepPayroll.Location = New System.Drawing.Point(12, 100)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCostActivityStepPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCostActivityStepPayroll.Name = "INDsleCostActivityStepPayroll"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.INDsleCostActivityStepPayroll.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCostActivityStepPayroll.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleCostActivityStepPayroll.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleCostActivityStepPayroll.Properties.Appearance.Options.UseFont = true
        Me.INDsleCostActivityStepPayroll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCostActivityStepPayroll.Properties.DisplayMember = "CodeName"
        Me.INDsleCostActivityStepPayroll.Properties.NullText = ""
        Me.INDsleCostActivityStepPayroll.Properties.PopupSizeable = false
        Me.INDsleCostActivityStepPayroll.Properties.ShowFooter = false
        Me.INDsleCostActivityStepPayroll.Properties.ValueMember = "Id"
        Me.INDsleCostActivityStepPayroll.Properties.View = Me.INDgvPayroll
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCostActivityStepPayroll, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCostActivityStepPayroll, true)
        Me.INDsleCostActivityStepPayroll.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCostActivityStepPayroll.StyleController = Me.INDlcRoot
        Me.INDsleCostActivityStepPayroll.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCostActivityStepPayroll, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCostActivityStepPayroll, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCostActivityStepPayroll, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCostActivityStepPayroll, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCostActivityStepPayroll, false)
        '
        'INDgvPayroll
        '
        Me.INDgvPayroll.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvPayroll.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvPayroll.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvPayroll.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvPayroll.Appearance.FocusedRow.Options.UseForeColor = true
        Me.INDgvPayroll.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvPayroll.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvPayroll.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvPayroll.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvPayroll.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvPayroll.Appearance.Row.Options.UseFont = true
        Me.INDgvPayroll.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgvPayrollCode, Me.INDgvPayrollName})
        Me.INDgvPayroll.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvPayroll.Name = "INDgvPayroll"
        Me.INDgvPayroll.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDgvPayroll.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvPayroll.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvPayroll.OptionsView.ShowAutoFilterRow = true
        Me.INDgvPayroll.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvPayroll, false)
        '
        'INDgvPayrollCode
        '
        Me.INDgvPayrollCode.Caption = "Código"
        Me.INDgvPayrollCode.FieldName = "Codigo"
        Me.INDgvPayrollCode.Name = "INDgvPayrollCode"
        Me.INDgvPayrollCode.Visible = true
        Me.INDgvPayrollCode.VisibleIndex = 0
        '
        'INDgvPayrollName
        '
        Me.INDgvPayrollName.Caption = "Descripción"
        Me.INDgvPayrollName.FieldName = "Descripcion"
        Me.INDgvPayrollName.Name = "INDgvPayrollName"
        Me.INDgvPayrollName.Visible = true
        Me.INDgvPayrollName.VisibleIndex = 1
        '
        'INDseHours
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseHours, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseHours, true)
        Me.INDseHours.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseHours.EnterMoveNextControl = true
        Me.INDseHours.Location = New System.Drawing.Point(12, 162)
        Me.IndigoTextEdit1.SetMascara(Me.INDseHours, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseHours.Name = "INDseHours"
        Me.INDseHours.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseHours.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseHours.Properties.Appearance.Options.UseBackColor = true
        Me.INDseHours.Properties.Appearance.Options.UseFont = true
        Me.INDseHours.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDseHours.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDseHours.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseHours.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDseHours.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDseHours.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDseHours.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseHours.Properties.DisplayFormat.FormatString = "f6"
        Me.INDseHours.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDseHours.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseHours.Properties.Mask.EditMask = "f6"
        Me.INDseHours.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDseHours.Properties.MaxLength = 24
        Me.INDseHours.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseHours.Size = New System.Drawing.Size(386, 28)
        Me.INDseHours.StyleController = Me.INDlcRoot
        Me.INDseHours.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseHours, 0)
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = true
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, false)
        Me.INDlcgRoot.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = false
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCostActivityStepPayroll, Me.INDlciAdd, Me.INDlciCostActivityStep, Me.INDlciHours})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(411, 240)
        Me.INDlcgRoot.TextVisible = false
        '
        'INDlciCostActivityStepPayroll
        '
        Me.INDlciCostActivityStepPayroll.AllowHide = false
        Me.INDlciCostActivityStepPayroll.Control = Me.INDsleCostActivityStepPayroll
        Me.INDlciCostActivityStepPayroll.CustomizationFormText = "Cargo"
        Me.INDlciCostActivityStepPayroll.Location = New System.Drawing.Point(0, 62)
        Me.INDlciCostActivityStepPayroll.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostActivityStepPayroll.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostActivityStepPayroll.Name = "INDlciCostActivityStepPayroll"
        Me.INDlciCostActivityStepPayroll.ShowInCustomizationForm = false
        Me.INDlciCostActivityStepPayroll.Size = New System.Drawing.Size(391, 62)
        Me.INDlciCostActivityStepPayroll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCostActivityStepPayroll.Text = "Cargo"
        Me.INDlciCostActivityStepPayroll.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCostActivityStepPayroll.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCostActivityStepPayroll.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlciCostActivityStepPayroll.TextToControlDistance = 5
        '
        'INDlciAdd
        '
        Me.INDlciAdd.Control = Me.INDsbAdd
        Me.INDlciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciAdd.Location = New System.Drawing.Point(0, 184)
        Me.INDlciAdd.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.Name = "INDlciAdd"
        Me.INDlciAdd.Size = New System.Drawing.Size(391, 36)
        Me.INDlciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAdd.TextToControlDistance = 0
        Me.INDlciAdd.TextVisible = false
        '
        'INDlciCostActivityStep
        '
        Me.INDlciCostActivityStep.AllowHide = false
        Me.INDlciCostActivityStep.Control = Me.INDsleCostActivityStep
        Me.INDlciCostActivityStep.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCostActivityStep.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostActivityStep.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostActivityStep.Name = "INDlciCostActivityStep"
        Me.INDlciCostActivityStep.ShowInCustomizationForm = false
        Me.INDlciCostActivityStep.Size = New System.Drawing.Size(391, 62)
        Me.INDlciCostActivityStep.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCostActivityStep.Text = "Paso"
        Me.INDlciCostActivityStep.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCostActivityStep.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCostActivityStep.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciCostActivityStep.TextToControlDistance = 5
        '
        'INDlciHours
        '
        Me.INDlciHours.AllowHide = false
        Me.INDlciHours.Control = Me.INDseHours
        Me.INDlciHours.CustomizationFormText = "Orden"
        Me.INDlciHours.Location = New System.Drawing.Point(0, 124)
        Me.INDlciHours.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciHours.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciHours.Name = "INDlciHours"
        Me.INDlciHours.ShowInCustomizationForm = false
        Me.INDlciHours.Size = New System.Drawing.Size(391, 60)
        Me.INDlciHours.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciHours.Text = "Horas"
        Me.INDlciHours.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciHours.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciHours.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciHours.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'CtrCostActivityStepPayroll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlcRoot)
        Me.Name = "CtrCostActivityStepPayroll"
        Me.Size = New System.Drawing.Size(411, 240)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(false)
        CType(Me.INDsleCostActivityStep.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvCostActivityStep,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleCostActivityStepPayroll.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvPayroll,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDseHours.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciCostActivityStepPayroll,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciCostActivityStep,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciHours,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDsleCostActivityStepPayroll As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvPayroll As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostActivityStepPayroll As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCostActivityStep As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCostActivityStep As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostActivityStep As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents INDseHours As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolCostActivityStepOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCostActivityStepDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvPayrollCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvPayrollName As DevExpress.XtraGrid.Columns.GridColumn
End Class
