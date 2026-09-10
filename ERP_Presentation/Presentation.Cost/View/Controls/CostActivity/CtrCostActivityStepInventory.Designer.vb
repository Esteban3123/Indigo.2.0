<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostActivityStepInventory
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
        Me.INDteMeasurementUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleCostActivityStep = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCostActivityStep = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCostActivityStepOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCostActivityStepDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleCostActivityStepInventory = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvInventory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgvInventoryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvInventoryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCostActivityStepInventory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCostActivityStep = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciMeasurementUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDteMeasurementUnit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleCostActivityStep.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvCostActivityStep,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleCostActivityStepInventory.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvInventory,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDseQuantity.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciCostActivityStepInventory,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciCostActivityStep,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciQuantity,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciMeasurementUnit,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDteMeasurementUnit)
        Me.INDlcRoot.Controls.Add(Me.INDsleCostActivityStep)
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Controls.Add(Me.INDsleCostActivityStepInventory)
        Me.INDlcRoot.Controls.Add(Me.INDseQuantity)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(411, 305)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDteMeasurementUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteMeasurementUnit, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteMeasurementUnit, false)
        Me.INDteMeasurementUnit.Location = New System.Drawing.Point(12, 162)
        Me.IndigoTextEdit1.SetMascara(Me.INDteMeasurementUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteMeasurementUnit.Name = "INDteMeasurementUnit"
        Me.INDteMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteMeasurementUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDteMeasurementUnit.Properties.Appearance.Options.UseBackColor = true
        Me.INDteMeasurementUnit.Properties.Appearance.Options.UseFont = true
        Me.INDteMeasurementUnit.Properties.ReadOnly = true
        Me.INDteMeasurementUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDteMeasurementUnit.StyleController = Me.INDlcRoot
        Me.INDteMeasurementUnit.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteMeasurementUnit, 0)
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
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 258)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDsleCostActivityStepInventory
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCostActivityStepInventory, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCostActivityStepInventory, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.INDsleCostActivityStepInventory.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.INDsleCostActivityStepInventory.Location = New System.Drawing.Point(12, 100)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCostActivityStepInventory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCostActivityStepInventory.Name = "INDsleCostActivityStepInventory"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.INDsleCostActivityStepInventory.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCostActivityStepInventory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleCostActivityStepInventory.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleCostActivityStepInventory.Properties.Appearance.Options.UseFont = true
        Me.INDsleCostActivityStepInventory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCostActivityStepInventory.Properties.DisplayMember = "CodeName"
        Me.INDsleCostActivityStepInventory.Properties.NullText = ""
        Me.INDsleCostActivityStepInventory.Properties.PopupSizeable = false
        Me.INDsleCostActivityStepInventory.Properties.ShowFooter = false
        Me.INDsleCostActivityStepInventory.Properties.ValueMember = "Id"
        Me.INDsleCostActivityStepInventory.Properties.View = Me.INDgvInventory
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCostActivityStepInventory, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCostActivityStepInventory, true)
        Me.INDsleCostActivityStepInventory.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCostActivityStepInventory.StyleController = Me.INDlcRoot
        Me.INDsleCostActivityStepInventory.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCostActivityStepInventory, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCostActivityStepInventory, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCostActivityStepInventory, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCostActivityStepInventory, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCostActivityStepInventory, false)
        '
        'INDgvInventory
        '
        Me.INDgvInventory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvInventory.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseForeColor = true
        Me.INDgvInventory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvInventory.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvInventory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvInventory.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvInventory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvInventory.Appearance.Row.Options.UseFont = true
        Me.INDgvInventory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgvInventoryCode, Me.INDgvInventoryName})
        Me.INDgvInventory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvInventory.Name = "INDgvInventory"
        Me.INDgvInventory.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDgvInventory.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvInventory.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvInventory.OptionsView.ShowAutoFilterRow = true
        Me.INDgvInventory.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvInventory, false)
        '
        'INDgvInventoryCode
        '
        Me.INDgvInventoryCode.Caption = "Código"
        Me.INDgvInventoryCode.FieldName = "Code"
        Me.INDgvInventoryCode.Name = "INDgvInventoryCode"
        Me.INDgvInventoryCode.Visible = true
        Me.INDgvInventoryCode.VisibleIndex = 0
        '
        'INDgvInventoryName
        '
        Me.INDgvInventoryName.Caption = "Nombre"
        Me.INDgvInventoryName.FieldName = "Name"
        Me.INDgvInventoryName.Name = "INDgvInventoryName"
        Me.INDgvInventoryName.Visible = true
        Me.INDgvInventoryName.VisibleIndex = 1
        '
        'INDseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantity, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantity, true)
        Me.INDseQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseQuantity.EnterMoveNextControl = true
        Me.INDseQuantity.Location = New System.Drawing.Point(12, 224)
        Me.IndigoTextEdit1.SetMascara(Me.INDseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseQuantity.Name = "INDseQuantity"
        Me.INDseQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseQuantity.Properties.Appearance.Options.UseBackColor = true
        Me.INDseQuantity.Properties.Appearance.Options.UseFont = true
        Me.INDseQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDseQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDseQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDseQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseQuantity.Properties.DisplayFormat.FormatString = "f6"
        Me.INDseQuantity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDseQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseQuantity.Properties.Mask.EditMask = "f6"
        Me.INDseQuantity.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDseQuantity.Properties.MaxLength = 24
        Me.INDseQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseQuantity.StyleController = Me.INDlcRoot
        Me.INDseQuantity.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantity, 0)
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
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCostActivityStepInventory, Me.INDlciAdd, Me.INDlciCostActivityStep, Me.INDlciQuantity, Me.INDlciMeasurementUnit})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(411, 305)
        Me.INDlcgRoot.TextVisible = false
        '
        'INDlciCostActivityStepInventory
        '
        Me.INDlciCostActivityStepInventory.AllowHide = false
        Me.INDlciCostActivityStepInventory.Control = Me.INDsleCostActivityStepInventory
        Me.INDlciCostActivityStepInventory.CustomizationFormText = "Grupo de Productos"
        Me.INDlciCostActivityStepInventory.Location = New System.Drawing.Point(0, 62)
        Me.INDlciCostActivityStepInventory.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostActivityStepInventory.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostActivityStepInventory.Name = "INDlciCostActivityStepInventory"
        Me.INDlciCostActivityStepInventory.ShowInCustomizationForm = false
        Me.INDlciCostActivityStepInventory.Size = New System.Drawing.Size(391, 62)
        Me.INDlciCostActivityStepInventory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCostActivityStepInventory.Text = "Grupo de Productos"
        Me.INDlciCostActivityStepInventory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCostActivityStepInventory.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCostActivityStepInventory.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlciCostActivityStepInventory.TextToControlDistance = 5
        '
        'INDlciAdd
        '
        Me.INDlciAdd.Control = Me.INDsbAdd
        Me.INDlciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciAdd.Location = New System.Drawing.Point(0, 246)
        Me.INDlciAdd.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.Name = "INDlciAdd"
        Me.INDlciAdd.Size = New System.Drawing.Size(391, 39)
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
        'INDlciQuantity
        '
        Me.INDlciQuantity.AllowHide = false
        Me.INDlciQuantity.Control = Me.INDseQuantity
        Me.INDlciQuantity.CustomizationFormText = "Cantidad"
        Me.INDlciQuantity.Location = New System.Drawing.Point(0, 186)
        Me.INDlciQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciQuantity.Name = "INDlciQuantity"
        Me.INDlciQuantity.ShowInCustomizationForm = false
        Me.INDlciQuantity.Size = New System.Drawing.Size(391, 60)
        Me.INDlciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciQuantity.Text = "Cantidad"
        Me.INDlciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciQuantity.TextToControlDistance = 5
        '
        'INDlciMeasurementUnit
        '
        Me.INDlciMeasurementUnit.Control = Me.INDteMeasurementUnit
        Me.INDlciMeasurementUnit.Location = New System.Drawing.Point(0, 124)
        Me.INDlciMeasurementUnit.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciMeasurementUnit.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciMeasurementUnit.Name = "INDlciMeasurementUnit"
        Me.INDlciMeasurementUnit.Size = New System.Drawing.Size(391, 62)
        Me.INDlciMeasurementUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciMeasurementUnit.Text = "Unidad de Medida"
        Me.INDlciMeasurementUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciMeasurementUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciMeasurementUnit.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlciMeasurementUnit.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'CtrCostActivityStepInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlcRoot)
        Me.Name = "CtrCostActivityStepInventory"
        Me.Size = New System.Drawing.Size(411, 305)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(false)
        CType(Me.INDteMeasurementUnit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleCostActivityStep.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvCostActivityStep,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleCostActivityStepInventory.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvInventory,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDseQuantity.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciCostActivityStepInventory,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciCostActivityStep,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciQuantity,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciMeasurementUnit,System.ComponentModel.ISupportInitialize).EndInit
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
    Friend WithEvents INDsleCostActivityStepInventory As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvInventory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostActivityStepInventory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCostActivityStep As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCostActivityStep As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostActivityStep As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents INDseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolCostActivityStepOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCostActivityStepDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvInventoryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvInventoryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDteMeasurementUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciMeasurementUnit As DevExpress.XtraLayout.LayoutControlItem
End Class
