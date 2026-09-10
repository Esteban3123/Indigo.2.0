Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupAttentionCenterDetail
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
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleProductionLine = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDtxtCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleCenterAttention = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDsleuAttentionCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDsleLocation = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemProductType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleProductionLine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCenterAttention.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleuAttentionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemProductType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDPanelControlBase.Size = New System.Drawing.Size(666, 414)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(666, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(666, 98)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDsleProductionLine)
        Me.INDlyRoot.Controls.Add(Me.INDtxtCode)
        Me.INDlyRoot.Controls.Add(Me.INDsleCenterAttention)
        Me.INDlyRoot.Controls.Add(Me.INDsleLocation)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(462, 367)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDsleProductionLine
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleProductionLine, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleProductionLine, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleProductionLine, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleProductionLine, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleProductionLine, False)
        Me.INDsleProductionLine.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleProductionLine, False)
        Me.INDsleProductionLine.Location = New System.Drawing.Point(24, 265)
        Me.INDsleProductionLine.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleProductionLine, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleProductionLine.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDsleProductionLine.Name = "INDsleProductionLine"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleProductionLine, False)
        Me.INDsleProductionLine.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleProductionLine.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleProductionLine.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleProductionLine.Properties.Appearance.Options.UseFont = True
        Me.INDsleProductionLine.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleProductionLine.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleProductionLine.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleProductionLine.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleProductionLine.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleProductionLine.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleProductionLine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleProductionLine.Properties.DisplayMember = "Name"
        Me.INDsleProductionLine.Properties.NullText = ""
        Me.INDsleProductionLine.Properties.PopupSizeable = False
        Me.INDsleProductionLine.Properties.PopupView = Me.GridView1
        Me.INDsleProductionLine.Properties.ShowFooter = False
        Me.INDsleProductionLine.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleProductionLine, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleProductionLine, True)
        Me.INDsleProductionLine.Size = New System.Drawing.Size(386, 28)
        Me.INDsleProductionLine.StyleController = Me.INDlyRoot
        Me.INDsleProductionLine.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleProductionLine, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleProductionLine, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleProductionLine, "{0} - {1}")
        Me.INDsleProductionLine.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleProductionLine, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'INDtxtCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCode, False)
        Me.INDtxtCode.Enabled = False
        Me.INDtxtCode.EnterMoveNextControl = True
        Me.INDtxtCode.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCode.Name = "INDtxtCode"
        Me.INDtxtCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCode.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCode.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtCode.StyleController = Me.INDlyRoot
        Me.INDtxtCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCode, 0)
        '
        'INDsleCenterAttention
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCenterAttention, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCenterAttention, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCenterAttention, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCenterAttention, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCenterAttention, False)
        Me.INDsleCenterAttention.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCenterAttention, False)
        Me.INDsleCenterAttention.Location = New System.Drawing.Point(24, 85)
        Me.INDsleCenterAttention.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCenterAttention, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCenterAttention.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDsleCenterAttention.Name = "INDsleCenterAttention"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCenterAttention, False)
        Me.INDsleCenterAttention.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleCenterAttention.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCenterAttention.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCenterAttention.Properties.Appearance.Options.UseFont = True
        Me.INDsleCenterAttention.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCenterAttention.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCenterAttention.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCenterAttention.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCenterAttention.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCenterAttention.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCenterAttention.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCenterAttention.Properties.DisplayMember = "NOMCENATE"
        Me.INDsleCenterAttention.Properties.NullText = ""
        Me.INDsleCenterAttention.Properties.PopupSizeable = False
        Me.INDsleCenterAttention.Properties.PopupView = Me.INDsleuAttentionCenter
        Me.INDsleCenterAttention.Properties.ShowFooter = False
        Me.INDsleCenterAttention.Properties.ValueMember = "CODCENATE"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCenterAttention, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCenterAttention, True)
        Me.INDsleCenterAttention.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCenterAttention.StyleController = Me.INDlyRoot
        Me.INDsleCenterAttention.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCenterAttention, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCenterAttention, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCenterAttention, "{0} - {1}")
        Me.INDsleCenterAttention.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCenterAttention, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCenterAttention, False)
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
        Me.INDsleuAttentionCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn4})
        Me.INDsleuAttentionCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDsleuAttentionCenter.Name = "INDsleuAttentionCenter"
        Me.INDsleuAttentionCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDsleuAttentionCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDsleuAttentionCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDsleuAttentionCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDsleuAttentionCenter.OptionsView.ShowGroupPanel = False
        '
        'INDsleLocation
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleLocation, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleLocation, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleLocation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleLocation, False)
        Me.INDsleLocation.Enabled = False
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleLocation, False)
        Me.INDsleLocation.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleLocation, False)
        Me.INDsleLocation.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleLocation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleLocation.Name = "INDsleLocation"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleLocation, False)
        Me.INDsleLocation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleLocation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleLocation.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleLocation.Properties.Appearance.Options.UseFont = True
        Me.INDsleLocation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleLocation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleLocation.Properties.DisplayMember = "MUNNOMBRE"
        Me.INDsleLocation.Properties.NullText = ""
        Me.INDsleLocation.Properties.PopupSizeable = False
        Me.INDsleLocation.Properties.PopupView = Me.GridView2
        Me.INDsleLocation.Properties.ShowFooter = False
        Me.INDsleLocation.Properties.ValueMember = "DEPMUNCOD"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleLocation, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleLocation, True)
        Me.INDsleLocation.Size = New System.Drawing.Size(386, 28)
        Me.INDsleLocation.StyleController = Me.INDlyRoot
        Me.INDsleLocation.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleLocation, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleLocation, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleLocation, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleLocation, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleLocation, False)
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
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(462, 367)
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
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemProducts, Me.INDlyItemProductType, Me.INDlyItemCode, Me.LayoutControlItem1})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(442, 347)
        Me.INDLcgProduct.Text = "Datos Principales"
        '
        'INDlyItemProducts
        '
        Me.INDlyItemProducts.AllowHide = False
        Me.INDlyItemProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlyItemProducts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemProducts.Control = Me.INDsleCenterAttention
        Me.INDlyItemProducts.CustomizationFormText = "Producto"
        Me.INDlyItemProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemProducts.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemProducts.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemProducts.Name = "INDlyItemProducts"
        Me.INDlyItemProducts.ShowInCustomizationForm = False
        Me.INDlyItemProducts.Size = New System.Drawing.Size(418, 60)
        Me.INDlyItemProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemProducts.Text = "Centro de Atención"
        Me.INDlyItemProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemProducts.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemProducts.TextToControlDistance = 5
        '
        'INDlyItemProductType
        '
        Me.INDlyItemProductType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlyItemProductType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemProductType.Control = Me.INDsleLocation
        Me.INDlyItemProductType.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemProductType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemProductType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemProductType.Name = "INDlyItemProductType"
        Me.INDlyItemProductType.Size = New System.Drawing.Size(418, 60)
        Me.INDlyItemProductType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemProductType.Text = "Ubicación"
        Me.INDlyItemProductType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemProductType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemProductType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemProductType.TextToControlDistance = 5
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlyItemCode.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemCode.Control = Me.INDtxtCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(418, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AllowHide = False
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.Control = Me.INDsleProductionLine
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.ShowInCustomizationForm = False
        Me.LayoutControlItem1.Size = New System.Drawing.Size(418, 108)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Línea de Producción"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAdd)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(202, 374)
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
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 405)
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
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.MinWidth = 10
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 254
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Línea de Producción"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.MinWidth = 10
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 524
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Unidad Funcional"
        Me.GridColumn7.FieldName = "Id_FunctionalUnit.CodeName"
        Me.GridColumn7.MinWidth = 10
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 604
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "CODCENATE"
        Me.GridColumn1.MaxWidth = 100
        Me.GridColumn1.MinWidth = 100
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 100
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Centro de Atención"
        Me.GridColumn2.FieldName = "NOMCENATE"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Código Municipio"
        Me.GridColumn4.FieldName = "DEPMUNCOD"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'FrmPopupAttentionCenterDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 536)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmPopupAttentionCenterDetail"
        Me.Opacity = 1R
        Me.Text = "Asociar centro de atención y línea de producción"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlyRoot.ResumeLayout(false)
        CType(Me.INDsleProductionLine.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDtxtCode.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleCenterAttention.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleuAttentionCenter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleLocation.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemProducts,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemProductType,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDpanelButtons,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDpanelButtons.ResumeLayout(false)
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDlyItemProductType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCenterAttention As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDsleuAttentionCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleProductionLine As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleLocation As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
