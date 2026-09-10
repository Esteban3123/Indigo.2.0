Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmContractDetailNovelty
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleStatus = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleNoveltySource = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdteNoveltyDate = New DevExpress.XtraEditors.DateEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemNoveltyDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNoveltySource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddNovelty = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleNoveltySource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteNoveltyDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteNoveltyDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNoveltyDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNoveltySource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(719, 564)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(719, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(719, 98)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 555)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDsleStatus)
        Me.INDlyRoot.Controls.Add(Me.INDmemoDescription)
        Me.INDlyRoot.Controls.Add(Me.INDtxtName)
        Me.INDlyRoot.Controls.Add(Me.INDsleNoveltySource)
        Me.INDlyRoot.Controls.Add(Me.INDdteNoveltyDate)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(515, 515)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDsleStatus
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleStatus, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleStatus, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleStatus, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleStatus, False)
        Me.INDsleStatus.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleStatus, False)
        Me.INDsleStatus.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleStatus, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleStatus.Name = "INDsleStatus"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleStatus, False)
        Me.INDsleStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleStatus.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleStatus.Properties.Appearance.Options.UseFont = True
        Me.INDsleStatus.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleStatus.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleStatus.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleStatus.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleStatus.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleStatus.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleStatus.Properties.DisplayMember = "Item2"
        Me.INDsleStatus.Properties.NullText = ""
        Me.INDsleStatus.Properties.PopupSizeable = False
        Me.INDsleStatus.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDsleStatus.Properties.ShowFooter = False
        Me.INDsleStatus.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleStatus, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleStatus, True)
        Me.INDsleStatus.Size = New System.Drawing.Size(386, 28)
        Me.INDsleStatus.StyleController = Me.INDlyRoot
        Me.INDsleStatus.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleStatus, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleStatus, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleStatus, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleStatus, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'INDmemoDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoDescription, False)
        Me.INDmemoDescription.Location = New System.Drawing.Point(24, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoDescription.Name = "INDmemoDescription"
        Me.INDmemoDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmemoDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemoDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemoDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemoDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemoDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoDescription.Size = New System.Drawing.Size(386, 120)
        Me.INDmemoDescription.StyleController = Me.INDlyRoot
        Me.INDmemoDescription.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoDescription, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlyRoot
        Me.INDtxtName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDsleNoveltySource
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleNoveltySource, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleNoveltySource, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleNoveltySource, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleNoveltySource, False)
        Me.INDsleNoveltySource.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleNoveltySource, False)
        Me.INDsleNoveltySource.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleNoveltySource, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleNoveltySource.Name = "INDsleNoveltySource"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleNoveltySource, False)
        Me.INDsleNoveltySource.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleNoveltySource.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleNoveltySource.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleNoveltySource.Properties.Appearance.Options.UseFont = True
        Me.INDsleNoveltySource.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleNoveltySource.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleNoveltySource.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleNoveltySource.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleNoveltySource.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleNoveltySource.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleNoveltySource.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleNoveltySource.Properties.DisplayMember = "Item2"
        Me.INDsleNoveltySource.Properties.NullText = ""
        Me.INDsleNoveltySource.Properties.PopupSizeable = False
        Me.INDsleNoveltySource.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleNoveltySource.Properties.ShowFooter = False
        Me.INDsleNoveltySource.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleNoveltySource, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleNoveltySource, True)
        Me.INDsleNoveltySource.Size = New System.Drawing.Size(386, 28)
        Me.INDsleNoveltySource.StyleController = Me.INDlyRoot
        Me.INDsleNoveltySource.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleNoveltySource, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleNoveltySource, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleNoveltySource, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleNoveltySource, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleNoveltySource, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Descripción"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDdteNoveltyDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteNoveltyDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteNoveltyDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteNoveltyDate, False)
        Me.INDdteNoveltyDate.EditValue = Nothing
        Me.INDdteNoveltyDate.EnterMoveNextControl = True
        Me.INDdteNoveltyDate.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteNoveltyDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteNoveltyDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDdteNoveltyDate.Name = "INDdteNoveltyDate"
        Me.INDdteNoveltyDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteNoveltyDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteNoveltyDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteNoveltyDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteNoveltyDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteNoveltyDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteNoveltyDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteNoveltyDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteNoveltyDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteNoveltyDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteNoveltyDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteNoveltyDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDdteNoveltyDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteNoveltyDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDdteNoveltyDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteNoveltyDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteNoveltyDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteNoveltyDate.StyleController = Me.INDlyRoot
        Me.INDdteNoveltyDate.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteNoveltyDate, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(515, 515)
        Me.Root.TextVisible = False
        '
        'INDlygPrincipalInformation
        '
        Me.INDlygPrincipalInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalInformation, False)
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemNoveltyDate, Me.INDlyItemNoveltySource, Me.INDlyItemName, Me.INDlyItemDescription, Me.INDlyItemStatus})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(495, 495)
        Me.INDlygPrincipalInformation.Text = "Información General"
        '
        'INDlyItemNoveltyDate
        '
        Me.INDlyItemNoveltyDate.Control = Me.INDdteNoveltyDate
        Me.INDlyItemNoveltyDate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemNoveltyDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNoveltyDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNoveltyDate.Name = "INDlyItemNoveltyDate"
        Me.INDlyItemNoveltyDate.ShowInCustomizationForm = False
        Me.INDlyItemNoveltyDate.Size = New System.Drawing.Size(471, 60)
        Me.INDlyItemNoveltyDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNoveltyDate.Text = "Fecha"
        Me.INDlyItemNoveltyDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNoveltyDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemNoveltyDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemNoveltyDate.TextToControlDistance = 5
        '
        'INDlyItemNoveltySource
        '
        Me.INDlyItemNoveltySource.Control = Me.INDsleNoveltySource
        Me.INDlyItemNoveltySource.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemNoveltySource.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNoveltySource.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNoveltySource.Name = "INDlyItemNoveltySource"
        Me.INDlyItemNoveltySource.ShowInCustomizationForm = False
        Me.INDlyItemNoveltySource.Size = New System.Drawing.Size(471, 60)
        Me.INDlyItemNoveltySource.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNoveltySource.Text = "Origen"
        Me.INDlyItemNoveltySource.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNoveltySource.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemNoveltySource.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemNoveltySource.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(471, 60)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDmemoDescription
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(471, 196)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Descripción"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'INDlyItemStatus
        '
        Me.INDlyItemStatus.Control = Me.INDsleStatus
        Me.INDlyItemStatus.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemStatus.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStatus.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStatus.Name = "INDlyItemStatus"
        Me.INDlyItemStatus.ShowInCustomizationForm = False
        Me.INDlyItemStatus.Size = New System.Drawing.Size(471, 60)
        Me.INDlyItemStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStatus.Text = "Estado"
        Me.INDlyItemStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemStatus.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemStatus.TextToControlDistance = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddNovelty)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 522)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(515, 40)
        Me.PanelControl1.TabIndex = 1
        '
        'INDbtnAddNovelty
        '
        Me.INDbtnAddNovelty.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddNovelty.Appearance.Options.UseFont = True
        Me.INDbtnAddNovelty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddNovelty.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddNovelty, True)
        Me.INDbtnAddNovelty.Name = "INDbtnAddNovelty"
        Me.INDbtnAddNovelty.Size = New System.Drawing.Size(511, 36)
        Me.INDbtnAddNovelty.TabIndex = 0
        Me.INDbtnAddNovelty.Text = "Agregar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmContractDetailNovelty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 686)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmContractDetailNovelty"
        Me.Opacity = 1.0R
        Me.Text = "Novedades"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDsleStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleNoveltySource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteNoveltyDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteNoveltyDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNoveltyDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNoveltySource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddNovelty As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDmemoDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleNoveltySource As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDdteNoveltyDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemNoveltyDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemNoveltySource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleStatus As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
