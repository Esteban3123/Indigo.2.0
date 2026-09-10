Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupConcepts
    Inherits FormBase

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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteTaxTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDteRate = New DevExpress.XtraEditors.TextEdit()
        Me.INDseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseTimeQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDgleConcept = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTimeQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciRate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDteTaxTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseTimeQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTimeQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(460, 404)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(460, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(460, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.INDteTaxTotalValue)
        Me.LayoutControl1.Controls.Add(Me.INDteRate)
        Me.LayoutControl1.Controls.Add(Me.INDseQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDseTimeQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDgleConcept)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, False)
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(350, 432, 533, 444)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(456, 359)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDteTaxTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteTaxTotalValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteTaxTotalValue, False)
        Me.INDteTaxTotalValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDteTaxTotalValue.EnterMoveNextControl = True
        Me.INDteTaxTotalValue.Location = New System.Drawing.Point(24, 307)
        Me.IndigoTextEdit1.SetMascara(Me.INDteTaxTotalValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteTaxTotalValue.Name = "INDteTaxTotalValue"
        Me.INDteTaxTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteTaxTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTaxTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDteTaxTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDteTaxTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteTaxTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteTaxTotalValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTaxTotalValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteTaxTotalValue.Properties.Mask.EditMask = "c0"
        Me.INDteTaxTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteTaxTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteTaxTotalValue.Size = New System.Drawing.Size(408, 28)
        Me.INDteTaxTotalValue.StyleController = Me.LayoutControl1
        Me.INDteTaxTotalValue.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteTaxTotalValue, 0)
        '
        'INDteRate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteRate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteRate, False)
        Me.INDteRate.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDteRate.EnterMoveNextControl = True
        Me.INDteRate.Location = New System.Drawing.Point(24, 251)
        Me.IndigoTextEdit1.SetMascara(Me.INDteRate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteRate.Name = "INDteRate"
        Me.INDteRate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteRate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteRate.Properties.Appearance.Options.UseBackColor = True
        Me.INDteRate.Properties.Appearance.Options.UseFont = True
        Me.INDteRate.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteRate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteRate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteRate.Properties.Mask.EditMask = "c0"
        Me.INDteRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteRate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteRate.Size = New System.Drawing.Size(408, 28)
        Me.INDteRate.StyleController = Me.LayoutControl1
        Me.INDteRate.TabIndex = 18
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteRate, 0)
        '
        'INDseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantity, False)
        Me.INDseQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseQuantity.EnterMoveNextControl = True
        Me.INDseQuantity.Location = New System.Drawing.Point(24, 195)
        Me.IndigoTextEdit1.SetMascara(Me.INDseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseQuantity.Name = "INDseQuantity"
        Me.INDseQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseQuantity.Properties.IsFloatValue = False
        Me.INDseQuantity.Properties.Mask.EditMask = "N00"
        Me.INDseQuantity.Properties.MaxValue = New Decimal(New Integer() {-559939585, 902409669, 54, 0})
        Me.INDseQuantity.Size = New System.Drawing.Size(408, 28)
        Me.INDseQuantity.StyleController = Me.LayoutControl1
        Me.INDseQuantity.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantity, 0)
        '
        'INDseTimeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseTimeQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseTimeQuantity, False)
        Me.INDseTimeQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseTimeQuantity.EnterMoveNextControl = True
        Me.INDseTimeQuantity.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDseTimeQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseTimeQuantity.Name = "INDseTimeQuantity"
        Me.INDseTimeQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseTimeQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseTimeQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseTimeQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseTimeQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseTimeQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseTimeQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseTimeQuantity.Properties.IsFloatValue = False
        Me.INDseTimeQuantity.Properties.Mask.EditMask = "N00"
        Me.INDseTimeQuantity.Properties.MaxValue = New Decimal(New Integer() {1661992959, 1808227885, 5, 0})
        Me.INDseTimeQuantity.Size = New System.Drawing.Size(408, 28)
        Me.INDseTimeQuantity.StyleController = Me.LayoutControl1
        Me.INDseTimeQuantity.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseTimeQuantity, 0)
        '
        'INDgleConcept
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleConcept, False)
        Me.INDgleConcept.Location = New System.Drawing.Point(24, 83)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleConcept.Name = "INDgleConcept"
        Me.INDgleConcept.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleConcept.Properties.Appearance.Options.UseFont = True
        Me.INDgleConcept.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleConcept.Properties.DisplayMember = "Item2"
        Me.INDgleConcept.Properties.NullText = ""
        Me.INDgleConcept.Properties.ValueMember = "Item1"
        Me.INDgleConcept.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleConcept.Size = New System.Drawing.Size(408, 28)
        Me.INDgleConcept.StyleController = Me.LayoutControl1
        Me.INDgleConcept.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleConcept, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "Agregar Producto"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(456, 359)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup2.CustomizationFormText = "Datos Principales"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDlciTimeQuantity, Me.LayoutControlItem3, Me.INDlciRate, Me.INDlciTotalValue})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(436, 339)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgleConcept
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(412, 56)
        Me.LayoutControlItem2.Text = "Concepto de instalación o fijación de avisos, carteles o afiches"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(408, 21)
        '
        'INDlciTimeQuantity
        '
        Me.INDlciTimeQuantity.Control = Me.INDseTimeQuantity
        Me.INDlciTimeQuantity.Location = New System.Drawing.Point(0, 56)
        Me.INDlciTimeQuantity.Name = "INDlciTimeQuantity"
        Me.INDlciTimeQuantity.Size = New System.Drawing.Size(412, 56)
        Me.INDlciTimeQuantity.Text = "Cantidad de Tiempo"
        Me.INDlciTimeQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTimeQuantity.TextSize = New System.Drawing.Size(408, 21)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDseQuantity
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 112)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(412, 56)
        Me.LayoutControlItem3.Text = "Cantidad"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(408, 21)
        '
        'INDlciRate
        '
        Me.INDlciRate.Control = Me.INDteRate
        Me.INDlciRate.Location = New System.Drawing.Point(0, 168)
        Me.INDlciRate.Name = "INDlciRate"
        Me.INDlciRate.Size = New System.Drawing.Size(412, 56)
        Me.INDlciRate.Text = "Tarifa"
        Me.INDlciRate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciRate.TextSize = New System.Drawing.Size(408, 21)
        '
        'INDlciTotalValue
        '
        Me.INDlciTotalValue.Control = Me.INDteTaxTotalValue
        Me.INDlciTotalValue.Location = New System.Drawing.Point(0, 224)
        Me.INDlciTotalValue.Name = "INDlciTotalValue"
        Me.INDlciTotalValue.Size = New System.Drawing.Size(412, 56)
        Me.INDlciTotalValue.Text = "Valor Total Impuesto"
        Me.INDlciTotalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTotalValue.TextSize = New System.Drawing.Size(408, 21)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 366)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(456, 36)
        Me.PanelControl1.TabIndex = 2
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(452, 32)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "BatchCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 401
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo"
        Me.GridColumn2.FieldName = "Type"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 991
        '
        'FrmPopupConcepts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 522)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupConcepts"
        Me.Opacity = 1.0R
        Me.Text = "Agregar Detalle de Impuesto Menor"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDteTaxTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseTimeQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTimeQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup

    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDteTaxTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteRate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseTimeQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDgleConcept As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciTimeQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciRate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
