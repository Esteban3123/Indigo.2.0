Imports DevExpress.Data
Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAlertsPopup
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcTransferMainControl = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLcAlertsMain_r = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcCurrentAlerts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCurrentAlerts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcAlertDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcAlertComment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcAlertManagementArea = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcAlertUsername = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SuspendButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDSbAddAlert = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcPreviousAlerts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPreviousAlerts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgAlertsMain_r = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgFolioAlerts = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgCurrentAlertsTab = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgPreviousAlertsTab = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgAlertsMainGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcciAlertsMain_r = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView_Current = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcTransferMainControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcTransferMainControl.SuspendLayout()
        CType(Me.INDLcAlertsMain_r, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcAlertsMain_r.SuspendLayout()
        CType(Me.INDGcCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SuspendButtonEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcPreviousAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPreviousAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAlertsMain_r, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgFolioAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCurrentAlertsTab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPreviousAlertsTab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAlertsMainGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcciAlertsMain_r, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView_Current, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'INDLcTransferMainControl
        '
        Me.INDLcTransferMainControl.Controls.Add(Me.INDLcAlertsMain_r)
        Me.INDLcTransferMainControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcTransferMainControl.Location = New System.Drawing.Point(0, 0)
        Me.INDLcTransferMainControl.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDLcTransferMainControl.Name = "INDLcTransferMainControl"
        Me.INDLcTransferMainControl.Root = Me.INDLcgAlertsMainGroup
        Me.INDLcTransferMainControl.Size = New System.Drawing.Size(657, 342)
        Me.INDLcTransferMainControl.TabIndex = 0
        Me.INDLcTransferMainControl.Text = "LayoutControl2"
        '
        'INDLcAlertsMain_r
        '
        Me.INDLcAlertsMain_r.Controls.Add(Me.INDGcCurrentAlerts)
        Me.INDLcAlertsMain_r.Controls.Add(Me.INDSbAddAlert)
        Me.INDLcAlertsMain_r.Controls.Add(Me.INDGcPreviousAlerts)
        Me.INDLcAlertsMain_r.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcAlertsMain_r.Location = New System.Drawing.Point(2, 2)
        Me.INDLcAlertsMain_r.Margin = New System.Windows.Forms.Padding(0)
        Me.INDLcAlertsMain_r.Name = "INDLcAlertsMain_r"
        Me.INDLcAlertsMain_r.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(871, 157, 574, 569)
        Me.INDLcAlertsMain_r.Root = Me.INDLcgAlertsMain_r
        Me.INDLcAlertsMain_r.Size = New System.Drawing.Size(653, 338)
        Me.INDLcAlertsMain_r.TabIndex = 4
        Me.INDLcAlertsMain_r.Text = "LayoutControl3"
        '
        'INDGcCurrentAlerts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCurrentAlerts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCurrentAlerts, Nothing)
        Me.INDGcCurrentAlerts.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCurrentAlerts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCurrentAlerts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCurrentAlerts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCurrentAlerts, False)
        Me.INDGcCurrentAlerts.Location = New System.Drawing.Point(24, 93)
        Me.INDGcCurrentAlerts.MainView = Me.INDGvCurrentAlerts
        Me.INDGcCurrentAlerts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcCurrentAlerts.Name = "INDGcCurrentAlerts"
        Me.INDGcCurrentAlerts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.SuspendButtonEdit})
        Me.INDGcCurrentAlerts.Size = New System.Drawing.Size(605, 221)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCurrentAlerts, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcCurrentAlerts.TabIndex = 4
        Me.INDGcCurrentAlerts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCurrentAlerts})
        '
        'INDGvCurrentAlerts
        '
        Me.INDGvCurrentAlerts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCurrentAlerts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCurrentAlerts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCurrentAlerts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCurrentAlerts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrentAlerts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCurrentAlerts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrentAlerts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCurrentAlerts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCurrentAlerts.Appearance.Row.Options.UseFont = True
        Me.INDGvCurrentAlerts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCurrentAlerts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCurrentAlerts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcAlertDate, Me.INDGcAlertComment, Me.INDGcAlertManagementArea, Me.INDGcAlertUsername, Me.INDGcActions})
        Me.INDGvCurrentAlerts.DetailHeight = 330
        Me.INDGvCurrentAlerts.GridControl = Me.INDGcCurrentAlerts
        Me.INDGvCurrentAlerts.Name = "INDGvCurrentAlerts"
        Me.INDGvCurrentAlerts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCurrentAlerts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCurrentAlerts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCurrentAlerts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView_Current.SetTemaIndigoMetro(Me.INDGvCurrentAlerts, False)
        '
        'INDGcAlertDate
        '
        Me.INDGcAlertDate.Caption = "Fecha"
        Me.INDGcAlertDate.FieldName = "CreationDate"
        Me.INDGcAlertDate.Name = "INDGcAlertDate"
        Me.INDGcAlertDate.OptionsColumn.AllowEdit = False
        Me.INDGcAlertDate.Visible = True
        Me.INDGcAlertDate.VisibleIndex = 0
        '
        'INDGcAlertComment
        '
        Me.INDGcAlertComment.Caption = "Comentario"
        Me.INDGcAlertComment.FieldName = "Comments"
        Me.INDGcAlertComment.Name = "INDGcAlertComment"
        Me.INDGcAlertComment.OptionsColumn.AllowEdit = False
        Me.INDGcAlertComment.Visible = True
        Me.INDGcAlertComment.VisibleIndex = 1
        '
        'INDGcAlertManagementArea
        '
        Me.INDGcAlertManagementArea.Caption = "Area de gestión"
        Me.INDGcAlertManagementArea.FieldName = "ManagementAreaName"
        Me.INDGcAlertManagementArea.Name = "INDGcAlertManagementArea"
        Me.INDGcAlertManagementArea.OptionsColumn.AllowEdit = False
        Me.INDGcAlertManagementArea.Visible = True
        Me.INDGcAlertManagementArea.VisibleIndex = 2
        '
        'INDGcAlertUsername
        '
        Me.INDGcAlertUsername.Caption = "Nombre de usuario"
        Me.INDGcAlertUsername.FieldName = "UserName"
        Me.INDGcAlertUsername.Name = "INDGcAlertUsername"
        Me.INDGcAlertUsername.OptionsColumn.AllowEdit = False
        Me.INDGcAlertUsername.Visible = True
        Me.INDGcAlertUsername.VisibleIndex = 3
        '
        'INDGcActions
        '
        Me.INDGcActions.Caption = "Acciones"
        Me.INDGcActions.ColumnEdit = Me.SuspendButtonEdit
        Me.INDGcActions.MinWidth = 25
        Me.INDGcActions.Name = "INDGcActions"
        Me.INDGcActions.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDGcActions.Visible = True
        Me.INDGcActions.VisibleIndex = 4
        Me.INDGcActions.Width = 100
        '
        'SuspendButtonEdit
        '
        Me.SuspendButtonEdit.AutoHeight = False
        Me.SuspendButtonEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Suspender", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.SuspendButtonEdit.Name = "SuspendButtonEdit"
        Me.SuspendButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDSbAddAlert
        '
        Me.INDSbAddAlert.ImageOptions.Image = Global.Presentation.AccountManagement.My.Resources.Resources.Agregar16
        Me.INDSbAddAlert.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDSbAddAlert.Location = New System.Drawing.Point(22, 62)
        Me.INDSbAddAlert.Margin = New System.Windows.Forms.Padding(0)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbAddAlert, False)
        Me.INDSbAddAlert.Name = "INDSbAddAlert"
        Me.INDSbAddAlert.Size = New System.Drawing.Size(609, 27)
        Me.INDSbAddAlert.StyleController = Me.INDLcAlertsMain_r
        Me.INDSbAddAlert.TabIndex = 6
        '
        'INDGcPreviousAlerts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPreviousAlerts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPreviousAlerts, Nothing)
        Me.INDGcPreviousAlerts.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPreviousAlerts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPreviousAlerts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPreviousAlerts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPreviousAlerts, False)
        Me.INDGcPreviousAlerts.Location = New System.Drawing.Point(24, 64)
        Me.INDGcPreviousAlerts.MainView = Me.INDGvPreviousAlerts
        Me.INDGcPreviousAlerts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcPreviousAlerts.Name = "INDGcPreviousAlerts"
        Me.INDGcPreviousAlerts.Size = New System.Drawing.Size(605, 250)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPreviousAlerts, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcPreviousAlerts.TabIndex = 5
        Me.INDGcPreviousAlerts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPreviousAlerts})
        '
        'INDGvPreviousAlerts
        '
        Me.INDGvPreviousAlerts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPreviousAlerts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPreviousAlerts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPreviousAlerts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPreviousAlerts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPreviousAlerts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPreviousAlerts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPreviousAlerts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPreviousAlerts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPreviousAlerts.Appearance.Row.Options.UseFont = True
        Me.INDGvPreviousAlerts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPreviousAlerts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPreviousAlerts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn5})
        Me.INDGvPreviousAlerts.DetailHeight = 330
        Me.INDGvPreviousAlerts.GridControl = Me.INDGcPreviousAlerts
        Me.INDGvPreviousAlerts.Name = "INDGvPreviousAlerts"
        Me.INDGvPreviousAlerts.OptionsBehavior.Editable = False
        Me.INDGvPreviousAlerts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPreviousAlerts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPreviousAlerts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPreviousAlerts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView_Current.SetTemaIndigoMetro(Me.INDGvPreviousAlerts, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Fecha"
        Me.GridColumn1.FieldName = "CreationDate"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Comentario"
        Me.GridColumn2.FieldName = "Comments"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Area de gestión"
        Me.GridColumn3.FieldName = "ManagementAreaName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nombre de usuario"
        Me.GridColumn5.FieldName = "UserName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        '
        'INDLcgAlertsMain_r
        '
        Me.INDLcgAlertsMain_r.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAlertsMain_r.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAlertsMain_r.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAlertsMain_r.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAlertsMain_r.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMain_r.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAlertsMain_r.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAlertsMain_r.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAlertsMain_r.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMain_r.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAlertsMain_r.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMain_r.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAlertsMain_r.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMain_r.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgAlertsMain_r, False)
        Me.INDLcgAlertsMain_r.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgAlertsMain_r.GroupBordersVisible = False
        Me.INDLcgAlertsMain_r.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgFolioAlerts})
        Me.INDLcgAlertsMain_r.Name = "INDLcgAlertsMain_r"
        Me.INDLcgAlertsMain_r.Size = New System.Drawing.Size(653, 338)
        Me.INDLcgAlertsMain_r.TextVisible = False
        '
        'INDTcgFolioAlerts
        '
        Me.INDTcgFolioAlerts.CustomizationFormText = "INDTcgFolioAlerts"
        Me.INDTcgFolioAlerts.Location = New System.Drawing.Point(0, 0)
        Me.INDTcgFolioAlerts.Name = "INDTcgFolioAlerts"
        Me.INDTcgFolioAlerts.SelectedTabPage = Me.INDLcgCurrentAlertsTab
        Me.INDTcgFolioAlerts.Size = New System.Drawing.Size(633, 318)
        Me.INDTcgFolioAlerts.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgCurrentAlertsTab, Me.INDLcgPreviousAlertsTab})
        '
        'INDLcgCurrentAlertsTab
        '
        Me.INDLcgCurrentAlertsTab.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCurrentAlertsTab.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCurrentAlertsTab.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCurrentAlertsTab.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCurrentAlertsTab.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCurrentAlertsTab, False)
        Me.INDLcgCurrentAlertsTab.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem9})
        Me.INDLcgCurrentAlertsTab.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCurrentAlertsTab.Name = "INDLcgCurrentAlertsTab"
        Me.INDLcgCurrentAlertsTab.Size = New System.Drawing.Size(609, 254)
        Me.INDLcgCurrentAlertsTab.Text = "Alertas Actuales"
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDGcCurrentAlerts
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 29)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(609, 225)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDSbAddAlert
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 2)
        Me.LayoutControlItem9.Size = New System.Drawing.Size(609, 29)
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'INDLcgPreviousAlertsTab
        '
        Me.INDLcgPreviousAlertsTab.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPreviousAlertsTab.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPreviousAlertsTab.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPreviousAlertsTab.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousAlertsTab.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPreviousAlertsTab, False)
        Me.INDLcgPreviousAlertsTab.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem8})
        Me.INDLcgPreviousAlertsTab.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgPreviousAlertsTab.Name = "INDLcgPreviousAlertsTab"
        Me.INDLcgPreviousAlertsTab.Size = New System.Drawing.Size(609, 254)
        Me.INDLcgPreviousAlertsTab.Text = "Alertas Pasadas"
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDGcPreviousAlerts
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(609, 254)
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'INDLcgAlertsMainGroup
        '
        Me.INDLcgAlertsMainGroup.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAlertsMainGroup.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAlertsMainGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAlertsMainGroup.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAlertsMainGroup.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgAlertsMainGroup, False)
        Me.INDLcgAlertsMainGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgAlertsMainGroup.GroupBordersVisible = False
        Me.INDLcgAlertsMainGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcciAlertsMain_r})
        Me.INDLcgAlertsMainGroup.Name = "INDLcgAlertsMainGroup"
        Me.INDLcgAlertsMainGroup.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLcgAlertsMainGroup.Size = New System.Drawing.Size(657, 342)
        Me.INDLcgAlertsMainGroup.Text = "Alertas De Traslados"
        '
        'INDLcciAlertsMain_r
        '
        Me.INDLcciAlertsMain_r.Control = Me.INDLcAlertsMain_r
        Me.INDLcciAlertsMain_r.Location = New System.Drawing.Point(0, 0)
        Me.INDLcciAlertsMain_r.Name = "INDLcciAlertsMain_r"
        Me.INDLcciAlertsMain_r.Size = New System.Drawing.Size(657, 342)
        Me.INDLcciAlertsMain_r.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLcciAlertsMain_r.TextVisible = False
        '
        'IndigoGridView_Current
        '
        Me.IndigoGridView_Current.RaiseMenuPopUp = True
        Me.IndigoGridView_Current.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmAlertsPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 342)
        Me.Controls.Add(Me.INDLcTransferMainControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 600)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(565, 310)
        Me.Name = "FrmAlertsPopup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Alertas De Traslados"
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcTransferMainControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcTransferMainControl.ResumeLayout(False)
        CType(Me.INDLcAlertsMain_r, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcAlertsMain_r.ResumeLayout(False)
        CType(Me.INDGcCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SuspendButtonEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcPreviousAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPreviousAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAlertsMain_r, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgFolioAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCurrentAlertsTab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPreviousAlertsTab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAlertsMainGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcciAlertsMain_r, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView_Current, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcTransferMainControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcAlertsMain_r As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcPreviousAlerts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPreviousAlerts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcCurrentAlerts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCurrentAlerts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcAlertDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcAlertComment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcAlertManagementArea As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcAlertUsername As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSbAddAlert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcgAlertsMain_r As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTcgFolioAlerts As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgCurrentAlertsTab As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgPreviousAlertsTab As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgAlertsMainGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcciAlertsMain_r As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView_Current As Controls.IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGcActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SuspendButtonEdit As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
End Class

