<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostActivityStepAddictionalCost
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
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleCostActivityStep = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCostActivityStep = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCostActivityStepOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCostActivityStepDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDseValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCostActivityStep = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDmeDescription.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleCostActivityStep.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvCostActivityStep,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDseValue.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciCostActivityStep,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciValue,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciDescription,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDmeDescription)
        Me.INDlcRoot.Controls.Add(Me.INDsleCostActivityStep)
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Controls.Add(Me.INDseValue)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(411, 272)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, false)
        Me.INDmeDescription.Location = New System.Drawing.Point(12, 94)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = true
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = true
        Me.INDmeDescription.Size = New System.Drawing.Size(387, 70)
        Me.INDmeDescription.StyleController = Me.INDlcRoot
        Me.INDmeDescription.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
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
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 228)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDseValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseValue, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseValue, true)
        Me.INDseValue.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseValue.EnterMoveNextControl = true
        Me.INDseValue.Location = New System.Drawing.Point(12, 194)
        Me.IndigoTextEdit1.SetMascara(Me.INDseValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDseValue.Name = "INDseValue"
        Me.INDseValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseValue.Properties.Appearance.Options.UseBackColor = true
        Me.INDseValue.Properties.Appearance.Options.UseFont = true
        Me.INDseValue.Properties.Appearance.Options.UseTextOptions = true
        Me.INDseValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDseValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDseValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDseValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseValue.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDseValue.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDseValue.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDseValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseValue.Properties.Mask.EditMask = "c"
        Me.INDseValue.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDseValue.Properties.MaxLength = 9
        Me.INDseValue.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseValue.Size = New System.Drawing.Size(386, 28)
        Me.INDseValue.StyleController = Me.INDlcRoot
        Me.INDseValue.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseValue, 0)
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
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciAdd, Me.INDlciCostActivityStep, Me.INDlciValue, Me.INDlciDescription})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(411, 272)
        Me.INDlcgRoot.TextVisible = false
        '
        'INDlciAdd
        '
        Me.INDlciAdd.Control = Me.INDsbAdd
        Me.INDlciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciAdd.Location = New System.Drawing.Point(0, 216)
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
        'INDlciValue
        '
        Me.INDlciValue.AllowHide = false
        Me.INDlciValue.Control = Me.INDseValue
        Me.INDlciValue.CustomizationFormText = "Costo"
        Me.INDlciValue.Location = New System.Drawing.Point(0, 156)
        Me.INDlciValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciValue.Name = "INDlciValue"
        Me.INDlciValue.ShowInCustomizationForm = false
        Me.INDlciValue.Size = New System.Drawing.Size(391, 60)
        Me.INDlciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValue.Text = "Costo"
        Me.INDlciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciValue.TextToControlDistance = 5
        '
        'INDlciDescription
        '
        Me.INDlciDescription.Control = Me.INDmeDescription
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 62)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.Size = New System.Drawing.Size(391, 94)
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDescription.TextSize = New System.Drawing.Size(75, 17)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'CtrCostActivityStepAddictionalCost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlcRoot)
        Me.Name = "CtrCostActivityStepAddictionalCost"
        Me.Size = New System.Drawing.Size(411, 272)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(false)
        CType(Me.INDmeDescription.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleCostActivityStep.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvCostActivityStep,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDseValue.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciCostActivityStep,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciValue,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciDescription,System.ComponentModel.ISupportInitialize).EndInit
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
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCostActivityStep As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCostActivityStep As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostActivityStep As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents INDseValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolCostActivityStepOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCostActivityStepDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
End Class
