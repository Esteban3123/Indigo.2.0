Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmOpenAlert
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOpenAlert))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDMeComments = New DevExpress.XtraEditors.MemoEdit()
        Me.INDlyAddAlert = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciComments = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAddAlert = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.INDpceAddAlert = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDpopupAddAlert = New DevExpress.XtraEditors.PopupContainerControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcPastAlerts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPastAlerts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvPastAlerts_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvPastAlerts_Comments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvPastAlerts_CreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcCurrentAlerts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCurrentAlerts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCurrentAlerts_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCurrentAlerts_Comments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCurrentAlerts_CreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeStatus = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDtcgInfo = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgCurrentAlerts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCurrentAlerts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgPastAlerts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciPastAlerts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeComments.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyAddAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyAddAlert.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAddAlert.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopupAddAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupAddAlert.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDGcPastAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPastAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtcgInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgPastAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPastAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(953, 579)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(953, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(953, 98)
        '
        'INDMeComments
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeComments, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeComments, False)
        Me.INDMeComments.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeComments, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeComments.Name = "INDMeComments"
        Me.INDMeComments.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMeComments.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeComments.Size = New System.Drawing.Size(393, 113)
        Me.INDMeComments.StyleController = Me.INDlyAddAlert
        Me.INDMeComments.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeComments, 0)
        '
        'INDlyAddAlert
        '
        Me.INDlyAddAlert.Controls.Add(Me.INDMeComments)
        Me.INDlyAddAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyAddAlert.Location = New System.Drawing.Point(0, 0)
        Me.INDlyAddAlert.Name = "INDlyAddAlert"
        Me.INDlyAddAlert.Root = Me.LayoutControlGroup1
        Me.INDlyAddAlert.Size = New System.Drawing.Size(417, 163)
        Me.INDlyAddAlert.TabIndex = 0
        Me.INDlyAddAlert.Text = "LayoutControl1"
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciComments})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(417, 163)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciComments
        '
        Me.INDLciComments.Control = Me.INDMeComments
        Me.INDLciComments.Location = New System.Drawing.Point(0, 0)
        Me.INDLciComments.Name = "INDLciComments"
        Me.INDLciComments.Size = New System.Drawing.Size(397, 143)
        Me.INDLciComments.Text = "Comentario"
        Me.INDLciComments.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciComments.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciComments.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciComments.TextToControlDistance = 5
        '
        'INDbtnAddAlert
        '
        Me.INDbtnAddAlert.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAlert.Appearance.Options.UseFont = True
        Me.INDbtnAddAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddAlert.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAlert, True)
        Me.INDbtnAddAlert.Name = "INDbtnAddAlert"
        Me.INDbtnAddAlert.Size = New System.Drawing.Size(413, 36)
        Me.INDbtnAddAlert.TabIndex = 0
        Me.INDbtnAddAlert.Text = "Agregar"
        '
        'INDpceAddAlert
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceAddAlert, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceAddAlert, Nothing)
        Me.INDpceAddAlert.Location = New System.Drawing.Point(24, 54)
        Me.INDpceAddAlert.MinimumSize = New System.Drawing.Size(896, 32)
        Me.INDpceAddAlert.Name = "INDpceAddAlert"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceAddAlert, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceAddAlert, False)
        Me.INDpceAddAlert.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceAddAlert.Properties.Appearance.Options.UseFont = True
        Me.INDpceAddAlert.Properties.AutoHeight = False
        Me.INDpceAddAlert.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDpceAddAlert.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceAddAlert.Properties.PopupControl = Me.INDpopupAddAlert
        Me.INDpceAddAlert.Properties.PopupSizeable = False
        Me.INDpceAddAlert.Properties.ShowPopupCloseButton = False
        Me.INDpceAddAlert.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAddAlert.Size = New System.Drawing.Size(896, 32)
        Me.INDpceAddAlert.StyleController = Me.INDlyRoot
        Me.INDpceAddAlert.TabIndex = 7
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceAddAlert, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceAddAlert, Nothing)
        '
        'INDpopupAddAlert
        '
        Me.INDpopupAddAlert.Controls.Add(Me.INDlyAddAlert)
        Me.INDpopupAddAlert.Controls.Add(Me.PanelControl1)
        Me.INDpopupAddAlert.Location = New System.Drawing.Point(480, 328)
        Me.INDpopupAddAlert.Name = "INDpopupAddAlert"
        Me.INDpopupAddAlert.Size = New System.Drawing.Size(417, 203)
        Me.INDpopupAddAlert.TabIndex = 8
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddAlert)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 163)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(417, 40)
        Me.PanelControl1.TabIndex = 1
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDpopupAddAlert)
        Me.INDlyRoot.Controls.Add(Me.INDpceAddAlert)
        Me.INDlyRoot.Controls.Add(Me.INDGcPastAlerts)
        Me.INDlyRoot.Controls.Add(Me.INDgcCurrentAlerts)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(949, 570)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDGcPastAlerts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPastAlerts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPastAlerts, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPastAlerts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPastAlerts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPastAlerts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPastAlerts, False)
        Me.INDGcPastAlerts.Location = New System.Drawing.Point(24, 54)
        Me.INDGcPastAlerts.MainView = Me.INDGvPastAlerts
        Me.INDGcPastAlerts.Name = "INDGcPastAlerts"
        Me.INDGcPastAlerts.Size = New System.Drawing.Size(901, 492)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPastAlerts, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcPastAlerts.TabIndex = 6
        Me.INDGcPastAlerts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPastAlerts})
        '
        'INDGvPastAlerts
        '
        Me.INDGvPastAlerts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPastAlerts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPastAlerts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPastAlerts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPastAlerts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPastAlerts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPastAlerts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPastAlerts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPastAlerts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPastAlerts.Appearance.Row.Options.UseFont = True
        Me.INDGvPastAlerts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPastAlerts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPastAlerts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvPastAlerts_CreationDate, Me.INDGvPastAlerts_Comments, Me.INDGvPastAlerts_CreationUser})
        Me.INDGvPastAlerts.GridControl = Me.INDGcPastAlerts
        Me.INDGvPastAlerts.Name = "INDGvPastAlerts"
        Me.INDGvPastAlerts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPastAlerts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPastAlerts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPastAlerts.OptionsView.ShowDetailButtons = False
        Me.INDGvPastAlerts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPastAlerts, False)
        '
        'INDGvPastAlerts_CreationDate
        '
        Me.INDGvPastAlerts_CreationDate.Caption = "Fecha"
        Me.INDGvPastAlerts_CreationDate.FieldName = "CreationDate"
        Me.INDGvPastAlerts_CreationDate.Name = "INDGvPastAlerts_CreationDate"
        Me.INDGvPastAlerts_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvPastAlerts_CreationDate.OptionsColumn.AllowFocus = False
        Me.INDGvPastAlerts_CreationDate.Visible = True
        Me.INDGvPastAlerts_CreationDate.VisibleIndex = 0
        Me.INDGvPastAlerts_CreationDate.Width = 120
        '
        'INDGvPastAlerts_Comments
        '
        Me.INDGvPastAlerts_Comments.Caption = "Comentario"
        Me.INDGvPastAlerts_Comments.FieldName = "Comments"
        Me.INDGvPastAlerts_Comments.Name = "INDGvPastAlerts_Comments"
        Me.INDGvPastAlerts_Comments.OptionsColumn.AllowEdit = False
        Me.INDGvPastAlerts_Comments.OptionsColumn.AllowFocus = False
        Me.INDGvPastAlerts_Comments.Visible = True
        Me.INDGvPastAlerts_Comments.VisibleIndex = 1
        Me.INDGvPastAlerts_Comments.Width = 598
        '
        'INDGvPastAlerts_CreationUser
        '
        Me.INDGvPastAlerts_CreationUser.Caption = "Usuario"
        Me.INDGvPastAlerts_CreationUser.FieldName = "CreationUser"
        Me.INDGvPastAlerts_CreationUser.Name = "INDGvPastAlerts_CreationUser"
        Me.INDGvPastAlerts_CreationUser.OptionsColumn.AllowEdit = False
        Me.INDGvPastAlerts_CreationUser.OptionsColumn.AllowFocus = False
        Me.INDGvPastAlerts_CreationUser.Visible = True
        Me.INDGvPastAlerts_CreationUser.VisibleIndex = 2
        Me.INDGvPastAlerts_CreationUser.Width = 165
        '
        'INDgcCurrentAlerts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcCurrentAlerts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcCurrentAlerts, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcCurrentAlerts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcCurrentAlerts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcCurrentAlerts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcCurrentAlerts, False)
        Me.INDgcCurrentAlerts.Location = New System.Drawing.Point(24, 90)
        Me.INDgcCurrentAlerts.MainView = Me.INDGvCurrentAlerts
        Me.INDgcCurrentAlerts.Name = "INDgcCurrentAlerts"
        Me.INDgcCurrentAlerts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepIcbeStatus})
        Me.INDgcCurrentAlerts.Size = New System.Drawing.Size(901, 456)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcCurrentAlerts, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcCurrentAlerts.TabIndex = 4
        Me.INDgcCurrentAlerts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCurrentAlerts})
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
        Me.INDGvCurrentAlerts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCurrentAlerts_CreationDate, Me.INDGvCurrentAlerts_Comments, Me.INDGvCurrentAlerts_CreationUser})
        Me.INDGvCurrentAlerts.GridControl = Me.INDgcCurrentAlerts
        Me.INDGvCurrentAlerts.Name = "INDGvCurrentAlerts"
        Me.INDGvCurrentAlerts.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDGvCurrentAlerts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCurrentAlerts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCurrentAlerts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCurrentAlerts.OptionsView.ShowDetailButtons = False
        Me.INDGvCurrentAlerts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCurrentAlerts, False)
        '
        'INDGvCurrentAlerts_CreationDate
        '
        Me.INDGvCurrentAlerts_CreationDate.Caption = "Fecha"
        Me.INDGvCurrentAlerts_CreationDate.FieldName = "CreationDate"
        Me.INDGvCurrentAlerts_CreationDate.Name = "INDGvCurrentAlerts_CreationDate"
        Me.INDGvCurrentAlerts_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvCurrentAlerts_CreationDate.OptionsColumn.AllowFocus = False
        Me.INDGvCurrentAlerts_CreationDate.Visible = True
        Me.INDGvCurrentAlerts_CreationDate.VisibleIndex = 0
        Me.INDGvCurrentAlerts_CreationDate.Width = 120
        '
        'INDGvCurrentAlerts_Comments
        '
        Me.INDGvCurrentAlerts_Comments.Caption = "Comentario"
        Me.INDGvCurrentAlerts_Comments.FieldName = "Comments"
        Me.INDGvCurrentAlerts_Comments.Name = "INDGvCurrentAlerts_Comments"
        Me.INDGvCurrentAlerts_Comments.OptionsColumn.AllowEdit = False
        Me.INDGvCurrentAlerts_Comments.OptionsColumn.AllowFocus = False
        Me.INDGvCurrentAlerts_Comments.Visible = True
        Me.INDGvCurrentAlerts_Comments.VisibleIndex = 1
        Me.INDGvCurrentAlerts_Comments.Width = 598
        '
        'INDGvCurrentAlerts_CreationUser
        '
        Me.INDGvCurrentAlerts_CreationUser.Caption = "Usuario"
        Me.INDGvCurrentAlerts_CreationUser.FieldName = "CreationUser"
        Me.INDGvCurrentAlerts_CreationUser.Name = "INDGvCurrentAlerts_CreationUser"
        Me.INDGvCurrentAlerts_CreationUser.Visible = True
        Me.INDGvCurrentAlerts_CreationUser.VisibleIndex = 2
        Me.INDGvCurrentAlerts_CreationUser.Width = 135
        '
        'INDrepIcbeStatus
        '
        Me.INDrepIcbeStatus.AutoHeight = False
        Me.INDrepIcbeStatus.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepIcbeStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Solicitado", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Radicado", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Radicado Pendiente de Autorizacion", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Radicado No Autorizado", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autorizado", 5, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autorizado en Entrega", 6, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autorizado Entregado", 7, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Agendado", 8, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ejecutado", 9, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Facturado", 10, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cancelado", 11, -1)})
        Me.INDrepIcbeStatus.Name = "INDrepIcbeStatus"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDtcgInfo})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(949, 570)
        Me.Root.TextVisible = False
        '
        'INDtcgInfo
        '
        Me.INDtcgInfo.Location = New System.Drawing.Point(0, 0)
        Me.INDtcgInfo.Name = "INDtcgInfo"
        Me.INDtcgInfo.SelectedTabPage = Me.INDlcgCurrentAlerts
        Me.INDtcgInfo.Size = New System.Drawing.Size(929, 550)
        Me.INDtcgInfo.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgCurrentAlerts, Me.INDlcgPastAlerts})
        '
        'INDlcgCurrentAlerts
        '
        Me.INDlcgCurrentAlerts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgCurrentAlerts.AppearanceGroup.Options.UseFont = True
        Me.INDlcgCurrentAlerts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgCurrentAlerts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgCurrentAlerts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCurrentAlerts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgCurrentAlerts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgCurrentAlerts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgCurrentAlerts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCurrentAlerts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgCurrentAlerts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCurrentAlerts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgCurrentAlerts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCurrentAlerts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgCurrentAlerts, False)
        Me.INDlcgCurrentAlerts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCurrentAlerts, Me.INDlyItemAddItem})
        Me.INDlcgCurrentAlerts.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgCurrentAlerts.Name = "INDlcgCurrentAlerts"
        Me.INDlcgCurrentAlerts.Size = New System.Drawing.Size(905, 496)
        Me.INDlcgCurrentAlerts.Text = "Alertas Actuales"
        '
        'INDLciCurrentAlerts
        '
        Me.INDLciCurrentAlerts.Control = Me.INDgcCurrentAlerts
        Me.INDLciCurrentAlerts.Location = New System.Drawing.Point(0, 36)
        Me.INDLciCurrentAlerts.MinSize = New System.Drawing.Size(1, 1)
        Me.INDLciCurrentAlerts.Name = "INDLciCurrentAlerts"
        Me.INDLciCurrentAlerts.Size = New System.Drawing.Size(905, 460)
        Me.INDLciCurrentAlerts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCurrentAlerts.Text = "Alertas"
        Me.INDLciCurrentAlerts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciCurrentAlerts.TextVisible = False
        '
        'INDlyItemAddItem
        '
        Me.INDlyItemAddItem.Control = Me.INDpceAddAlert
        Me.INDlyItemAddItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddItem.MaxSize = New System.Drawing.Size(900, 36)
        Me.INDlyItemAddItem.MinSize = New System.Drawing.Size(900, 36)
        Me.INDlyItemAddItem.Name = "INDlyItemAddItem"
        Me.INDlyItemAddItem.Size = New System.Drawing.Size(905, 36)
        Me.INDlyItemAddItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddItem.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddItem.TextVisible = False
        '
        'INDlcgPastAlerts
        '
        Me.INDlcgPastAlerts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPastAlerts.AppearanceGroup.Options.UseFont = True
        Me.INDlcgPastAlerts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPastAlerts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgPastAlerts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPastAlerts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgPastAlerts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgPastAlerts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgPastAlerts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPastAlerts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgPastAlerts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPastAlerts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgPastAlerts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPastAlerts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgPastAlerts, False)
        Me.INDlcgPastAlerts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciPastAlerts})
        Me.INDlcgPastAlerts.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgPastAlerts.Name = "INDlcgPastAlerts"
        Me.INDlcgPastAlerts.Size = New System.Drawing.Size(905, 496)
        Me.INDlcgPastAlerts.Text = "Alertas Pasadas"
        '
        'INDLciPastAlerts
        '
        Me.INDLciPastAlerts.Control = Me.INDGcPastAlerts
        Me.INDLciPastAlerts.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPastAlerts.Name = "INDLciPastAlerts"
        Me.INDLciPastAlerts.Size = New System.Drawing.Size(905, 496)
        Me.INDLciPastAlerts.Text = "Alertas"
        Me.INDLciPastAlerts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPastAlerts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPastAlerts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciPastAlerts.TextToControlDistance = 0
        Me.INDLciPastAlerts.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmOpenAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 701)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmOpenAlert"
        Me.Opacity = 1.0R
        Me.Tag = "2176"
        Me.Text = "Alertas"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeComments.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyAddAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyAddAlert.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAddAlert.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopupAddAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupAddAlert.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDGcPastAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPastAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtcgInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgPastAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPastAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcCurrentAlerts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCurrentAlerts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvCurrentAlerts_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCurrentAlerts_Comments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepIcbeStatus As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDtcgInfo As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgCurrentAlerts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCurrentAlerts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgPastAlerts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcPastAlerts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPastAlerts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciPastAlerts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvPastAlerts_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvPastAlerts_Comments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvPastAlerts_CreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceAddAlert As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyItemAddItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpopupAddAlert As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyAddAlert As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddAlert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDGvCurrentAlerts_CreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDMeComments As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciComments As DevExpress.XtraLayout.LayoutControlItem
End Class
