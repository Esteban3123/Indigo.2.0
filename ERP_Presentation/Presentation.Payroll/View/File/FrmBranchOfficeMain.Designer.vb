<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBranchOfficeMain
    Inherits Presentation.Controls.FormBase

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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbNewBO = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcBranchOffice = New DevExpress.XtraGrid.GridControl()
        Me.INDgvBranchOffice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeBO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameBO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAddressBO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPhoneBO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLocationBO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepLinkEdit = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.INDSleCompany = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleGvCompany = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrBranchOffice = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCompany = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBranchOffice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNewBranchOffice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepLinkEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleGvCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNewBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 612)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        Me.BarraBotones.Visible = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSbNewBO)
        Me.LayoutControl1.Controls.Add(Me.INDgcBranchOffice)
        Me.LayoutControl1.Controls.Add(Me.INDSleCompany)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1004, 603)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "INDlyCtrBranchOffice"
        '
        'INDSbNewBO
        '
        Me.INDSbNewBO.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSbNewBO.Appearance.Options.UseFont = True
        Me.INDSbNewBO.Location = New System.Drawing.Point(414, 60)
        Me.INDSbNewBO.Name = "INDSbNewBO"
        Me.INDSbNewBO.Size = New System.Drawing.Size(146, 32)
        Me.INDSbNewBO.StyleController = Me.LayoutControl1
        Me.INDSbNewBO.TabIndex = 6
        Me.INDSbNewBO.Text = "Nuevo"
        '
        'INDgcBranchOffice
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcBranchOffice, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcBranchOffice, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcBranchOffice, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcBranchOffice, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcBranchOffice, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcBranchOffice, False)
        Me.INDgcBranchOffice.Location = New System.Drawing.Point(24, 96)
        Me.INDgcBranchOffice.MainView = Me.INDgvBranchOffice
        Me.INDgcBranchOffice.Name = "INDgcBranchOffice"
        Me.INDgcBranchOffice.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepLinkEdit})
        Me.INDgcBranchOffice.Size = New System.Drawing.Size(956, 483)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcBranchOffice, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcBranchOffice.TabIndex = 5
        Me.INDgcBranchOffice.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvBranchOffice})
        '
        'INDgvBranchOffice
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDgvBranchOffice.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvBranchOffice.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvBranchOffice.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvBranchOffice.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvBranchOffice.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvBranchOffice.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvBranchOffice.Appearance.Row.Options.UseFont = True
        Me.INDgvBranchOffice.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvBranchOffice.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvBranchOffice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeBO, Me.INDColNameBO, Me.INDColAddressBO, Me.INDColPhoneBO, Me.INDColLocationBO, Me.INDColStatus, Me.INDColActions})
        Me.INDgvBranchOffice.GridControl = Me.INDgcBranchOffice
        Me.INDgvBranchOffice.Name = "INDgvBranchOffice"
        Me.INDgvBranchOffice.OptionsCustomization.AllowColumnMoving = False
        Me.INDgvBranchOffice.OptionsCustomization.AllowGroup = False
        Me.INDgvBranchOffice.OptionsDetail.AllowZoomDetail = False
        Me.INDgvBranchOffice.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvBranchOffice.OptionsDetail.ShowDetailTabs = False
        Me.INDgvBranchOffice.OptionsDetail.SmartDetailExpand = False
        Me.INDgvBranchOffice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvBranchOffice.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvBranchOffice.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvBranchOffice.OptionsView.ShowAutoFilterRow = True
        Me.INDgvBranchOffice.OptionsView.ShowGroupPanel = False
        Me.INDgvBranchOffice.Tag = 459
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvBranchOffice, False)
        '
        'INDColCodeBO
        '
        Me.INDColCodeBO.Caption = "Código"
        Me.INDColCodeBO.FieldName = "Code"
        Me.INDColCodeBO.Name = "INDColCodeBO"
        Me.INDColCodeBO.OptionsColumn.AllowEdit = False
        Me.INDColCodeBO.Visible = True
        Me.INDColCodeBO.VisibleIndex = 0
        '
        'INDColNameBO
        '
        Me.INDColNameBO.Caption = "Nombre"
        Me.INDColNameBO.FieldName = "Name"
        Me.INDColNameBO.Name = "INDColNameBO"
        Me.INDColNameBO.OptionsColumn.AllowEdit = False
        Me.INDColNameBO.Visible = True
        Me.INDColNameBO.VisibleIndex = 1
        '
        'INDColAddressBO
        '
        Me.INDColAddressBO.Caption = "Dirección"
        Me.INDColAddressBO.FieldName = "Address"
        Me.INDColAddressBO.Name = "INDColAddressBO"
        Me.INDColAddressBO.OptionsColumn.AllowEdit = False
        Me.INDColAddressBO.Visible = True
        Me.INDColAddressBO.VisibleIndex = 2
        '
        'INDColPhoneBO
        '
        Me.INDColPhoneBO.Caption = "Teléfono"
        Me.INDColPhoneBO.FieldName = "Telephone"
        Me.INDColPhoneBO.Name = "INDColPhoneBO"
        Me.INDColPhoneBO.OptionsColumn.AllowEdit = False
        Me.INDColPhoneBO.Visible = True
        Me.INDColPhoneBO.VisibleIndex = 3
        '
        'INDColLocationBO
        '
        Me.INDColLocationBO.Caption = "Ubicación"
        Me.INDColLocationBO.FieldName = "Location"
        Me.INDColLocationBO.Name = "INDColLocationBO"
        Me.INDColLocationBO.OptionsColumn.AllowEdit = False
        Me.INDColLocationBO.Visible = True
        Me.INDColLocationBO.VisibleIndex = 4
        '
        'INDColStatus
        '
        Me.INDColStatus.Caption = "Activo"
        Me.INDColStatus.FieldName = "State"
        Me.INDColStatus.Name = "INDColStatus"
        Me.INDColStatus.OptionsColumn.AllowEdit = False
        Me.INDColStatus.Visible = True
        Me.INDColStatus.VisibleIndex = 5
        '
        'INDColActions
        '
        Me.INDColActions.AppearanceCell.Options.UseTextOptions = True
        Me.INDColActions.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColActions.Caption = "Acciones"
        Me.INDColActions.ColumnEdit = Me.RepLinkEdit
        Me.INDColActions.Name = "INDColActions"
        Me.INDColActions.Visible = True
        Me.INDColActions.VisibleIndex = 6
        '
        'RepLinkEdit
        '
        Me.RepLinkEdit.AutoHeight = False
        Me.RepLinkEdit.Caption = "Editar"
        Me.RepLinkEdit.Name = "RepLinkEdit"
        '
        'INDSleCompany
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCompany, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCompany, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCompany, False)
        Me.INDSleCompany.Location = New System.Drawing.Point(156, 60)
        Me.INDSleCompany.Name = "INDSleCompany"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCompany, False)
        Me.INDSleCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCompany.Properties.Appearance.Options.UseFont = True
        Me.INDSleCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleCompany.Properties.DisplayMember = "Descripcion"
        Me.INDSleCompany.Properties.NullText = ""
        Me.INDSleCompany.Properties.PopupSizeable = False
        Me.INDSleCompany.Properties.ShowFooter = False
        Me.INDSleCompany.Properties.ValueMember = "Codigo"
        Me.INDSleCompany.Properties.View = Me.INDSleGvCompany
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCompany, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCompany, True)
        Me.INDSleCompany.Size = New System.Drawing.Size(254, 28)
        Me.INDSleCompany.StyleController = Me.LayoutControl1
        Me.INDSleCompany.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCompany, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCompany, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCompany, False)
        '
        'INDSleGvCompany
        '
        Me.INDSleGvCompany.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleGvCompany.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDSleGvCompany.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleGvCompany.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDSleGvCompany.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSleGvCompany.Appearance.Row.Options.UseFont = True
        Me.INDSleGvCompany.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCode, Me.INDColDescription})
        Me.INDSleGvCompany.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSleGvCompany.Name = "INDSleGvCompany"
        Me.INDSleGvCompany.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSleGvCompany.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSleGvCompany.OptionsView.EnableAppearanceOddRow = True
        Me.INDSleGvCompany.OptionsView.ShowAutoFilterRow = True
        Me.INDSleGvCompany.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDSleGvCompany, False)
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "Codigo"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Descripción"
        Me.INDColDescription.FieldName = "Descripcion"
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrBranchOffice})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1004, 603)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrBranchOffice
        '
        Me.INDlyGrBranchOffice.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGrBranchOffice.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrBranchOffice.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrBranchOffice.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrBranchOffice.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBranchOffice.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrBranchOffice.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGrBranchOffice.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrBranchOffice.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBranchOffice.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrBranchOffice.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGrBranchOffice.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrBranchOffice.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBranchOffice.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrBranchOffice, False)
        Me.INDlyGrBranchOffice.CustomizationFormText = "Sucursales"
        Me.INDlyGrBranchOffice.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCompany, Me.INDlyItemBranchOffice, Me.INDlyItemNewBranchOffice})
        Me.INDlyGrBranchOffice.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrBranchOffice.Name = "INDlyGrBranchOffice"
        Me.INDlyGrBranchOffice.Size = New System.Drawing.Size(984, 583)
        Me.INDlyGrBranchOffice.Text = "Sedes o Sucursales"
        '
        'INDlyItemCompany
        '
        Me.INDlyItemCompany.Control = Me.INDSleCompany
        Me.INDlyItemCompany.CustomizationFormText = "Empresa"
        Me.INDlyItemCompany.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCompany.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCompany.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCompany.Name = "INDlyItemCompany"
        Me.INDlyItemCompany.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCompany.Text = "Empresa"
        Me.INDlyItemCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCompany.TextSize = New System.Drawing.Size(120, 21)
        Me.INDlyItemCompany.TextToControlDistance = 12
        '
        'INDlyItemBranchOffice
        '
        Me.INDlyItemBranchOffice.Control = Me.INDgcBranchOffice
        Me.INDlyItemBranchOffice.CustomizationFormText = "LayoutControlItem2"
        Me.INDlyItemBranchOffice.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemBranchOffice.MinSize = New System.Drawing.Size(203, 24)
        Me.INDlyItemBranchOffice.Name = "INDlyItemBranchOffice"
        Me.INDlyItemBranchOffice.Size = New System.Drawing.Size(960, 487)
        Me.INDlyItemBranchOffice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBranchOffice.Text = "INDlyItemBranchOffice"
        Me.INDlyItemBranchOffice.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemBranchOffice.TextToControlDistance = 0
        Me.INDlyItemBranchOffice.TextVisible = False
        '
        'INDlyItemNewBranchOffice
        '
        Me.INDlyItemNewBranchOffice.Control = Me.INDSbNewBO
        Me.INDlyItemNewBranchOffice.CustomizationFormText = "INDlyItemNewBranchOffice"
        Me.INDlyItemNewBranchOffice.Location = New System.Drawing.Point(390, 0)
        Me.INDlyItemNewBranchOffice.MaxSize = New System.Drawing.Size(150, 36)
        Me.INDlyItemNewBranchOffice.MinSize = New System.Drawing.Size(150, 36)
        Me.INDlyItemNewBranchOffice.Name = "INDlyItemNewBranchOffice"
        Me.INDlyItemNewBranchOffice.Size = New System.Drawing.Size(570, 36)
        Me.INDlyItemNewBranchOffice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNewBranchOffice.Text = "INDlyItemNewBranchOffice"
        Me.INDlyItemNewBranchOffice.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemNewBranchOffice.TextToControlDistance = 0
        Me.INDlyItemNewBranchOffice.TextVisible = False
        '
        'FrmBranchOfficeMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmBranchOfficeMain"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "520"
        Me.Text = "Sucursales"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepLinkEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleGvCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNewBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcBranchOffice As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDgvBranchOffice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDSleCompany As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDSleGvCompany As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGrBranchOffice As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemBranchOffice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCodeBO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameBO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAddressBO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPhoneBO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLocationBO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepLinkEdit As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents INDSbNewBO As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemNewBranchOffice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColStatus As DevExpress.XtraGrid.Columns.GridColumn
End Class
