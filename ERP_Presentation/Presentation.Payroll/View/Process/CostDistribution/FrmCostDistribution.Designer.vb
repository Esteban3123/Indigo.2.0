<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostDistribution
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDProcessButton = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgleLastLiquidationDate = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescriptionGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLastLiquidationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgleLastLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLastLiquidationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
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
        Me.LayoutControl1.Controls.Add(Me.INDProcessButton)
        Me.LayoutControl1.Controls.Add(Me.INDgleLastLiquidationDate)
        Me.LayoutControl1.Controls.Add(Me.INDsleGroup)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(4, 9)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(724, 283, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1000, 593)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDProcessButton
        '
        Me.INDProcessButton.Location = New System.Drawing.Point(182, 112)
        Me.INDProcessButton.MaximumSize = New System.Drawing.Size(270, 32)
        Me.INDProcessButton.MinimumSize = New System.Drawing.Size(270, 32)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDProcessButton, False)
        Me.INDProcessButton.Name = "INDProcessButton"
        Me.INDProcessButton.Size = New System.Drawing.Size(270, 32)
        Me.INDProcessButton.StyleController = Me.LayoutControl1
        Me.INDProcessButton.TabIndex = 6
        Me.INDProcessButton.Text = "Procesar"
        '
        'INDgleLastLiquidationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleLastLiquidationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleLastLiquidationDate, False)
        Me.INDgleLastLiquidationDate.Location = New System.Drawing.Point(186, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleLastLiquidationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleLastLiquidationDate.Name = "INDgleLastLiquidationDate"
        Me.INDgleLastLiquidationDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleLastLiquidationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleLastLiquidationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleLastLiquidationDate.Properties.Appearance.Options.UseFont = True
        Me.INDgleLastLiquidationDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleLastLiquidationDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgleLastLiquidationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleLastLiquidationDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleLastLiquidationDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleLastLiquidationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleLastLiquidationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleLastLiquidationDate.Properties.DisplayMember = "Date.Date"
        Me.INDgleLastLiquidationDate.Properties.NullText = ""
        Me.INDgleLastLiquidationDate.Properties.ValueMember = "Date"
        Me.INDgleLastLiquidationDate.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleLastLiquidationDate.Size = New System.Drawing.Size(263, 28)
        Me.INDgleLastLiquidationDate.StyleController = Me.LayoutControl1
        Me.INDgleLastLiquidationDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleLastLiquidationDate, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDate})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.GridLookUpEdit1View.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColDate, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDColDate
        '
        Me.INDColDate.Caption = "Fechas"
        Me.INDColDate.FieldName = "Date"
        Me.INDColDate.Name = "INDColDate"
        Me.INDColDate.Visible = True
        Me.INDColDate.VisibleIndex = 0
        '
        'INDsleGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleGroup, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleGroup, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleGroup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleGroup, False)
        Me.INDsleGroup.Location = New System.Drawing.Point(186, 43)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleGroup.Name = "INDsleGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleGroup, False)
        Me.INDsleGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleGroup.Properties.Appearance.Options.UseFont = True
        Me.INDsleGroup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleGroup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleGroup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleGroup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleGroup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleGroup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleGroup.Properties.DisplayMember = "Descripcion"
        Me.INDsleGroup.Properties.NullText = ""
        Me.INDsleGroup.Properties.PopupSizeable = False
        Me.INDsleGroup.Properties.ShowFooter = False
        Me.INDsleGroup.Properties.ValueMember = "Id"
        Me.INDsleGroup.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleGroup, True)
        Me.INDsleGroup.Size = New System.Drawing.Size(263, 28)
        Me.INDsleGroup.StyleController = Me.LayoutControl1
        Me.INDsleGroup.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleGroup, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleGroup, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleGroup, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeGroup, Me.INDColDescriptionGroup})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDColCodeGroup
        '
        Me.INDColCodeGroup.Caption = "Código"
        Me.INDColCodeGroup.FieldName = "Codigo"
        Me.INDColCodeGroup.Name = "INDColCodeGroup"
        Me.INDColCodeGroup.Visible = True
        Me.INDColCodeGroup.VisibleIndex = 0
        '
        'INDColDescriptionGroup
        '
        Me.INDColDescriptionGroup.Caption = "Nombre"
        Me.INDColDescriptionGroup.FieldName = "Descripcion"
        Me.INDColDescriptionGroup.Name = "INDColDescriptionGroup"
        Me.INDColDescriptionGroup.Visible = True
        Me.INDColDescriptionGroup.VisibleIndex = 1
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1000, 593)
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
        Me.LayoutControlGroup2.CustomizationFormText = "Distribución de Gasto"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGroup, Me.INDlyItemLastLiquidationDate, Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1000, 593)
        Me.LayoutControlGroup2.Text = "Distribución de Gastos"
        '
        'INDlyItemGroup
        '
        Me.INDlyItemGroup.Control = Me.INDsleGroup
        Me.INDlyItemGroup.CustomizationFormText = "Grupo"
        Me.INDlyItemGroup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGroup.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemGroup.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemGroup.Name = "INDlyItemGroup"
        Me.INDlyItemGroup.Size = New System.Drawing.Size(992, 36)
        Me.INDlyItemGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGroup.Text = "Grupo"
        Me.INDlyItemGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemGroup.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemGroup.TextToControlDistance = 12
        '
        'INDlyItemLastLiquidationDate
        '
        Me.INDlyItemLastLiquidationDate.Control = Me.INDgleLastLiquidationDate
        Me.INDlyItemLastLiquidationDate.CustomizationFormText = "Fecha Liquidación"
        Me.INDlyItemLastLiquidationDate.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemLastLiquidationDate.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemLastLiquidationDate.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemLastLiquidationDate.Name = "INDlyItemLastLiquidationDate"
        Me.INDlyItemLastLiquidationDate.Size = New System.Drawing.Size(992, 36)
        Me.INDlyItemLastLiquidationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLastLiquidationDate.Text = "Fecha Liquidación"
        Me.INDlyItemLastLiquidationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLastLiquidationDate.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemLastLiquidationDate.TextToControlDistance = 12
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDProcessButton
        Me.LayoutControlItem1.CustomizationFormText = "Procesar"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(83, 26)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(178, 2, 2, 2)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(992, 478)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmCostDistribution
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmCostDistribution"
        Me.Opacity = 1.0R
        Me.Tag = "583"
        Me.Text = "Distribución de Gastos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgleLastLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLastLiquidationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDgleLastLiquidationDate As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemLastLiquidationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDColCodeGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescriptionGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDProcessButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
