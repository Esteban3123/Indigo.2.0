<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccessories
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
        Me.components = New System.ComponentModel.Container()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrAccessories = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcListEquipmentType = New DevExpress.XtraGrid.GridControl()
        Me.INDgcListEquipmentTypeView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDglEquipmentType = New Presentation.Controls.EquipmentTypeSearch()
        Me.INDlyKinship = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.Accesorios = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCodeAccessories = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemNameAccessories = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgEquipmentType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEquipmentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemListEquipmentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.INDColInventoryType = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDColCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrAccessories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrAccessories.SuspendLayout()
        CType(Me.INDgcListEquipmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcListEquipmentTypeView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyKinship, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Accesorios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodeAccessories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemNameAccessories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgEquipmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEquipmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemListEquipmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrAccessories)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1183, 536)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1183, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1183, 98)
        '
        'INDLyCtrAccessories
        '
        Me.INDLyCtrAccessories.AllowCustomization = False
        Me.INDLyCtrAccessories.Controls.Add(Me.INDbtnAgregar)
        Me.INDLyCtrAccessories.Controls.Add(Me.INDgcListEquipmentType)
        Me.INDLyCtrAccessories.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrAccessories.Controls.Add(Me.INDBteCode)
        Me.INDLyCtrAccessories.Controls.Add(Me.INDglEquipmentType)
        Me.INDLyCtrAccessories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrAccessories, False)
        Me.INDLyCtrAccessories.Location = New System.Drawing.Point(202, 7)
        Me.INDLyCtrAccessories.Name = "INDLyCtrAccessories"
        Me.INDLyCtrAccessories.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrAccessories.Root = Me.INDlyKinship
        Me.INDLyCtrAccessories.Size = New System.Drawing.Size(979, 527)
        Me.INDLyCtrAccessories.TabIndex = 0
        Me.INDLyCtrAccessories.Text = "LayoutControl1"
        '
        'INDbtnAgregar
        '
        Me.INDbtnAgregar.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbtnAgregar.Appearance.Options.UseFont = True
        Me.INDbtnAgregar.Location = New System.Drawing.Point(889, 49)
        Me.INDbtnAgregar.Name = "INDbtnAgregar"
        Me.INDbtnAgregar.Size = New System.Drawing.Size(76, 28)
        Me.INDbtnAgregar.StyleController = Me.INDLyCtrAccessories
        Me.INDbtnAgregar.TabIndex = 20
        Me.INDbtnAgregar.Text = "Agregar"
        '
        'INDgcListEquipmentType
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcListEquipmentType, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcListEquipmentType, Nothing)
        Me.INDgcListEquipmentType.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.INDgcListEquipmentType, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcListEquipmentType, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcListEquipmentType, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcListEquipmentType, False)
        Me.INDgcListEquipmentType.Location = New System.Drawing.Point(458, 85)
        Me.INDgcListEquipmentType.MainView = Me.INDgcListEquipmentTypeView
        Me.INDgcListEquipmentType.Name = "INDgcListEquipmentType"
        Me.INDgcListEquipmentType.Size = New System.Drawing.Size(507, 428)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcListEquipmentType, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcListEquipmentType.TabIndex = 7
        Me.INDgcListEquipmentType.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcListEquipmentTypeView})
        '
        'INDgcListEquipmentTypeView
        '
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgcListEquipmentTypeView.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgcListEquipmentTypeView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgcListEquipmentTypeView.Appearance.Row.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgcListEquipmentTypeView.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDgcListEquipmentTypeView.GridControl = Me.INDgcListEquipmentType
        Me.INDgcListEquipmentTypeView.Name = "INDgcListEquipmentTypeView"
        Me.INDgcListEquipmentTypeView.OptionsDetail.EnableMasterViewMode = False
        Me.INDgcListEquipmentTypeView.OptionsDetail.ShowDetailTabs = False
        Me.INDgcListEquipmentTypeView.OptionsDetail.SmartDetailExpand = False
        Me.INDgcListEquipmentTypeView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcListEquipmentTypeView.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcListEquipmentTypeView.OptionsView.ShowAutoFilterRow = True
        Me.INDgcListEquipmentTypeView.OptionsView.ShowGroupPanel = False
        Me.INDgcListEquipmentTypeView.Tag = 344
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcListEquipmentTypeView, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Codigo"
        Me.GridColumn3.FieldName = "EquipmentTypeCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 133
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripcion"
        Me.GridColumn4.FieldName = "EquipmentTypeName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 356
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        Me.INDTxtName.Location = New System.Drawing.Point(186, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDTxtName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtName.Properties.MaxLength = 100
        Me.INDTxtName.Size = New System.Drawing.Size(244, 28)
        Me.INDTxtName.StyleController = Me.INDLyCtrAccessories
        Me.INDTxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        Me.INDTxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.INDBteCode.Location = New System.Drawing.Point(186, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDBteCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDBteCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(244, 28)
        Me.INDBteCode.StyleController = Me.INDLyCtrAccessories
        Me.INDBteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        Me.INDBteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDglEquipmentType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglEquipmentType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglEquipmentType, False)
        Me.INDglEquipmentType.EnterMoveNextControl = True
        Me.INDglEquipmentType.Location = New System.Drawing.Point(630, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDglEquipmentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglEquipmentType.MaximumSize = New System.Drawing.Size(244, 0)
        Me.INDglEquipmentType.Name = "INDglEquipmentType"
        Me.INDglEquipmentType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDglEquipmentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDglEquipmentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDglEquipmentType.Properties.NullText = ""
        Me.INDglEquipmentType.Properties.PopupFormMinSize = New System.Drawing.Size(500, 0)
        Me.INDglEquipmentType.Size = New System.Drawing.Size(244, 20)
        Me.INDglEquipmentType.StyleController = Me.INDLyCtrAccessories
        Me.INDglEquipmentType.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglEquipmentType, 0)
        '
        'INDlyKinship
        '
        Me.INDlyKinship.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyKinship.AppearanceGroup.Options.UseFont = True
        Me.INDlyKinship.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyKinship.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyKinship.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyKinship.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyKinship.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyKinship.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyKinship.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyKinship.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyKinship.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyKinship.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyKinship.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyKinship.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyKinship, False)
        Me.INDlyKinship.CustomizationFormText = "Accesorios"
        Me.INDlyKinship.GroupBordersVisible = False
        Me.INDlyKinship.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.Accesorios, Me.INDLcgEquipmentType})
        Me.INDlyKinship.Location = New System.Drawing.Point(0, 0)
        Me.INDlyKinship.Name = "Root"
        Me.INDlyKinship.Size = New System.Drawing.Size(979, 527)
        Me.INDlyKinship.TextVisible = False
        '
        'Accesorios
        '
        Me.Accesorios.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Accesorios.AppearanceGroup.Options.UseFont = True
        Me.Accesorios.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Accesorios.AppearanceItemCaption.Options.UseFont = True
        Me.Accesorios.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Accesorios.AppearanceTabPage.Header.Options.UseFont = True
        Me.Accesorios.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Accesorios.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Accesorios.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Accesorios.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Accesorios.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Accesorios.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Accesorios.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Accesorios.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Accesorios, False)
        Me.Accesorios.CustomizationFormText = "Datos Principales"
        Me.Accesorios.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCodeAccessories, Me.INDLyItemNameAccessories})
        Me.Accesorios.Location = New System.Drawing.Point(0, 0)
        Me.Accesorios.Name = "Accesorios"
        Me.Accesorios.Size = New System.Drawing.Size(444, 527)
        Me.Accesorios.Text = "Datos Principales"
        '
        'INDlyItemCodeAccessories
        '
        Me.INDlyItemCodeAccessories.Control = Me.INDBteCode
        Me.INDlyItemCodeAccessories.CustomizationFormText = "Código"
        Me.INDlyItemCodeAccessories.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCodeAccessories.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeAccessories.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeAccessories.Name = "INDlyItemCodeAccessories"
        Me.INDlyItemCodeAccessories.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeAccessories.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodeAccessories.Tag = "Code"
        Me.INDlyItemCodeAccessories.Text = "Código"
        Me.INDlyItemCodeAccessories.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodeAccessories.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCodeAccessories.TextToControlDistance = 12
        '
        'INDLyItemNameAccessories
        '
        Me.INDLyItemNameAccessories.Control = Me.INDTxtName
        Me.INDLyItemNameAccessories.CustomizationFormText = "Nombre"
        Me.INDLyItemNameAccessories.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemNameAccessories.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNameAccessories.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNameAccessories.Name = "INDLyItemNameAccessories"
        Me.INDLyItemNameAccessories.Size = New System.Drawing.Size(420, 432)
        Me.INDLyItemNameAccessories.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemNameAccessories.Tag = "Name"
        Me.INDLyItemNameAccessories.Text = "Nombre"
        Me.INDLyItemNameAccessories.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemNameAccessories.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLyItemNameAccessories.TextToControlDistance = 12
        '
        'INDLcgEquipmentType
        '
        Me.INDLcgEquipmentType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgEquipmentType.AppearanceGroup.Options.UseFont = True
        Me.INDLcgEquipmentType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgEquipmentType.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgEquipmentType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgEquipmentType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgEquipmentType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgEquipmentType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgEquipmentType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgEquipmentType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgEquipmentType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgEquipmentType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgEquipmentType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgEquipmentType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgEquipmentType, False)
        Me.INDLcgEquipmentType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEquipmentType, Me.INDlyItemListEquipmentType, Me.LayoutControlItem1})
        Me.INDLcgEquipmentType.Location = New System.Drawing.Point(444, 0)
        Me.INDLcgEquipmentType.Name = "INDLcgEquipmentType"
        Me.INDLcgEquipmentType.Size = New System.Drawing.Size(535, 527)
        Me.INDLcgEquipmentType.Text = "Tipos de Equipo"
        '
        'INDlyItemEquipmentType
        '
        Me.INDlyItemEquipmentType.Control = Me.INDglEquipmentType
        Me.INDlyItemEquipmentType.CustomizationFormText = "Tipo Equipo"
        Me.INDlyItemEquipmentType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEquipmentType.MaxSize = New System.Drawing.Size(431, 36)
        Me.INDlyItemEquipmentType.MinSize = New System.Drawing.Size(431, 36)
        Me.INDlyItemEquipmentType.Name = "INDlyItemEquipmentType"
        Me.INDlyItemEquipmentType.Size = New System.Drawing.Size(431, 36)
        Me.INDlyItemEquipmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEquipmentType.Text = "Tipo Equipo"
        Me.INDlyItemEquipmentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEquipmentType.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemEquipmentType.TextToControlDistance = 12
        '
        'INDlyItemListEquipmentType
        '
        Me.INDlyItemListEquipmentType.Control = Me.INDgcListEquipmentType
        Me.INDlyItemListEquipmentType.CustomizationFormText = "Listado de Tipos de Equipo"
        Me.INDlyItemListEquipmentType.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemListEquipmentType.MaxSize = New System.Drawing.Size(511, 0)
        Me.INDlyItemListEquipmentType.MinSize = New System.Drawing.Size(511, 24)
        Me.INDlyItemListEquipmentType.Name = "INDlyItemListEquipmentType"
        Me.INDlyItemListEquipmentType.Size = New System.Drawing.Size(511, 432)
        Me.INDlyItemListEquipmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemListEquipmentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemListEquipmentType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemListEquipmentType.TextToControlDistance = 0
        Me.INDlyItemListEquipmentType.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDbtnAgregar
        Me.LayoutControlItem1.CustomizationFormText = "Agregar"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(431, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(80, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'TreeListLookUpEdit1TreeList
        '
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDColInventoryType, Me.INDColCode, Me.INDColName})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.TreeListLookUpEdit1TreeList, False)
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        Me.TreeListLookUpEdit1TreeList.Location = New System.Drawing.Point(0, 0)
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = True
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "IdParent"
        Me.TreeListLookUpEdit1TreeList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.TreeListLookUpEdit1TreeList.Size = New System.Drawing.Size(400, 200)
        Me.TreeListLookUpEdit1TreeList.TabIndex = 0
        '
        'INDColInventoryType
        '
        Me.INDColInventoryType.Caption = "Tipo de Inventario"
        Me.INDColInventoryType.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.INDColInventoryType.FieldName = "InventoryType"
        Me.INDColInventoryType.MinWidth = 30
        Me.INDColInventoryType.Name = "INDColInventoryType"
        Me.INDColInventoryType.OptionsColumn.AllowEdit = False
        Me.INDColInventoryType.Visible = True
        Me.INDColInventoryType.VisibleIndex = 0
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Biomédico", "1", -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Infraestructura", "2", -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Industriales", "3", -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Equipos de Oficina", "4", -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Codigo"
        Me.INDColCode.FieldName = "Code"
        Me.INDColCode.MinWidth = 30
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 1
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.MinWidth = 30
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 2
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCtrAccessories
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 527)
        Me.CtrNavigationControl1.TabIndex = 1
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmAccessories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1183, 658)
        Me.Name = "FrmAccessories"
        Me.Opacity = 1.0R
        Me.Tag = "565"
        Me.Text = "Accesorios"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrAccessories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrAccessories.ResumeLayout(False)
        CType(Me.INDgcListEquipmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcListEquipmentTypeView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyKinship, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Accesorios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodeAccessories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemNameAccessories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgEquipmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEquipmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemListEquipmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrAccessories As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyKinship As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents Accesorios As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCodeAccessories As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemNameAccessories As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDgcListEquipmentType As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcListEquipmentTypeView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemListEquipmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEquipmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDglEquipmentType As Presentation.Controls.EquipmentTypeSearch
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDColInventoryType As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLcgEquipmentType As DevExpress.XtraLayout.LayoutControlGroup
End Class
