Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRejectionReason
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcUsers = New DevExpress.XtraGrid.GridControl()
        Me.viewUsersGrid = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAddUser = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleUsers = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewUserSearch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgGeneral = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAuthorization = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemUsers = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddUser = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcBase.SuspendLayout()
        CType(Me.INDgcUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUsersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleUsers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUserSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAuthorization, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcBase)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1183, 593)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1183, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1183, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcBase
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 583)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcBase
        '
        Me.INDlcBase.AllowCustomization = False
        Me.INDlcBase.Controls.Add(Me.INDgcUsers)
        Me.INDlcBase.Controls.Add(Me.INDbtnAddUser)
        Me.INDlcBase.Controls.Add(Me.INDsleUsers)
        Me.INDlcBase.Controls.Add(Me.INDtxtName)
        Me.INDlcBase.Controls.Add(Me.INDbtnCode)
        Me.INDlcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcBase, False)
        Me.INDlcBase.Location = New System.Drawing.Point(202, 8)
        Me.INDlcBase.Name = "INDlcBase"
        Me.INDlcBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(430, 410, 574, 569)
        Me.INDlcBase.Root = Me.LayoutControlGroup1
        Me.INDlcBase.Size = New System.Drawing.Size(979, 583)
        Me.INDlcBase.TabIndex = 1
        Me.INDlcBase.Text = "LayoutControl1"
        '
        'INDgcUsers
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcUsers, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcUsers, Nothing)
        Me.INDgcUsers.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcUsers, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcUsers, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcUsers, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcUsers, False)
        Me.INDgcUsers.Location = New System.Drawing.Point(263, 85)
        Me.INDgcUsers.MainView = Me.viewUsersGrid
        Me.INDgcUsers.Name = "INDgcUsers"
        Me.INDgcUsers.Size = New System.Drawing.Size(692, 474)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcUsers, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcUsers.TabIndex = 10
        Me.INDgcUsers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewUsersGrid})
        '
        'viewUsersGrid
        '
        Me.viewUsersGrid.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewUsersGrid.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewUsersGrid.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewUsersGrid.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewUsersGrid.Appearance.FocusedRow.Options.UseFont = True
        Me.viewUsersGrid.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUsersGrid.Appearance.GroupRow.Options.UseFont = True
        Me.viewUsersGrid.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUsersGrid.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewUsersGrid.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewUsersGrid.Appearance.Row.Options.UseFont = True
        Me.viewUsersGrid.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewUsersGrid.Appearance.ViewCaption.Options.UseFont = True
        Me.viewUsersGrid.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.viewUsersGrid.GridControl = Me.INDgcUsers
        Me.viewUsersGrid.Name = "viewUsersGrid"
        Me.viewUsersGrid.OptionsCustomization.AllowGroup = False
        Me.viewUsersGrid.OptionsDetail.EnableMasterViewMode = False
        Me.viewUsersGrid.OptionsDetail.ShowDetailTabs = False
        Me.viewUsersGrid.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUsersGrid.OptionsView.EnableAppearanceOddRow = True
        Me.viewUsersGrid.OptionsView.ShowAutoFilterRow = True
        Me.viewUsersGrid.OptionsView.ShowDetailButtons = False
        Me.viewUsersGrid.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código "
        Me.GridColumn3.FieldName = "Usercode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 290
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción"
        Me.GridColumn4.FieldName = "FullNameUser"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 511
        '
        'INDbtnAddUser
        '
        Me.INDbtnAddUser.Location = New System.Drawing.Point(667, 53)
        Me.INDbtnAddUser.Margin = New System.Windows.Forms.Padding(2)
        Me.INDbtnAddUser.Name = "INDbtnAddUser"
        Me.INDbtnAddUser.Size = New System.Drawing.Size(288, 27)
        Me.INDbtnAddUser.StyleController = Me.INDlcBase
        Me.INDbtnAddUser.TabIndex = 10
        Me.INDbtnAddUser.Text = "Agregar"
        '
        'INDsleUsers
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleUsers, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleUsers, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleUsers, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.Location = New System.Drawing.Point(403, 53)
        Me.INDsleUsers.Margin = New System.Windows.Forms.Padding(2)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleUsers, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleUsers.Name = "INDsleUsers"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleUsers, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleUsers.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleUsers.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleUsers.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleUsers.Properties.Appearance.Options.UseFont = True
        Me.INDsleUsers.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleUsers.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleUsers.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleUsers.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleUsers.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleUsers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleUsers.Properties.DisplayMember = "CodeName"
        Me.INDsleUsers.Properties.NullText = ""
        Me.INDsleUsers.Properties.PopupSizeable = False
        Me.INDsleUsers.Properties.PopupView = Me.viewUserSearch
        Me.INDsleUsers.Properties.ShowFooter = False
        Me.INDsleUsers.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleUsers, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleUsers, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleUsers, True)
        Me.INDsleUsers.Size = New System.Drawing.Size(260, 28)
        Me.INDsleUsers.StyleController = Me.INDlcBase
        Me.INDsleUsers.TabIndex = 9
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleUsers, "103")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleUsers, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleUsers, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleUsers, False)
        '
        'viewUserSearch
        '
        Me.viewUserSearch.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewUserSearch.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewUserSearch.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewUserSearch.Appearance.FocusedRow.Options.UseFont = True
        Me.viewUserSearch.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearch.Appearance.GroupRow.Options.UseFont = True
        Me.viewUserSearch.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearch.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewUserSearch.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewUserSearch.Appearance.Row.Options.UseFont = True
        Me.viewUserSearch.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.viewUserSearch.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewUserSearch.Name = "viewUserSearch"
        Me.viewUserSearch.OptionsFind.AlwaysVisible = True
        Me.viewUserSearch.OptionsFind.FindFilterColumns = "UserCode"
        Me.viewUserSearch.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewUserSearch.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUserSearch.OptionsView.EnableAppearanceOddRow = True
        Me.viewUserSearch.OptionsView.ShowAutoFilterRow = True
        Me.viewUserSearch.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewUserSearch, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "UserCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "IdPerson.Fullname"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 137)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtName.Properties.MaxLength = 300
        Me.INDtxtName.Size = New System.Drawing.Size(211, 28)
        Me.INDtxtName.StyleController = Me.INDlcBase
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions1.Image = Global.Presentation.AccountManagement.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(211, 28)
        Me.INDbtnCode.StyleController = Me.INDlcBase
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.LayoutControlGroup1.CustomizationFormText = "Autorizaciones de Facturación"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgGeneral, Me.INDlygAuthorization})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(979, 583)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgGeneral
        '
        Me.INDlcgGeneral.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgGeneral.AppearanceGroup.Options.UseFont = True
        Me.INDlcgGeneral.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgGeneral.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgGeneral.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneral.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgGeneral.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgGeneral.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgGeneral.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneral.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgGeneral.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneral.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgGeneral.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneral.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgGeneral, False)
        Me.INDlcgGeneral.CustomizationFormText = "Información General"
        Me.INDlcgGeneral.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciName, Me.INDlciCode})
        Me.INDlcgGeneral.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgGeneral.Name = "INDlcgGeneral"
        Me.INDlcgGeneral.Size = New System.Drawing.Size(239, 563)
        Me.INDlcgGeneral.Text = "Datos Principales"
        '
        'INDlciName
        '
        Me.INDlciName.AllowHide = False
        Me.INDlciName.Control = Me.INDtxtName
        Me.INDlciName.CustomizationFormText = "LayoutControlItem2"
        Me.INDlciName.Location = New System.Drawing.Point(0, 58)
        Me.INDlciName.Name = "INDlciName"
        Me.INDlciName.ShowInCustomizationForm = False
        Me.INDlciName.Size = New System.Drawing.Size(215, 452)
        Me.INDlciName.Text = "Nombre"
        Me.INDlciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciName.TextToControlDistance = 5
        '
        'INDlciCode
        '
        Me.INDlciCode.AllowHide = False
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(0, 58)
        Me.INDlciCode.MinSize = New System.Drawing.Size(200, 58)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(215, 58)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlygAuthorization
        '
        Me.INDlygAuthorization.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAuthorization.AppearanceGroup.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAuthorization.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAuthorization, False)
        Me.INDlygAuthorization.CustomizationFormText = "Autorización"
        Me.INDlygAuthorization.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemUsers, Me.LayoutControlItem1, Me.INDlyItemAddUser})
        Me.INDlygAuthorization.Location = New System.Drawing.Point(239, 0)
        Me.INDlygAuthorization.Name = "INDlygAuthorization"
        Me.INDlygAuthorization.Size = New System.Drawing.Size(720, 563)
        Me.INDlygAuthorization.Text = "Autorización"
        '
        'INDlyItemUsers
        '
        Me.INDlyItemUsers.Control = Me.INDsleUsers
        Me.INDlyItemUsers.CustomizationFormText = "Usuarios"
        Me.INDlyItemUsers.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemUsers.Name = "INDlyItemUsers"
        Me.INDlyItemUsers.Size = New System.Drawing.Size(404, 32)
        Me.INDlyItemUsers.Text = "Usuarios"
        Me.INDlyItemUsers.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemUsers.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemUsers.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcUsers
        Me.LayoutControlItem1.CustomizationFormText = "Usuarios"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(696, 478)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlyItemAddUser
        '
        Me.INDlyItemAddUser.Control = Me.INDbtnAddUser
        Me.INDlyItemAddUser.CustomizationFormText = "Agregar"
        Me.INDlyItemAddUser.Location = New System.Drawing.Point(404, 0)
        Me.INDlyItemAddUser.MaxSize = New System.Drawing.Size(0, 31)
        Me.INDlyItemAddUser.MinSize = New System.Drawing.Size(49, 31)
        Me.INDlyItemAddUser.Name = "INDlyItemAddUser"
        Me.INDlyItemAddUser.Size = New System.Drawing.Size(292, 32)
        Me.INDlyItemAddUser.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddUser.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddUser.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmRejectionReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1183, 729)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmRejectionReason"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "2850"
        Me.Text = "Áreas de Getión"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcBase.ResumeLayout(False)
        CType(Me.INDgcUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUsersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleUsers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUserSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAuthorization, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDlcgGeneral As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDsleUsers As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewUserSearch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygAuthorization As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemUsers As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnAddUser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddUser As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcUsers As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewUsersGrid As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoDate1 As IndigoDate
End Class
