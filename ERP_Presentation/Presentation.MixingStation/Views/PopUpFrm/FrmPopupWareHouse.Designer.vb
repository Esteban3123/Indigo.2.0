Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupWareHouse
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
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleItemCode = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDsleuAttentionCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleDescription = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGleType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleItemCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleuAttentionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(666, 401)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(666, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(666, 130)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDsleItemCode)
        Me.INDlyRoot.Controls.Add(Me.INDsleDescription)
        Me.INDlyRoot.Controls.Add(Me.INDGleType)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(712, 105, 574, 569)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(462, 354)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDsleItemCode
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleItemCode, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleItemCode, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleItemCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleItemCode, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleItemCode, False)
        Me.INDsleItemCode.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleItemCode, False)
        Me.INDsleItemCode.Location = New System.Drawing.Point(24, 139)
        Me.INDsleItemCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleItemCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleItemCode.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDsleItemCode.Name = "INDsleItemCode"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleItemCode, False)
        Me.INDsleItemCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleItemCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleItemCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleItemCode.Properties.Appearance.Options.UseFont = True
        Me.INDsleItemCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleItemCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleItemCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleItemCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleItemCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleItemCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleItemCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleItemCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleItemCode.Properties.DisplayMember = "Code"
        Me.INDsleItemCode.Properties.NullText = ""
        Me.INDsleItemCode.Properties.PopupSizeable = False
        Me.INDsleItemCode.Properties.PopupView = Me.INDsleuAttentionCenter
        Me.INDsleItemCode.Properties.ShowFooter = False
        Me.INDsleItemCode.Properties.ValueMember = "Code"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleItemCode, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleItemCode, True)
        Me.INDsleItemCode.Size = New System.Drawing.Size(386, 28)
        Me.INDsleItemCode.StyleController = Me.INDlyRoot
        Me.INDsleItemCode.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleItemCode, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleItemCode, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleItemCode, "{0} - {1}")
        Me.INDsleItemCode.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleItemCode, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleItemCode, False)
        '
        'INDsleuAttentionCenter
        '
        Me.INDsleuAttentionCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDsleuAttentionCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDsleuAttentionCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDsleuAttentionCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDsleuAttentionCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleuAttentionCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDsleuAttentionCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleuAttentionCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDsleuAttentionCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDsleuAttentionCenter.Appearance.Row.Options.UseFont = True
        Me.INDsleuAttentionCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDsleuAttentionCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDsleuAttentionCenter.Name = "INDsleuAttentionCenter"
        Me.INDsleuAttentionCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDsleuAttentionCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDsleuAttentionCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDsleuAttentionCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDsleuAttentionCenter.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.MaxWidth = 100
        Me.GridColumn1.MinWidth = 100
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 100
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'INDsleDescription
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleDescription, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleDescription, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleDescription, False)
        Me.INDsleDescription.Enabled = False
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleDescription, False)
        Me.INDsleDescription.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleDescription, False)
        Me.INDsleDescription.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleDescription.Name = "INDsleDescription"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleDescription, False)
        Me.INDsleDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleDescription.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleDescription.Properties.Appearance.Options.UseFont = True
        Me.INDsleDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleDescription.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleDescription.Properties.DisplayMember = "MUNNOMBRE"
        Me.INDsleDescription.Properties.NullText = ""
        Me.INDsleDescription.Properties.PopupSizeable = False
        Me.INDsleDescription.Properties.PopupView = Me.GridView2
        Me.INDsleDescription.Properties.ShowFooter = False
        Me.INDsleDescription.Properties.ValueMember = "DEPMUNCOD"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleDescription, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleDescription, True)
        Me.INDsleDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDsleDescription.StyleController = Me.INDlyRoot
        Me.INDsleDescription.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleDescription, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleDescription, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleDescription, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleDescription, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleDescription, False)
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
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'INDGleType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleType, False)
        Me.INDGleType.EnterMoveNextControl = True
        Me.INDGleType.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleType.Name = "INDGleType"
        Me.INDGleType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleType.Properties.Appearance.Options.UseFont = True
        Me.INDGleType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleType.Properties.DisplayMember = "Item2"
        Me.INDGleType.Properties.NullText = ""
        Me.INDGleType.Properties.PopupView = Me.GridLookUpEdit2View
        Me.INDGleType.Properties.ValueMember = "Item1"
        Me.INDGleType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleType.StyleController = Me.INDlyRoot
        Me.INDGleType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleType, 0)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn21})
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Tipo Almacén"
        Me.GridColumn21.FieldName = "Item2"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 0
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
        Me.LayoutControlGroup1.CustomizationFormText = "Agregar Producto"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgProduct})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(462, 354)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgProduct
        '
        Me.INDLcgProduct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProduct.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProduct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProduct.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProduct, False)
        Me.INDLcgProduct.CustomizationFormText = "Producto"
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDescription, Me.INDlciType})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(442, 334)
        Me.INDLcgProduct.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlyItemCode.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemCode.Control = Me.INDsleItemCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(418, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlyItemDescription.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemDescription.Control = Me.INDsleDescription
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(418, 161)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Descripción"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'INDlciType
        '
        Me.INDlciType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlciType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciType.Control = Me.INDGleType
        Me.INDlciType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciType.CustomizationFormText = "Tipo de almacén"
        Me.INDlciType.Location = New System.Drawing.Point(0, 0)
        Me.INDlciType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciType.Name = "INDlciType"
        Me.INDlciType.ShowInCustomizationForm = False
        Me.INDlciType.Size = New System.Drawing.Size(418, 60)
        Me.INDlciType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciType.Text = "Tipo de almacén"
        Me.INDlciType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciType.TextToControlDistance = 5
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAdd)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(202, 361)
        Me.INDpanelButtons.MaximumSize = New System.Drawing.Size(0, 38)
        Me.INDpanelButtons.MinimumSize = New System.Drawing.Size(0, 38)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(462, 38)
        Me.INDpanelButtons.TabIndex = 1
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAdd.Appearance.Options.UseFont = True
        Me.INDbtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAdd, True)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(458, 34)
        Me.INDbtnAdd.TabIndex = 4
        Me.INDbtnAdd.Text = "Agregar"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 392)
        Me.CtrNavigationControlPanel1.TabIndex = 3
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Opción"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'FrmPopupWareHouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 536)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupWareHouse"
        Me.Opacity = 1.0R
        Me.Text = "Almacenes"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDsleItemCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleuAttentionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleItemCode As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDsleuAttentionCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleDescription As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDGleType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciType As DevExpress.XtraLayout.LayoutControlItem
End Class
