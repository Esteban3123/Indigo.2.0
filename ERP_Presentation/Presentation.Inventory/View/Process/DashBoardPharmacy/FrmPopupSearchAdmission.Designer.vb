Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupSearchAdmission
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPopupSearchAdmission))
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAgree = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleAdmission = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvAdmission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvFunctional = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1271 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1281 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFuncType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAgree = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgPrincipals = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl11 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFunctional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAgree, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPrincipals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 5)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(502, 258)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolBars.Size = New System.Drawing.Size(502, 130)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(502, 130)
        Me.BarraBotones.Visible = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDSbAgree)
        Me.INDlyRoot.Controls.Add(Me.INDsleAdmission)
        Me.INDlyRoot.Controls.Add(Me.INDSleFunctionalUnit)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(712, 105, 574, 569)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(498, 249)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDSbAgree
        '
        Me.INDSbAgree.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbAgree.Appearance.Options.UseFont = True
        Me.INDSbAgree.Location = New System.Drawing.Point(12, 206)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbAgree, True)
        Me.INDSbAgree.Name = "INDSbAgree"
        Me.INDSbAgree.Size = New System.Drawing.Size(474, 31)
        Me.INDSbAgree.StyleController = Me.INDlyRoot
        Me.INDSbAgree.TabIndex = 6
        Me.INDSbAgree.Text = "Aceptar"
        '
        'INDsleAdmission
        '
        Me.IndigoSearchLookUpControl11.SetAppearanceEmbeddedNavigator(Me.INDsleAdmission, AppearanceObject1)
        Me.IndigoSearchLookUpControl11.SetAppearanceTextFindControl(Me.INDsleAdmission, AppearanceObject2)
        Me.IndigoSearchLookUpControl11.SetAppendButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetAutomaticOpenForm(Me.INDsleAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetCancelEditButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetEditButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetEndEditButtonNavigator(Me.INDsleAdmission, False)
        Me.INDsleAdmission.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl11.SetExportButton(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetFirstButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetLastButtonNavigator(Me.INDsleAdmission, False)
        Me.INDsleAdmission.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAdmission.Name = "INDsleAdmission"
        Me.IndigoSearchLookUpControl11.SetNextButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetNextPageButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetOpenForm(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetPopupSizeable(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetPrevButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetPrevPageButtonNavigator(Me.INDsleAdmission, False)
        Me.INDsleAdmission.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAdmission.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAdmission.Properties.Appearance.Options.UseFont = True
        Me.INDsleAdmission.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAdmission.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleAdmission.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAdmission.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAdmission.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAdmission.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAdmission.Properties.DisplayMember = "FullNameAdmission"
        Me.INDsleAdmission.Properties.NullText = ""
        Me.INDsleAdmission.Properties.PopupSizeable = False
        Me.INDsleAdmission.Properties.PopupView = Me.INDGvAdmission
        Me.INDsleAdmission.Properties.ShowClearButton = False
        Me.INDsleAdmission.Properties.ShowFooter = False
        Me.INDsleAdmission.Properties.ValueMember = "AdmissionCode"
        Me.IndigoSearchLookUpControl11.SetRemoveButtonNavigator(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetSaveXmlGrid(Me.INDsleAdmission, True)
        Me.IndigoSearchLookUpControl11.SetShowDeleteButton(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetShowFindButton(Me.INDsleAdmission, False)
        Me.INDsleAdmission.Size = New System.Drawing.Size(450, 28)
        Me.INDsleAdmission.StyleController = Me.INDlyRoot
        Me.INDsleAdmission.TabIndex = 5
        Me.IndigoSearchLookUpControl11.SetTagForm(Me.INDsleAdmission, "")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAdmission, 0)
        Me.IndigoSearchLookUpControl11.SetTextStringFormat(Me.INDsleAdmission, "{0} - {1}")
        Me.IndigoSearchLookUpControl11.SetTxtFindEnterEnabled(Me.INDsleAdmission, False)
        Me.IndigoSearchLookUpControl11.SetUseEmbeddedNavigator(Me.INDsleAdmission, False)
        '
        'INDGvAdmission
        '
        Me.INDGvAdmission.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAdmission.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAdmission.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAdmission.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAdmission.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAdmission.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAdmission.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAdmission.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAdmission.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAdmission.Appearance.Row.Options.UseFont = True
        Me.INDGvAdmission.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn127, Me.GridColumn128, Me.GridColumn1, Me.GridColumn2})
        Me.INDGvAdmission.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvAdmission.Name = "INDGvAdmission"
        Me.INDGvAdmission.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAdmission.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAdmission.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAdmission.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAdmission.OptionsView.ShowGroupPanel = False
        '
        'GridColumn127
        '
        Me.GridColumn127.Caption = "Ingreso"
        Me.GridColumn127.FieldName = "AdmissionCode"
        Me.GridColumn127.Name = "GridColumn127"
        Me.GridColumn127.OptionsColumn.AllowEdit = False
        Me.GridColumn127.OptionsColumn.AllowFocus = False
        Me.GridColumn127.Visible = True
        Me.GridColumn127.VisibleIndex = 0
        '
        'GridColumn128
        '
        Me.GridColumn128.Caption = "Codígo"
        Me.GridColumn128.FieldName = "PatientCode"
        Me.GridColumn128.Name = "GridColumn128"
        Me.GridColumn128.OptionsColumn.AllowEdit = False
        Me.GridColumn128.OptionsColumn.AllowFocus = False
        Me.GridColumn128.Visible = True
        Me.GridColumn128.VisibleIndex = 1
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nombre"
        Me.GridColumn1.FieldName = "PatientName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Cama"
        Me.GridColumn2.FieldName = "BedStay"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'INDSleFunctionalUnit
        '
        Me.IndigoSearchLookUpControl11.SetAppearanceEmbeddedNavigator(Me.INDSleFunctionalUnit, AppearanceObject3)
        Me.IndigoSearchLookUpControl11.SetAppearanceTextFindControl(Me.INDSleFunctionalUnit, AppearanceObject4)
        Me.IndigoSearchLookUpControl11.SetAppendButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetAutomaticOpenForm(Me.INDSleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetCancelEditButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetEditButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetEndEditButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.INDSleFunctionalUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl11.SetExportButton(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetFirstButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetLastButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.INDSleFunctionalUnit.Location = New System.Drawing.Point(24, 147)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleFunctionalUnit.Name = "INDSleFunctionalUnit"
        Me.IndigoSearchLookUpControl11.SetNextButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetNextPageButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetOpenForm(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetPopupSizeable(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetPrevButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetPrevPageButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.INDSleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleFunctionalUnit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleFunctionalUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleFunctionalUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleFunctionalUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleFunctionalUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleFunctionalUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleFunctionalUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleFunctionalUnit.Properties.DisplayMember = "CodeName"
        Me.INDSleFunctionalUnit.Properties.NullText = ""
        Me.INDSleFunctionalUnit.Properties.PopupSizeable = False
        Me.INDSleFunctionalUnit.Properties.PopupView = Me.INDGvFunctional
        Me.INDSleFunctionalUnit.Properties.ShowClearButton = False
        Me.INDSleFunctionalUnit.Properties.ShowFooter = False
        Me.INDSleFunctionalUnit.Properties.ValueMember = "UFUCODIGO"
        Me.IndigoSearchLookUpControl11.SetRemoveButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetSaveXmlGrid(Me.INDSleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl11.SetShowDeleteButton(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetShowFindButton(Me.INDSleFunctionalUnit, True)
        Me.INDSleFunctionalUnit.Size = New System.Drawing.Size(450, 28)
        Me.INDSleFunctionalUnit.StyleController = Me.INDlyRoot
        Me.INDSleFunctionalUnit.TabIndex = 5
        Me.IndigoSearchLookUpControl11.SetTagForm(Me.INDSleFunctionalUnit, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleFunctionalUnit, 0)
        Me.IndigoSearchLookUpControl11.SetTextStringFormat(Me.INDSleFunctionalUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl11.SetTxtFindEnterEnabled(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl11.SetUseEmbeddedNavigator(Me.INDSleFunctionalUnit, False)
        '
        'INDGvFunctional
        '
        Me.INDGvFunctional.ActiveFilterString = "[GridColumn4] = 'Cirugia'"
        Me.INDGvFunctional.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFunctional.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFunctional.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFunctional.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFunctional.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFunctional.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFunctional.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFunctional.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFunctional.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFunctional.Appearance.Row.Options.UseFont = True
        Me.INDGvFunctional.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1271, Me.GridColumn1281, Me.INDColType, Me.INDColFuncType})
        Me.INDGvFunctional.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFunctional.Name = "INDGvFunctional"
        Me.INDGvFunctional.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFunctional.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFunctional.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFunctional.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFunctional.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1271
        '
        Me.GridColumn1271.Caption = "Codígo"
        Me.GridColumn1271.FieldName = "UFUCODIGO"
        Me.GridColumn1271.Name = "GridColumn1271"
        Me.GridColumn1271.OptionsColumn.AllowEdit = False
        Me.GridColumn1271.OptionsColumn.AllowFocus = False
        Me.GridColumn1271.Visible = True
        Me.GridColumn1271.VisibleIndex = 0
        '
        'GridColumn1281
        '
        Me.GridColumn1281.Caption = "Descripción"
        Me.GridColumn1281.FieldName = "UFUDESCRI"
        Me.GridColumn1281.Name = "GridColumn1281"
        Me.GridColumn1281.OptionsColumn.AllowEdit = False
        Me.GridColumn1281.OptionsColumn.AllowFocus = False
        Me.GridColumn1281.Visible = True
        Me.GridColumn1281.VisibleIndex = 1
        '
        'INDColType
        '
        Me.INDColType.Caption = "Tipo"
        Me.INDColType.FieldName = "GridColumn4"
        Me.INDColType.Name = "INDColType"
        Me.INDColType.OptionsColumn.AllowEdit = False
        Me.INDColType.OptionsColumn.AllowFocus = False
        Me.INDColType.UnboundExpression = resources.GetString("INDColType.UnboundExpression")
        Me.INDColType.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColType.Visible = True
        Me.INDColType.VisibleIndex = 2
        '
        'INDColFuncType
        '
        Me.INDColFuncType.Caption = "INDColFuncType"
        Me.INDColFuncType.FieldName = "UFUTIPUNI"
        Me.INDColFuncType.Name = "INDColFuncType"
        Me.INDColFuncType.OptionsColumn.AllowEdit = False
        Me.INDColFuncType.OptionsColumn.AllowFocus = False
        Me.INDColFuncType.OptionsColumn.ShowInCustomizationForm = False
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAgree, Me.INDLcgPrincipals})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(498, 249)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciAgree
        '
        Me.INDLciAgree.Control = Me.INDSbAgree
        Me.INDLciAgree.Location = New System.Drawing.Point(0, 194)
        Me.INDLciAgree.MinSize = New System.Drawing.Size(49, 26)
        Me.INDLciAgree.Name = "INDLciAgree"
        Me.INDLciAgree.Size = New System.Drawing.Size(478, 35)
        Me.INDLciAgree.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAgree.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAgree.TextVisible = False
        '
        'INDLcgPrincipals
        '
        Me.INDLcgPrincipals.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPrincipals.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPrincipals.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPrincipals.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPrincipals.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipals.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPrincipals.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPrincipals.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPrincipals.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipals.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPrincipals.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipals.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPrincipals.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipals.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPrincipals, False)
        Me.INDLcgPrincipals.CustomizationFormText = "Datos Principales"
        Me.INDLcgPrincipals.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliAdmission, Me.INDLciFunctionalUnit})
        Me.INDLcgPrincipals.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgPrincipals.Name = "INDLcgPrincipals"
        Me.INDLcgPrincipals.Size = New System.Drawing.Size(478, 194)
        Me.INDLcgPrincipals.Text = "Datos Principales"
        '
        'INDliAdmission
        '
        Me.INDliAdmission.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.INDliAdmission.AppearanceItemCaption.Options.UseFont = True
        Me.INDliAdmission.Control = Me.INDsleAdmission
        Me.INDliAdmission.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliAdmission.CustomizationFormText = "Ingreso"
        Me.INDliAdmission.Location = New System.Drawing.Point(0, 0)
        Me.INDliAdmission.MinSize = New System.Drawing.Size(1, 60)
        Me.INDliAdmission.Name = "INDliAdmission"
        Me.INDliAdmission.ShowInCustomizationForm = False
        Me.INDliAdmission.Size = New System.Drawing.Size(454, 68)
        Me.INDliAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAdmission.Text = "Ingreso"
        Me.INDliAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAdmission.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliAdmission.TextSize = New System.Drawing.Size(108, 21)
        Me.INDliAdmission.TextToControlDistance = 5
        '
        'INDLciFunctionalUnit
        '
        Me.INDLciFunctionalUnit.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.INDLciFunctionalUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciFunctionalUnit.Control = Me.INDSleFunctionalUnit
        Me.INDLciFunctionalUnit.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciFunctionalUnit.CustomizationFormText = "Unidad Funcional"
        Me.INDLciFunctionalUnit.Location = New System.Drawing.Point(0, 68)
        Me.INDLciFunctionalUnit.MinSize = New System.Drawing.Size(1, 60)
        Me.INDLciFunctionalUnit.Name = "INDLciFunctionalUnit"
        Me.INDLciFunctionalUnit.ShowInCustomizationForm = False
        Me.INDLciFunctionalUnit.Size = New System.Drawing.Size(454, 73)
        Me.INDLciFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFunctionalUnit.Text = "Unidad Funcional"
        Me.INDLciFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFunctionalUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFunctionalUnit.TextSize = New System.Drawing.Size(108, 21)
        Me.INDLciFunctionalUnit.TextToControlDistance = 5
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
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmPopupSearchAdmission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 263)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupSearchAdmission"
        Me.Opacity = 1.0R
        Me.Text = "Observaciones"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDsleAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFunctional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAgree, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPrincipals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDsleAdmission As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl11 As IndigoSearchLookUpControl
    Friend WithEvents INDGvAdmission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbAgree As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvFunctional As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1271 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1281 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciAgree As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgPrincipals As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFuncType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColType As DevExpress.XtraGrid.Columns.GridColumn
End Class
