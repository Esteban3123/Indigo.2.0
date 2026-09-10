Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAddProductInventoryAdjustments
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDPupCBatchSerial = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrBatchSerial1 = New Presentation.Inventory.CtrBatchSerial()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProduct = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPceBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceProduct = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDTxtUnitValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtUnid = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleConcept = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGleConceptsView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNameConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTypeConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTxtQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPceProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPceBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.s = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLySleConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAddProduct = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CtrNavigationControl1.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDPupCBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCBatchSerial.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleConceptsView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1292, 519)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1292, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1292, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Controls.Add(Me.INDPupCProducto)
        Me.CtrNavigationControl1.Controls.Add(Me.INDPupCBatchSerial)
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 9)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 508)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(19, 388)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(663, 376)
        Me.INDPupCProducto.TabIndex = 13
        '
        'CtrProducts1
        '
        Me.CtrProducts1.DataSource = Nothing
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(663, 376)
        Me.CtrProducts1.TabIndex = 0
        '
        'INDPupCBatchSerial
        '
        Me.INDPupCBatchSerial.Controls.Add(Me.CtrBatchSerial1)
        Me.INDPupCBatchSerial.Location = New System.Drawing.Point(94, 324)
        Me.INDPupCBatchSerial.Name = "INDPupCBatchSerial"
        Me.INDPupCBatchSerial.Size = New System.Drawing.Size(565, 313)
        Me.INDPupCBatchSerial.TabIndex = 15
        '
        'CtrBatchSerial1
        '
        Me.CtrBatchSerial1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrBatchSerial1.Location = New System.Drawing.Point(0, 0)
        Me.CtrBatchSerial1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrBatchSerial1.Name = "CtrBatchSerial1"
        Me.CtrBatchSerial1.Size = New System.Drawing.Size(565, 313)
        Me.CtrBatchSerial1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.INDSleProduct)
        Me.LayoutControl1.Controls.Add(Me.INDPceBatchSerial)
        Me.LayoutControl1.Controls.Add(Me.INDPceProduct)
        Me.LayoutControl1.Controls.Add(Me.INDTxtUnitValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtUnid)
        Me.LayoutControl1.Controls.Add(Me.INDTxtQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDSleConcept)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, False)
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 9)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1088, 472)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Location = New System.Drawing.Point(16, 189)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct.Name = "INDSleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleProduct.Properties.DisplayMember = "CodeNameProduct"
        Me.INDSleProduct.Properties.NullText = ""
        Me.INDSleProduct.Properties.PopupSizeable = False
        Me.INDSleProduct.Properties.PopupView = Me.INDGvProduct
        Me.INDSleProduct.Properties.ShowFooter = False
        Me.INDSleProduct.Properties.ValueMember = "ProductId"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct, True)
        Me.INDSleProduct.Size = New System.Drawing.Size(388, 28)
        Me.INDSleProduct.StyleController = Me.LayoutControl1
        Me.INDSleProduct.TabIndex = 16
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct, "{0} - {1}")
        Me.INDSleProduct.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct, False)
        '
        'INDGvProduct
        '
        Me.INDGvProduct.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.INDGvProduct.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct.Name = "INDGvProduct"
        Me.INDGvProduct.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "ID"
        Me.GridColumn1.FieldName = "ID"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Producto"
        Me.GridColumn2.FieldName = "CodeNameProduct"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Lote"
        Me.GridColumn3.FieldName = "CodeNameBatchSerial"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Fecha Vencimiento"
        Me.GridColumn4.FieldName = "BatchSerialExpiredDate"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Saldo"
        Me.GridColumn5.FieldName = "Quantity"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        '
        'INDPceBatchSerial
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceBatchSerial, Nothing)
        Me.INDPceBatchSerial.Location = New System.Drawing.Point(16, 434)
        Me.INDPceBatchSerial.MinimumSize = New System.Drawing.Size(387, 22)
        Me.INDPceBatchSerial.Name = "INDPceBatchSerial"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDPceBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceBatchSerial.Properties.PopupControl = Me.INDPupCBatchSerial
        Me.INDPceBatchSerial.Properties.PopupSizeable = False
        Me.INDPceBatchSerial.Properties.ShowPopupCloseButton = False
        Me.INDPceBatchSerial.Size = New System.Drawing.Size(388, 28)
        Me.INDPceBatchSerial.StyleController = Me.LayoutControl1
        Me.INDPceBatchSerial.TabIndex = 3
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceBatchSerial, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceBatchSerial, Nothing)
        '
        'INDPceProduct
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProduct, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProduct, Nothing)
        Me.INDPceProduct.Location = New System.Drawing.Point(16, 124)
        Me.INDPceProduct.MinimumSize = New System.Drawing.Size(387, 22)
        Me.INDPceProduct.Name = "INDPceProduct"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProduct, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProduct, False)
        Me.INDPceProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProduct.Properties.Appearance.Options.UseFont = True
        Me.INDPceProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProduct.Properties.PopupControl = Me.INDPupCProducto
        Me.INDPceProduct.Properties.PopupSizeable = False
        Me.INDPceProduct.Properties.ShowPopupCloseButton = False
        Me.INDPceProduct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPceProduct.Size = New System.Drawing.Size(388, 28)
        Me.INDPceProduct.StyleController = Me.LayoutControl1
        Me.INDPceProduct.TabIndex = 0
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceProduct, "304")
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceProduct, Nothing)
        '
        'INDTxtUnitValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnitValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnitValue, False)
        Me.INDTxtUnitValue.Location = New System.Drawing.Point(16, 310)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtUnitValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtUnitValue.Name = "INDTxtUnitValue"
        Me.INDTxtUnitValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtUnitValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnitValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtUnitValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtUnitValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtUnitValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtUnitValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtUnitValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtUnitValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtUnitValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtUnitValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtUnitValue.Properties.ReadOnly = True
        Me.INDTxtUnitValue.Size = New System.Drawing.Size(388, 28)
        Me.INDTxtUnitValue.StyleController = Me.LayoutControl1
        Me.INDTxtUnitValue.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnitValue, 0)
        '
        'INDTxtUnid
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnid, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnid, False)
        Me.INDTxtUnid.Location = New System.Drawing.Point(16, 248)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtUnid, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtUnid.Name = "INDTxtUnid"
        Me.INDTxtUnid.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtUnid.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnid.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtUnid.Properties.Appearance.Options.UseFont = True
        Me.INDTxtUnid.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtUnid.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtUnid.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnid.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtUnid.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtUnid.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtUnid.Properties.ReadOnly = True
        Me.INDTxtUnid.Size = New System.Drawing.Size(388, 28)
        Me.INDTxtUnid.StyleController = Me.LayoutControl1
        Me.INDTxtUnid.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnid, 0)
        '
        'INDTxtQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtQuantity, False)
        Me.INDTxtQuantity.Location = New System.Drawing.Point(16, 372)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtQuantity.Name = "INDTxtQuantity"
        Me.INDTxtQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtQuantity.Properties.MaxLength = 9
        Me.INDTxtQuantity.Size = New System.Drawing.Size(388, 28)
        Me.INDTxtQuantity.StyleController = Me.LayoutControl1
        Me.INDTxtQuantity.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtQuantity, 0)
        '
        'INDSleConcept
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleConcept, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleConcept, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.Location = New System.Drawing.Point(16, 64)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleConcept.Name = "INDSleConcept"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleConcept.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleConcept.Properties.Appearance.Options.UseFont = True
        Me.INDSleConcept.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleConcept.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleConcept.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleConcept.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleConcept.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleConcept.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleConcept.Properties.DisplayMember = "CodeName"
        Me.INDSleConcept.Properties.NullText = ""
        Me.INDSleConcept.Properties.PopupSizeable = False
        Me.INDSleConcept.Properties.PopupView = Me.INDGleConceptsView
        Me.INDSleConcept.Properties.ShowFooter = False
        Me.INDSleConcept.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleConcept, True)
        Me.INDSleConcept.Size = New System.Drawing.Size(388, 28)
        Me.INDSleConcept.StyleController = Me.LayoutControl1
        Me.INDSleConcept.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleConcept, "307")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleConcept, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleConcept, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleConcept, False)
        '
        'INDGleConceptsView
        '
        Me.INDGleConceptsView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGleConceptsView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGleConceptsView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGleConceptsView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGleConceptsView.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGleConceptsView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleConceptsView.Appearance.GroupRow.Options.UseFont = True
        Me.INDGleConceptsView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleConceptsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGleConceptsView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGleConceptsView.Appearance.Row.Options.UseFont = True
        Me.INDGleConceptsView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeConcept, Me.ColNameConcept, Me.ColTypeConcept})
        Me.INDGleConceptsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGleConceptsView.Name = "INDGleConceptsView"
        Me.INDGleConceptsView.OptionsCustomization.AllowGroup = False
        Me.INDGleConceptsView.OptionsDetail.EnableMasterViewMode = False
        Me.INDGleConceptsView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGleConceptsView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGleConceptsView.OptionsView.EnableAppearanceOddRow = True
        Me.INDGleConceptsView.OptionsView.ShowAutoFilterRow = True
        Me.INDGleConceptsView.OptionsView.ShowDetailButtons = False
        Me.INDGleConceptsView.OptionsView.ShowGroupPanel = False
        '
        'ColCodeConcept
        '
        Me.ColCodeConcept.Caption = "Código"
        Me.ColCodeConcept.FieldName = "Code"
        Me.ColCodeConcept.Name = "ColCodeConcept"
        Me.ColCodeConcept.OptionsColumn.AllowEdit = False
        Me.ColCodeConcept.Visible = True
        Me.ColCodeConcept.VisibleIndex = 0
        Me.ColCodeConcept.Width = 348
        '
        'ColNameConcept
        '
        Me.ColNameConcept.Caption = "Nombre"
        Me.ColNameConcept.FieldName = "Name"
        Me.ColNameConcept.Name = "ColNameConcept"
        Me.ColNameConcept.OptionsColumn.AllowEdit = False
        Me.ColNameConcept.Visible = True
        Me.ColNameConcept.VisibleIndex = 1
        Me.ColNameConcept.Width = 736
        '
        'ColTypeConcept
        '
        Me.ColTypeConcept.Caption = "Tipo"
        Me.ColTypeConcept.FieldName = "ConceptTypeName"
        Me.ColTypeConcept.Name = "ColTypeConcept"
        Me.ColTypeConcept.OptionsColumn.AllowEdit = False
        Me.ColTypeConcept.Visible = True
        Me.ColTypeConcept.VisibleIndex = 2
        Me.ColTypeConcept.Width = 228
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1071, 491)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "Producto"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTxtQuantity, Me.INDLyPceProduct, Me.LayoutControlItem3, Me.INDLyPceBatchSerial, Me.s, Me.INDLciProduct, Me.INDLySleConcept})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1057, 477)
        Me.LayoutControlGroup2.Text = "Producto"
        '
        'INDLyTxtQuantity
        '
        Me.INDLyTxtQuantity.Control = Me.INDTxtQuantity
        Me.INDLyTxtQuantity.CustomizationFormText = "LayoutControlItem2"
        Me.INDLyTxtQuantity.Location = New System.Drawing.Point(0, 308)
        Me.INDLyTxtQuantity.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtQuantity.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtQuantity.Name = "INDLyTxtQuantity"
        Me.INDLyTxtQuantity.Size = New System.Drawing.Size(1041, 62)
        Me.INDLyTxtQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtQuantity.Text = "Cantidad"
        Me.INDLyTxtQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtQuantity.TextSize = New System.Drawing.Size(69, 17)
        '
        'INDLyPceProduct
        '
        Me.INDLyPceProduct.Control = Me.INDPceProduct
        Me.INDLyPceProduct.CustomizationFormText = "LayoutControlItem1"
        Me.INDLyPceProduct.Location = New System.Drawing.Point(0, 60)
        Me.INDLyPceProduct.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceProduct.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceProduct.Name = "INDLyPceProduct"
        Me.INDLyPceProduct.Size = New System.Drawing.Size(1041, 62)
        Me.INDLyPceProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceProduct.Text = "Producto"
        Me.INDLyPceProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceProduct.TextSize = New System.Drawing.Size(69, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDTxtUnid
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 184)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 62)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 62)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1041, 62)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Unidad"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(69, 17)
        '
        'INDLyPceBatchSerial
        '
        Me.INDLyPceBatchSerial.Control = Me.INDPceBatchSerial
        Me.INDLyPceBatchSerial.CustomizationFormText = "Lote/Serial"
        Me.INDLyPceBatchSerial.Location = New System.Drawing.Point(0, 370)
        Me.INDLyPceBatchSerial.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceBatchSerial.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceBatchSerial.Name = "INDLyPceBatchSerial"
        Me.INDLyPceBatchSerial.Size = New System.Drawing.Size(1041, 62)
        Me.INDLyPceBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceBatchSerial.Text = "Lote/Serial"
        Me.INDLyPceBatchSerial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceBatchSerial.TextSize = New System.Drawing.Size(69, 17)
        '
        's
        '
        Me.s.Control = Me.INDTxtUnitValue
        Me.s.CustomizationFormText = "LayoutControlItem4"
        Me.s.Location = New System.Drawing.Point(0, 246)
        Me.s.MaxSize = New System.Drawing.Size(390, 62)
        Me.s.MinSize = New System.Drawing.Size(390, 62)
        Me.s.Name = "LayoutControlItem4"
        Me.s.Size = New System.Drawing.Size(1041, 62)
        Me.s.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.s.Text = "V. Unitario"
        Me.s.TextLocation = DevExpress.Utils.Locations.Top
        Me.s.TextSize = New System.Drawing.Size(69, 17)
        '
        'INDLciProduct
        '
        Me.INDLciProduct.AllowHide = False
        Me.INDLciProduct.Control = Me.INDSleProduct
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 122)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.ShowInCustomizationForm = False
        Me.INDLciProduct.Size = New System.Drawing.Size(1041, 62)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Producto"
        Me.INDLciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(69, 17)
        Me.INDLciProduct.TextToControlDistance = 5
        '
        'INDLySleConcept
        '
        Me.INDLySleConcept.Control = Me.INDSleConcept
        Me.INDLySleConcept.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLySleConcept.CustomizationFormText = "Concepto"
        Me.INDLySleConcept.Location = New System.Drawing.Point(0, 0)
        Me.INDLySleConcept.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLySleConcept.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLySleConcept.Name = "INDLySleConcept"
        Me.INDLySleConcept.ShowInCustomizationForm = False
        Me.INDLySleConcept.Size = New System.Drawing.Size(1041, 60)
        Me.INDLySleConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleConcept.Text = "Concepto"
        Me.INDLySleConcept.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLySleConcept.TextSize = New System.Drawing.Size(69, 17)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDTxtUnitValue
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 186)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 62)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 62)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1050, 62)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "V. Unitario"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(69, 17)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAddProduct)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 481)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1088, 36)
        Me.PanelControl1.TabIndex = 2
        '
        'INDBtnAddProduct
        '
        Me.INDBtnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAddProduct.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAddProduct.Name = "INDBtnAddProduct"
        Me.INDBtnAddProduct.Size = New System.Drawing.Size(1084, 32)
        Me.INDBtnAddProduct.TabIndex = 0
        Me.INDBtnAddProduct.Text = "Agregar"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmAddProductInventoryAdjustments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1292, 656)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmAddProductInventoryAdjustments"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Text = "Agregar Producto"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CtrNavigationControl1.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDPupCBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCBatchSerial.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleConceptsView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtUnitValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtUnid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTxtQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAddProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDPceProduct As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLyPceProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPceBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPupCBatchSerial As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrBatchSerial1 As Presentation.Inventory.CtrBatchSerial
    Friend WithEvents INDLyPceBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProducts1 As Presentation.Controls.CtrProducts
    Friend WithEvents INDSleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents s As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleConcept As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGleConceptsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColCodeConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNameConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTypeConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLySleConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
End Class
