Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDeliverToService
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDsleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCentAtencion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvFunctionalUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCentAtencion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(447, 226)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(447, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(447, 98)
        '
        'INDsleCareCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCareCenter, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCareCenter, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCareCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCareCenter, False)
        Me.INDsleCareCenter.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCareCenter, False)
        Me.INDsleCareCenter.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCareCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCareCenter.Name = "INDsleCareCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCareCenter, False)
        Me.INDsleCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleCareCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCareCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleCareCenter.Properties.NullText = ""
        Me.INDsleCareCenter.Properties.PopupSizeable = False
        Me.INDsleCareCenter.Properties.PopupView = Me.INDGvCentAtencion
        Me.INDsleCareCenter.Properties.ShowClearButton = False
        Me.INDsleCareCenter.Properties.ShowFooter = False
        Me.INDsleCareCenter.Properties.ValueMember = "CODCENATE"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCareCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCareCenter, True)
        Me.INDsleCareCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCareCenter.StyleController = Me.INDlyRoot
        Me.INDsleCareCenter.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCareCenter, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCareCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCareCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCareCenter, False)
        '
        'INDGvCentAtencion
        '
        Me.INDGvCentAtencion.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCentAtencion.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCentAtencion.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCentAtencion.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCentAtencion.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvCentAtencion.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCentAtencion.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCentAtencion.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCentAtencion.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCentAtencion.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCentAtencion.Appearance.Row.Options.UseFont = True
        Me.INDGvCentAtencion.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25})
        Me.INDGvCentAtencion.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCentAtencion.Name = "INDGvCentAtencion"
        Me.INDGvCentAtencion.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCentAtencion.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCentAtencion.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCentAtencion.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCentAtencion.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCentAtencion, False)
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Código"
        Me.GridColumn24.FieldName = "CODCENATE"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 0
        Me.GridColumn24.Width = 143
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Nombre"
        Me.GridColumn25.FieldName = "NOMCENATE"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 1
        Me.GridColumn25.Width = 277
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDsleFunctionalUnit)
        Me.INDlyRoot.Controls.Add(Me.INDsleCareCenter)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(443, 177)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDsleFunctionalUnit
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFunctionalUnit, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFunctionalUnit, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFunctionalUnit.Name = "INDsleFunctionalUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDsleFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFunctionalUnit.Properties.DisplayMember = "CodeDescription"
        Me.INDsleFunctionalUnit.Properties.NullText = ""
        Me.INDsleFunctionalUnit.Properties.PopupSizeable = False
        Me.INDsleFunctionalUnit.Properties.PopupView = Me.INDGvFunctionalUnit
        Me.INDsleFunctionalUnit.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDsleFunctionalUnit.Properties.ShowClearButton = False
        Me.INDsleFunctionalUnit.Properties.ShowFooter = False
        Me.INDsleFunctionalUnit.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFunctionalUnit, True)
        Me.INDsleFunctionalUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFunctionalUnit.StyleController = Me.INDlyRoot
        Me.INDsleFunctionalUnit.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFunctionalUnit, "523")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFunctionalUnit, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFunctionalUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFunctionalUnit, False)
        '
        'INDGvFunctionalUnit
        '
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvFunctionalUnit.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFunctionalUnit.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFunctionalUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFunctionalUnit.Appearance.Row.Options.UseFont = True
        Me.INDGvFunctionalUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn88, Me.GridColumn89, Me.GridColumn28})
        Me.INDGvFunctionalUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFunctionalUnit.Name = "INDGvFunctionalUnit"
        Me.INDGvFunctionalUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFunctionalUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFunctionalUnit.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFunctionalUnit.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFunctionalUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFunctionalUnit, False)
        '
        'GridColumn88
        '
        Me.GridColumn88.Caption = "Código"
        Me.GridColumn88.FieldName = "Codigo"
        Me.GridColumn88.Name = "GridColumn88"
        Me.GridColumn88.Visible = True
        Me.GridColumn88.VisibleIndex = 0
        Me.GridColumn88.Width = 372
        '
        'GridColumn89
        '
        Me.GridColumn89.Caption = "Descripción"
        Me.GridColumn89.FieldName = "Descripcion"
        Me.GridColumn89.Name = "GridColumn89"
        Me.GridColumn89.Visible = True
        Me.GridColumn89.VisibleIndex = 1
        Me.GridColumn89.Width = 586
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Tipo"
        Me.GridColumn28.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn28.FieldName = "UnitType"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 2
        Me.GridColumn28.Width = 434
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Urgencias", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hospitalizacion", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Apoyo Dx", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Apoyo Terapeutico", CType(4, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Intensivo Adulto", CType(5, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Intermedio Adulto", CType(6, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Intensivo Pediatrica", CType(7, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Intermedio Pediatrica", CType(8, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Intensivo Neonatal", CType(9, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Intermedio Neonatal", CType(10, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidades de Cuidado Basico Neonatal", CType(11, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidad Renal", CType(12, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidad Oncologica", CType(13, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidad Medicina Nuclear", CType(14, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Consulta Externa", CType(15, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidad Mental", CType(16, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidad de Quemados", CType(17, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unidad de Cuidado Paliativo", CType(18, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cirugia", CType(19, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Laboratorio", CType(20, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cardiologia No Invasiva", CType(21, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cardiologia Invasiva", CType(22, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gineco-Obstetricia", CType(23, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Consulta Externa - Gineco-Obstetricia", CType(24, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Otras", CType(25, Byte), -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCareCenter, Me.INDlyItemFunctionalUnit})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(443, 177)
        Me.Root.TextVisible = False
        '
        'INDlyItemCareCenter
        '
        Me.INDlyItemCareCenter.Control = Me.INDsleCareCenter
        Me.INDlyItemCareCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCareCenter.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCareCenter.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCareCenter.Name = "INDlyItemCareCenter"
        Me.INDlyItemCareCenter.Size = New System.Drawing.Size(423, 60)
        Me.INDlyItemCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCareCenter.Text = "Centro Atención"
        Me.INDlyItemCareCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCareCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCareCenter.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCareCenter.TextToControlDistance = 5
        '
        'INDlyItemFunctionalUnit
        '
        Me.INDlyItemFunctionalUnit.Control = Me.INDsleFunctionalUnit
        Me.INDlyItemFunctionalUnit.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemFunctionalUnit.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFunctionalUnit.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFunctionalUnit.Name = "INDlyItemFunctionalUnit"
        Me.INDlyItemFunctionalUnit.Size = New System.Drawing.Size(423, 97)
        Me.INDlyItemFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFunctionalUnit.Text = "Unidad Funcional"
        Me.INDlyItemFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFunctionalUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFunctionalUnit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFunctionalUnit.TextToControlDistance = 5
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAccept.Appearance.Options.UseFont = True
        Me.INDbtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, True)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(439, 36)
        Me.INDbtnAccept.TabIndex = 0
        Me.INDbtnAccept.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAccept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 184)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(443, 40)
        Me.PanelControl1.TabIndex = 1
        '
        'FrmDeliverToService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 348)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDeliverToService"
        Me.Opacity = 1.0R
        Me.Text = "Entrega al Servicio"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCentAtencion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCentAtencion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvFunctionalUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDlyItemFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
End Class
