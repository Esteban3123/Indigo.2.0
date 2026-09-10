Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPatientExternalCareCenter
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
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleGender = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtLastName = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleIdentificationType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnIdentification = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDTxtPatientMobileNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtPatientEmail = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtFunctionalUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtBed = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemIdentification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIdentificationType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLastName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPatientEmail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPatientMobileNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBed = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGender = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDSleGender.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtLastName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleIdentificationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnIdentification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtPatientMobileNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtPatientEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtBed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIdentification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIdentificationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLastName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPatientEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPatientMobileNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 140)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1756, 927)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 10)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ToolBars.Size = New System.Drawing.Size(1756, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.BarraBotones.Size = New System.Drawing.Size(1756, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 12)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 913)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDSleGender)
        Me.INDlyRoot.Controls.Add(Me.INDtxtLastName)
        Me.INDlyRoot.Controls.Add(Me.INDsleIdentificationType)
        Me.INDlyRoot.Controls.Add(Me.INDtxtName)
        Me.INDlyRoot.Controls.Add(Me.INDbtnIdentification)
        Me.INDlyRoot.Controls.Add(Me.INDTxtPatientMobileNumber)
        Me.INDlyRoot.Controls.Add(Me.INDTxtPatientEmail)
        Me.INDlyRoot.Controls.Add(Me.INDTxtFunctionalUnit)
        Me.INDlyRoot.Controls.Add(Me.INDTxtBed)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 12)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1552, 857)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDSleGender
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleGender, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleGender, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleGender, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleGender, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleGender, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleGender, False)
        Me.INDSleGender.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleGender, False)
        Me.INDSleGender.Location = New System.Drawing.Point(654, 115)
        Me.INDSleGender.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleGender, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleGender.Name = "INDSleGender"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleGender, False)
        Me.INDSleGender.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleGender.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleGender.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleGender.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleGender.Properties.Appearance.Options.UseFont = True
        Me.INDSleGender.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleGender.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleGender.Properties.DisplayMember = "CodeName"
        Me.INDSleGender.Properties.NullText = ""
        Me.INDSleGender.Properties.PopupSizeable = False
        Me.INDSleGender.Properties.PopupView = Me.GridView1
        Me.INDSleGender.Properties.ShowFooter = False
        Me.INDSleGender.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleGender, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleGender, True)
        Me.INDSleGender.Size = New System.Drawing.Size(579, 38)
        Me.INDSleGender.StyleController = Me.INDlyRoot
        Me.INDSleGender.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleGender, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleGender, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleGender, "{0} - {1}")
        Me.INDSleGender.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleGender, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleGender, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridView1.DetailHeight = 512
        Me.GridView1.FixedLineWidth = 3
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Género"
        Me.GridColumn2.FieldName = "CodeName"
        Me.GridColumn2.MinWidth = 30
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 112
        '
        'INDtxtLastName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtLastName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtLastName, True)
        Me.INDtxtLastName.EnterMoveNextControl = True
        Me.INDtxtLastName.Location = New System.Drawing.Point(35, 379)
        Me.INDtxtLastName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtLastName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtLastName.Name = "INDtxtLastName"
        Me.INDtxtLastName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtLastName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtLastName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtLastName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLastName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLastName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtLastName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtLastName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLastName.Properties.Mask.EditMask = "[a-zA-Z ]+"
        Me.INDtxtLastName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtLastName.Properties.MaxLength = 100
        Me.INDtxtLastName.Size = New System.Drawing.Size(579, 38)
        Me.INDtxtLastName.StyleController = Me.INDlyRoot
        Me.INDtxtLastName.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtLastName, 0)
        Me.INDtxtLastName.ToolTip = "Este Campo es Necesario"
        '
        'INDsleIdentificationType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleIdentificationType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleIdentificationType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleIdentificationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleIdentificationType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleIdentificationType, False)
        Me.INDsleIdentificationType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleIdentificationType, False)
        Me.INDsleIdentificationType.Location = New System.Drawing.Point(35, 203)
        Me.INDsleIdentificationType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleIdentificationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleIdentificationType.Name = "INDsleIdentificationType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleIdentificationType, False)
        Me.INDsleIdentificationType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleIdentificationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleIdentificationType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleIdentificationType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleIdentificationType.Properties.Appearance.Options.UseFont = True
        Me.INDsleIdentificationType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleIdentificationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleIdentificationType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleIdentificationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleIdentificationType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleIdentificationType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleIdentificationType.Properties.DisplayMember = "NOMBRE"
        Me.INDsleIdentificationType.Properties.NullText = ""
        Me.INDsleIdentificationType.Properties.PopupSizeable = False
        Me.INDsleIdentificationType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleIdentificationType.Properties.ShowFooter = False
        Me.INDsleIdentificationType.Properties.ValueMember = "ID"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleIdentificationType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleIdentificationType, True)
        Me.INDsleIdentificationType.Size = New System.Drawing.Size(579, 38)
        Me.INDsleIdentificationType.StyleController = Me.INDlyRoot
        Me.INDsleIdentificationType.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleIdentificationType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleIdentificationType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleIdentificationType, "{0} - {1}")
        Me.INDsleIdentificationType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleIdentificationType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleIdentificationType, False)
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
        Me.SearchLookUpEdit1View.DetailHeight = 512
        Me.SearchLookUpEdit1View.FixedLineWidth = 3
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
        Me.GridColumn1.Caption = "Tipo de identificación"
        Me.GridColumn1.FieldName = "NOMBRE"
        Me.GridColumn1.MinWidth = 30
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 112
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(35, 291)
        Me.INDtxtName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.Mask.EditMask = "[a-zA-Z ]+"
        Me.INDtxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtName.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(579, 38)
        Me.INDtxtName.StyleController = Me.INDlyRoot
        Me.INDtxtName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnIdentification
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnIdentification, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnIdentification, True)
        Me.INDbtnIdentification.Location = New System.Drawing.Point(35, 115)
        Me.INDbtnIdentification.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnIdentification, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnIdentification.Name = "INDbtnIdentification"
        Me.INDbtnIdentification.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnIdentification.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnIdentification.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnIdentification.Properties.Appearance.Options.UseFont = True
        Me.INDbtnIdentification.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnIdentification.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnIdentification.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnIdentification.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnIdentification.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnIdentification.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnIdentification.Properties.Mask.EditMask = "[0-9a-zA-Z ]*"
        Me.INDbtnIdentification.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbtnIdentification.Properties.MaxLength = 20
        Me.INDbtnIdentification.Size = New System.Drawing.Size(579, 38)
        Me.INDbtnIdentification.StyleController = Me.INDlyRoot
        Me.INDbtnIdentification.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnIdentification, 0)
        '
        'INDTxtPatientMobileNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPatientMobileNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPatientMobileNumber, True)
        Me.INDTxtPatientMobileNumber.EnterMoveNextControl = True
        Me.INDTxtPatientMobileNumber.Location = New System.Drawing.Point(35, 467)
        Me.INDTxtPatientMobileNumber.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPatientMobileNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtPatientMobileNumber.Name = "INDTxtPatientMobileNumber"
        Me.INDTxtPatientMobileNumber.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtPatientMobileNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPatientMobileNumber.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtPatientMobileNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPatientMobileNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPatientMobileNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtPatientMobileNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPatientMobileNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPatientMobileNumber.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtPatientMobileNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtPatientMobileNumber.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtPatientMobileNumber.Properties.MaxLength = 10
        Me.INDTxtPatientMobileNumber.Size = New System.Drawing.Size(579, 38)
        Me.INDTxtPatientMobileNumber.StyleController = Me.INDlyRoot
        Me.INDTxtPatientMobileNumber.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPatientMobileNumber, 0)
        Me.INDTxtPatientMobileNumber.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtPatientEmail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPatientEmail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPatientEmail, True)
        Me.INDTxtPatientEmail.EnterMoveNextControl = True
        Me.INDTxtPatientEmail.Location = New System.Drawing.Point(35, 555)
        Me.INDTxtPatientEmail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPatientEmail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtPatientEmail.Name = "INDTxtPatientEmail"
        Me.INDTxtPatientEmail.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtPatientEmail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPatientEmail.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtPatientEmail.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPatientEmail.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPatientEmail.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtPatientEmail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPatientEmail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPatientEmail.Properties.MaxLength = 300
        Me.INDTxtPatientEmail.Size = New System.Drawing.Size(579, 38)
        Me.INDTxtPatientEmail.StyleController = Me.INDlyRoot
        Me.INDTxtPatientEmail.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPatientEmail, 0)
        Me.INDTxtPatientEmail.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtFunctionalUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtFunctionalUnit, True)
        Me.INDTxtFunctionalUnit.EnterMoveNextControl = True
        Me.INDTxtFunctionalUnit.Location = New System.Drawing.Point(654, 203)
        Me.INDTxtFunctionalUnit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtFunctionalUnit.Name = "INDTxtFunctionalUnit"
        Me.INDTxtFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFunctionalUnit.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTxtFunctionalUnit.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtFunctionalUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFunctionalUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtFunctionalUnit.Properties.Mask.EditMask = "[a-zA-Z ]+"
        Me.INDTxtFunctionalUnit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtFunctionalUnit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtFunctionalUnit.Properties.MaxLength = 100
        Me.INDTxtFunctionalUnit.Size = New System.Drawing.Size(579, 38)
        Me.INDTxtFunctionalUnit.StyleController = Me.INDlyRoot
        Me.INDTxtFunctionalUnit.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtFunctionalUnit, 0)
        Me.INDTxtFunctionalUnit.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtBed
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtBed, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtBed, True)
        Me.INDTxtBed.EnterMoveNextControl = True
        Me.INDTxtBed.Location = New System.Drawing.Point(654, 291)
        Me.INDTxtBed.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtBed, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtBed.Name = "INDTxtBed"
        Me.INDTxtBed.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtBed.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtBed.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtBed.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtBed.Properties.Appearance.Options.UseFont = True
        Me.INDTxtBed.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtBed.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtBed.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtBed.Properties.Mask.EditMask = "[0-9a-zA-Z ]*"
        Me.INDTxtBed.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtBed.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtBed.Properties.MaxLength = 10
        Me.INDTxtBed.Size = New System.Drawing.Size(579, 38)
        Me.INDTxtBed.StyleController = Me.INDlyRoot
        Me.INDTxtBed.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtBed, 0)
        Me.INDTxtBed.ToolTip = "Este Campo es Necesario"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation, Me.LayoutControlGroup1})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1552, 857)
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
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemIdentification, Me.INDlyItemName, Me.INDlyItemIdentificationType, Me.INDlyItemLastName, Me.INDLciPatientEmail, Me.INDLciPatientMobileNumber})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(619, 827)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDlyItemIdentification
        '
        Me.INDlyItemIdentification.AllowHide = False
        Me.INDlyItemIdentification.Control = Me.INDbtnIdentification
        Me.INDlyItemIdentification.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemIdentification.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemIdentification.MinSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemIdentification.Name = "INDlyItemIdentification"
        Me.INDlyItemIdentification.Size = New System.Drawing.Size(585, 88)
        Me.INDlyItemIdentification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIdentification.Text = "Identificación"
        Me.INDlyItemIdentification.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIdentification.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIdentification.TextSize = New System.Drawing.Size(202, 31)
        Me.INDlyItemIdentification.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 176)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(585, 88)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombres del paciente"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(202, 31)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemIdentificationType
        '
        Me.INDlyItemIdentificationType.AllowHide = False
        Me.INDlyItemIdentificationType.Control = Me.INDsleIdentificationType
        Me.INDlyItemIdentificationType.Location = New System.Drawing.Point(0, 88)
        Me.INDlyItemIdentificationType.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemIdentificationType.MinSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemIdentificationType.Name = "INDlyItemIdentificationType"
        Me.INDlyItemIdentificationType.Size = New System.Drawing.Size(585, 88)
        Me.INDlyItemIdentificationType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIdentificationType.Text = "Tipo Identificación"
        Me.INDlyItemIdentificationType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIdentificationType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIdentificationType.TextSize = New System.Drawing.Size(202, 31)
        Me.INDlyItemIdentificationType.TextToControlDistance = 5
        '
        'INDlyItemLastName
        '
        Me.INDlyItemLastName.AllowHide = False
        Me.INDlyItemLastName.Control = Me.INDtxtLastName
        Me.INDlyItemLastName.Location = New System.Drawing.Point(0, 264)
        Me.INDlyItemLastName.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemLastName.MinSize = New System.Drawing.Size(585, 88)
        Me.INDlyItemLastName.Name = "INDlyItemLastName"
        Me.INDlyItemLastName.Size = New System.Drawing.Size(585, 88)
        Me.INDlyItemLastName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLastName.Text = "Apellidos del paciente"
        Me.INDlyItemLastName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLastName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemLastName.TextSize = New System.Drawing.Size(202, 31)
        Me.INDlyItemLastName.TextToControlDistance = 5
        '
        'INDLciPatientEmail
        '
        Me.INDLciPatientEmail.AllowHide = False
        Me.INDLciPatientEmail.Control = Me.INDTxtPatientEmail
        Me.INDLciPatientEmail.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPatientEmail.CustomizationFormText = "Email"
        Me.INDLciPatientEmail.Location = New System.Drawing.Point(0, 440)
        Me.INDLciPatientEmail.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDLciPatientEmail.MinSize = New System.Drawing.Size(585, 88)
        Me.INDLciPatientEmail.Name = "INDLciPatientEmail"
        Me.INDLciPatientEmail.Size = New System.Drawing.Size(585, 309)
        Me.INDLciPatientEmail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPatientEmail.Text = "Correo electrónico"
        Me.INDLciPatientEmail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPatientEmail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPatientEmail.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciPatientEmail.TextToControlDistance = 5
        '
        'INDLciPatientMobileNumber
        '
        Me.INDLciPatientMobileNumber.AllowHide = False
        Me.INDLciPatientMobileNumber.Control = Me.INDTxtPatientMobileNumber
        Me.INDLciPatientMobileNumber.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPatientMobileNumber.CustomizationFormText = "Número de celular"
        Me.INDLciPatientMobileNumber.Location = New System.Drawing.Point(0, 352)
        Me.INDLciPatientMobileNumber.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDLciPatientMobileNumber.MinSize = New System.Drawing.Size(585, 88)
        Me.INDLciPatientMobileNumber.Name = "INDLciPatientMobileNumber"
        Me.INDLciPatientMobileNumber.Size = New System.Drawing.Size(585, 88)
        Me.INDLciPatientMobileNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPatientMobileNumber.Text = "Número de celular"
        Me.INDLciPatientMobileNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPatientMobileNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPatientMobileNumber.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciPatientMobileNumber.TextToControlDistance = 5
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
        Me.LayoutControlGroup1.CustomizationFormText = "Información principal"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciFunctionalUnit, Me.INDLciBed, Me.INDLciGender})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(619, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(903, 827)
        Me.LayoutControlGroup1.Text = "Información principal"
        '
        'INDLciFunctionalUnit
        '
        Me.INDLciFunctionalUnit.AllowHide = False
        Me.INDLciFunctionalUnit.Control = Me.INDTxtFunctionalUnit
        Me.INDLciFunctionalUnit.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciFunctionalUnit.CustomizationFormText = "Unidad funcional"
        Me.INDLciFunctionalUnit.Location = New System.Drawing.Point(0, 88)
        Me.INDLciFunctionalUnit.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDLciFunctionalUnit.MinSize = New System.Drawing.Size(585, 88)
        Me.INDLciFunctionalUnit.Name = "INDLciFunctionalUnit"
        Me.INDLciFunctionalUnit.Size = New System.Drawing.Size(869, 88)
        Me.INDLciFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFunctionalUnit.Text = "Unidad funcional"
        Me.INDLciFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFunctionalUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFunctionalUnit.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciFunctionalUnit.TextToControlDistance = 5
        '
        'INDLciBed
        '
        Me.INDLciBed.AllowHide = False
        Me.INDLciBed.Control = Me.INDTxtBed
        Me.INDLciBed.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciBed.CustomizationFormText = "Cama"
        Me.INDLciBed.Location = New System.Drawing.Point(0, 176)
        Me.INDLciBed.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDLciBed.MinSize = New System.Drawing.Size(585, 88)
        Me.INDLciBed.Name = "INDLciBed"
        Me.INDLciBed.Size = New System.Drawing.Size(869, 573)
        Me.INDLciBed.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBed.Text = "Cama"
        Me.INDLciBed.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBed.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBed.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciBed.TextToControlDistance = 5
        '
        'INDLciGender
        '
        Me.INDLciGender.AllowHide = False
        Me.INDLciGender.Control = Me.INDSleGender
        Me.INDLciGender.Location = New System.Drawing.Point(0, 0)
        Me.INDLciGender.MaxSize = New System.Drawing.Size(585, 88)
        Me.INDLciGender.MinSize = New System.Drawing.Size(585, 88)
        Me.INDLciGender.Name = "INDLciGender"
        Me.INDLciGender.Size = New System.Drawing.Size(869, 88)
        Me.INDLciGender.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGender.Text = "Género"
        Me.INDLciGender.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGender.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciGender.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciGender.TextToControlDistance = 5
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAdd.Appearance.Options.UseFont = True
        Me.INDbtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDbtnAdd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAdd, True)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(1548, 52)
        Me.INDbtnAdd.TabIndex = 5
        Me.INDbtnAdd.Text = "Agregar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAdd)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(202, 869)
        Me.INDpanelButtons.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDpanelButtons.MaximumSize = New System.Drawing.Size(0, 56)
        Me.INDpanelButtons.MinimumSize = New System.Drawing.Size(0, 56)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(1552, 56)
        Me.INDpanelButtons.TabIndex = 2
        '
        'FrmPatientExternalCareCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1756, 1067)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPatientExternalCareCenter"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Tag = "2196"
        Me.Text = "Pacientes para Centros de Atención Externos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDSleGender.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtLastName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleIdentificationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnIdentification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtPatientMobileNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtPatientEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtBed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIdentification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIdentificationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLastName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPatientEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPatientMobileNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDbtnIdentification As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemIdentification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteStabilityDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemStabilityDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleIdentificationType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemIdentificationType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtLastName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemLastName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleGender As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciGender As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtPatientMobileNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciPatientMobileNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtPatientEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciPatientEmail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtFunctionalUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtBed As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciBed As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
End Class
