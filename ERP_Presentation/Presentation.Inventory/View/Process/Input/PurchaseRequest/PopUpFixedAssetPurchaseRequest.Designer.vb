Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpFixedAssetPurchaseRequest
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSpeQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxeModel = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleTrademark = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvTradeMark = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleFixedAssetItem = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvName = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPncAdd = New DevExpress.XtraEditors.PanelControl()
        Me.INDSmbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciFixedAssetItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTrademark = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciModel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.INDMmeDescription.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDSpeQuantity.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxeModel.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDSleTrademark.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvTradeMark,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDSleFixedAssetItem.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvName,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDPncAdd,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPncAdd.SuspendLayout
        CType(Me.INDLcgBase,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciFixedAssetItem,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciTrademark,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciModel,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciQuantity,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciDescription,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = true
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 598)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDMmeDescription)
        Me.LayoutControl1.Controls.Add(Me.INDSpeQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDTxeModel)
        Me.LayoutControl1.Controls.Add(Me.INDSleTrademark)
        Me.LayoutControl1.Controls.Add(Me.INDSleFixedAssetItem)
        Me.LayoutControl1.Controls.Add(Me.INDPncAdd)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDLcgBase
        Me.LayoutControl1.Size = New System.Drawing.Size(804, 598)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDMmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmeDescription, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmeDescription, false)
        Me.INDMmeDescription.EnterMoveNextControl = true
        Me.INDMmeDescription.Location = New System.Drawing.Point(24, 341)
        Me.INDMmeDescription.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmeDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDMmeDescription.Name = "INDMmeDescription"
        Me.INDMmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDMmeDescription.Properties.Appearance.Options.UseBackColor = true
        Me.INDMmeDescription.Properties.Appearance.Options.UseFont = true
        Me.INDMmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDMmeDescription.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDMmeDescription.Properties.MaxLength = 300
        Me.INDMmeDescription.Size = New System.Drawing.Size(386, 50)
        Me.INDMmeDescription.StyleController = Me.LayoutControl1
        Me.INDMmeDescription.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmeDescription, 0)
        '
        'INDSpeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpeQuantity, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpeQuantity, true)
        Me.INDSpeQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpeQuantity.EnterMoveNextControl = true
        Me.INDSpeQuantity.Location = New System.Drawing.Point(24, 277)
        Me.INDSpeQuantity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpeQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpeQuantity.Name = "INDSpeQuantity"
        Me.INDSpeQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpeQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSpeQuantity.Properties.Appearance.Options.UseBackColor = true
        Me.INDSpeQuantity.Properties.Appearance.Options.UseFont = true
        Me.INDSpeQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSpeQuantity.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDSpeQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpeQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpeQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpeQuantity.Properties.MaxLength = 9
        Me.INDSpeQuantity.Properties.MaxValue = New Decimal(New Integer() {2147483646, 0, 0, 0})
        Me.INDSpeQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSpeQuantity.StyleController = Me.LayoutControl1
        Me.INDSpeQuantity.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpeQuantity, 0)
        '
        'INDTxeModel
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeModel, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeModel, false)
        Me.INDTxeModel.EnterMoveNextControl = true
        Me.INDTxeModel.Location = New System.Drawing.Point(24, 213)
        Me.INDTxeModel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeModel, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxeModel.Name = "INDTxeModel"
        Me.INDTxeModel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeModel.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDTxeModel.Properties.Appearance.Options.UseBackColor = true
        Me.INDTxeModel.Properties.Appearance.Options.UseFont = true
        Me.INDTxeModel.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDTxeModel.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDTxeModel.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxeModel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxeModel.Properties.MaxLength = 100
        Me.INDTxeModel.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeModel.StyleController = Me.LayoutControl1
        Me.INDTxeModel.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeModel, 0)
        '
        'INDSleTrademark
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleTrademark, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleTrademark, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleTrademark, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleTrademark, true)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleTrademark, false)
        Me.INDSleTrademark.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleTrademark, false)
        Me.INDSleTrademark.Location = New System.Drawing.Point(24, 149)
        Me.INDSleTrademark.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleTrademark, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleTrademark.Name = "INDSleTrademark"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleTrademark, false)
        Me.INDSleTrademark.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleTrademark.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSleTrademark.Properties.Appearance.Options.UseBackColor = true
        Me.INDSleTrademark.Properties.Appearance.Options.UseFont = true
        Me.INDSleTrademark.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSleTrademark.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDSleTrademark.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, true)})
        Me.INDSleTrademark.Properties.DisplayMember = "CodeDescription"
        Me.INDSleTrademark.Properties.NullText = ""
        Me.INDSleTrademark.Properties.PopupSizeable = false
        Me.INDSleTrademark.Properties.ShowFooter = false
        Me.INDSleTrademark.Properties.ValueMember = "Id"
        Me.INDSleTrademark.Properties.View = Me.INDGvTradeMark
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleTrademark, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleTrademark, true)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleTrademark, true)
        Me.INDSleTrademark.Size = New System.Drawing.Size(386, 28)
        Me.INDSleTrademark.StyleController = Me.LayoutControl1
        Me.INDSleTrademark.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleTrademark, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleTrademark, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleTrademark, "{0} - {1}")
        Me.INDSleTrademark.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleTrademark, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleTrademark, false)
        '
        'INDGvTradeMark
        '
        Me.INDGvTradeMark.Appearance.ColumnFilterButton.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDGvTradeMark.Appearance.ColumnFilterButton.Options.UseImage = true
        Me.INDGvTradeMark.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDGvTradeMark.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvTradeMark.Name = "INDGvTradeMark"
        Me.INDGvTradeMark.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDGvTradeMark.OptionsView.ShowGroupPanel = false
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Id"
        Me.GridColumn4.FieldName = "Id"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Codigo"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = true
        Me.GridColumn5.VisibleIndex = 0
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Descripcion"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = true
        Me.GridColumn6.VisibleIndex = 1
        '
        'INDSleFixedAssetItem
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleFixedAssetItem, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleFixedAssetItem, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleFixedAssetItem, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleFixedAssetItem, true)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.INDSleFixedAssetItem.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.INDSleFixedAssetItem.Location = New System.Drawing.Point(24, 85)
        Me.INDSleFixedAssetItem.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleFixedAssetItem, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleFixedAssetItem.Name = "INDSleFixedAssetItem"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.INDSleFixedAssetItem.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleFixedAssetItem.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSleFixedAssetItem.Properties.Appearance.Options.UseBackColor = true
        Me.INDSleFixedAssetItem.Properties.Appearance.Options.UseFont = true
        Me.INDSleFixedAssetItem.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSleFixedAssetItem.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDSleFixedAssetItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, true)})
        Me.INDSleFixedAssetItem.Properties.DisplayMember = "CodeDescription"
        Me.INDSleFixedAssetItem.Properties.NullText = ""
        Me.INDSleFixedAssetItem.Properties.PopupSizeable = false
        Me.INDSleFixedAssetItem.Properties.ShowFooter = false
        Me.INDSleFixedAssetItem.Properties.ValueMember = "Id"
        Me.INDSleFixedAssetItem.Properties.View = Me.INDGvName
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleFixedAssetItem, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleFixedAssetItem, true)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleFixedAssetItem, true)
        Me.INDSleFixedAssetItem.Size = New System.Drawing.Size(386, 28)
        Me.INDSleFixedAssetItem.StyleController = Me.LayoutControl1
        Me.INDSleFixedAssetItem.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleFixedAssetItem, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleFixedAssetItem, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleFixedAssetItem, "{0} - {1}")
        Me.INDSleFixedAssetItem.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleFixedAssetItem, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleFixedAssetItem, false)
        '
        'INDGvName
        '
        Me.INDGvName.Appearance.ColumnFilterButton.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDGvName.Appearance.ColumnFilterButton.Options.UseImage = true
        Me.INDGvName.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.INDGvName.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvName.Name = "INDGvName"
        Me.INDGvName.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDGvName.OptionsView.ShowGroupPanel = false
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Id"
        Me.GridColumn1.FieldName = "Id"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = true
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Descripción"
        Me.GridColumn3.FieldName = "Description"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = true
        Me.GridColumn3.VisibleIndex = 1
        '
        'INDPncAdd
        '
        Me.INDPncAdd.Controls.Add(Me.INDSmbAdd)
        Me.INDPncAdd.Location = New System.Drawing.Point(12, 407)
        Me.INDPncAdd.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDPncAdd.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDPncAdd.Name = "INDPncAdd"
        Me.INDPncAdd.Size = New System.Drawing.Size(780, 36)
        Me.INDPncAdd.TabIndex = 10
        '
        'INDSmbAdd
        '
        Me.INDSmbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSmbAdd.Appearance.Options.UseFont = true
        Me.INDSmbAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDSmbAdd.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAdd, true)
        Me.INDSmbAdd.Name = "INDSmbAdd"
        Me.INDSmbAdd.Size = New System.Drawing.Size(776, 32)
        Me.INDSmbAdd.TabIndex = 0
        Me.INDSmbAdd.Text = "Agregar"
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = true
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = true
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, false)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = false
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.INDLciAdd})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(804, 598)
        Me.INDLcgBase.TextVisible = false
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciFixedAssetItem, Me.INDLciTrademark, Me.INDLciModel, Me.INDLciQuantity, Me.INDLciDescription})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(784, 395)
        Me.LayoutControlGroup1.Text = "Artículo"
        '
        'INDLciFixedAssetItem
        '
        Me.INDLciFixedAssetItem.AllowHide = false
        Me.INDLciFixedAssetItem.Control = Me.INDSleFixedAssetItem
        Me.INDLciFixedAssetItem.Location = New System.Drawing.Point(0, 0)
        Me.INDLciFixedAssetItem.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciFixedAssetItem.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciFixedAssetItem.Name = "INDLciFixedAssetItem"
        Me.INDLciFixedAssetItem.ShowInCustomizationForm = false
        Me.INDLciFixedAssetItem.Size = New System.Drawing.Size(760, 64)
        Me.INDLciFixedAssetItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFixedAssetItem.Text = "Nombre"
        Me.INDLciFixedAssetItem.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFixedAssetItem.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFixedAssetItem.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciFixedAssetItem.TextToControlDistance = 5
        '
        'INDLciTrademark
        '
        Me.INDLciTrademark.AllowHide = false
        Me.INDLciTrademark.Control = Me.INDSleTrademark
        Me.INDLciTrademark.Location = New System.Drawing.Point(0, 64)
        Me.INDLciTrademark.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciTrademark.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciTrademark.Name = "INDLciTrademark"
        Me.INDLciTrademark.ShowInCustomizationForm = false
        Me.INDLciTrademark.Size = New System.Drawing.Size(760, 64)
        Me.INDLciTrademark.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTrademark.Text = "Marca"
        Me.INDLciTrademark.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTrademark.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTrademark.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciTrademark.TextToControlDistance = 5
        '
        'INDLciModel
        '
        Me.INDLciModel.Control = Me.INDTxeModel
        Me.INDLciModel.Location = New System.Drawing.Point(0, 128)
        Me.INDLciModel.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciModel.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciModel.Name = "INDLciModel"
        Me.INDLciModel.Size = New System.Drawing.Size(760, 64)
        Me.INDLciModel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciModel.Text = "Modelo"
        Me.INDLciModel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciModel.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciModel.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciModel.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.AllowHide = false
        Me.INDLciQuantity.Control = Me.INDSpeQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 192)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.ShowInCustomizationForm = false
        Me.INDLciQuantity.Size = New System.Drawing.Size(760, 64)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciDescription
        '
        Me.INDLciDescription.Control = Me.INDMmeDescription
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 256)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 80)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 80)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.Size = New System.Drawing.Size(760, 80)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDPncAdd
        Me.INDLciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 395)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(784, 183)
        Me.INDLciAdd.Text = "LayoutControlItem1"
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextVisible = false
        '
        'PopUpFixedAssetPurchaseRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "PopUpFixedAssetPurchaseRequest"
        Me.Opacity = 1R
        Me.Text = "Agregar Activos Fijos"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDMmeDescription.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDSpeQuantity.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxeModel.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDSleTrademark.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvTradeMark,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDSleFixedAssetItem.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvName,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDPncAdd,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPncAdd.ResumeLayout(false)
        CType(Me.INDLcgBase,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciFixedAssetItem,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciTrademark,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciModel,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciQuantity,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciDescription,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDMmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSpeQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxeModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSleTrademark As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvTradeMark As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleFixedAssetItem As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvName As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciFixedAssetItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTrademark As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciModel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents INDPncAdd As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSmbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
