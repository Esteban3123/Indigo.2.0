Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmStabilityTable
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmStabilityTable))
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnMedicaments = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcMedicaments = New DevExpress.XtraGrid.GridControl()
        Me.INDviewMedicaments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteStabilityDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStabilityDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygMedicaments = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemMedicaments = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddMedicaments = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.INDgcMedicaments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewMedicaments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteStabilityDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteStabilityDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStabilityDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygMedicaments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMedicaments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddMedicaments, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1498, 603)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1498, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1498, 98)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 594)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDbtnMedicaments)
        Me.INDlyRoot.Controls.Add(Me.INDgcMedicaments)
        Me.INDlyRoot.Controls.Add(Me.INDmemoObservations)
        Me.INDlyRoot.Controls.Add(Me.INDdteStabilityDate)
        Me.INDlyRoot.Controls.Add(Me.INDtxtName)
        Me.INDlyRoot.Controls.Add(Me.INDbtnCode)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1294, 594)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDbtnMedicaments
        '
        Me.INDbtnMedicaments.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnMedicaments.Appearance.Options.UseFont = True
        Me.INDbtnMedicaments.Location = New System.Drawing.Point(438, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnMedicaments, True)
        Me.INDbtnMedicaments.Name = "INDbtnMedicaments"
        Me.INDbtnMedicaments.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnMedicaments.StyleController = Me.INDlyRoot
        Me.INDbtnMedicaments.TabIndex = 4
        Me.INDbtnMedicaments.Text = "Agregar"
        '
        'INDgcMedicaments
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMedicaments, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMedicaments, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMedicaments, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMedicaments, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMedicaments, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMedicaments, False)
        Me.INDgcMedicaments.Location = New System.Drawing.Point(438, 89)
        Me.INDgcMedicaments.MainView = Me.INDviewMedicaments
        Me.INDgcMedicaments.Name = "INDgcMedicaments"
        Me.INDgcMedicaments.Size = New System.Drawing.Size(824, 481)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMedicaments, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcMedicaments.TabIndex = 5
        Me.INDgcMedicaments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewMedicaments})
        '
        'INDviewMedicaments
        '
        Me.INDviewMedicaments.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewMedicaments.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewMedicaments.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewMedicaments.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewMedicaments.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewMedicaments.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewMedicaments.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewMedicaments.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewMedicaments.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewMedicaments.Appearance.Row.Options.UseFont = True
        Me.INDviewMedicaments.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewMedicaments.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewMedicaments.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewMedicaments.GridControl = Me.INDgcMedicaments
        Me.INDviewMedicaments.Name = "INDviewMedicaments"
        Me.INDviewMedicaments.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewMedicaments.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewMedicaments.OptionsView.ShowAutoFilterRow = True
        Me.INDviewMedicaments.OptionsView.ShowDetailButtons = False
        Me.INDviewMedicaments.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewMedicaments, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Medicamento"
        Me.GridColumn1.FieldName = "ATCCodeName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Producto"
        Me.GridColumn2.FieldName = "ProductCodeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tipo Dosis Unitaria"
        Me.GridColumn3.FieldName = "UnitDoseTypeCodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Dosis Permisibles"
        Me.GridColumn4.FieldName = "AllowableDoses"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Observaciones"
        Me.GridColumn5.FieldName = "Observations"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Unidad Medida"
        Me.GridColumn6.FieldName = "MeasurementUnitCodeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        '
        'INDmemoObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoObservations, False)
        Me.INDmemoObservations.EnterMoveNextControl = True
        Me.INDmemoObservations.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoObservations.Name = "INDmemoObservations"
        Me.INDmemoObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmemoObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoObservations.Size = New System.Drawing.Size(386, 70)
        Me.INDmemoObservations.StyleController = Me.INDlyRoot
        Me.INDmemoObservations.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoObservations, 0)
        '
        'INDdteStabilityDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteStabilityDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteStabilityDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteStabilityDate, False)
        Me.INDdteStabilityDate.EditValue = Nothing
        Me.INDdteStabilityDate.EnterMoveNextControl = True
        Me.INDdteStabilityDate.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteStabilityDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteStabilityDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteStabilityDate.Name = "INDdteStabilityDate"
        Me.INDdteStabilityDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteStabilityDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteStabilityDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteStabilityDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteStabilityDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteStabilityDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteStabilityDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteStabilityDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteStabilityDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteStabilityDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteStabilityDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteStabilityDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteStabilityDate.StyleController = Me.INDlyRoot
        Me.INDdteStabilityDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteStabilityDate, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlyRoot
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.MixingStation.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyRoot
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation, Me.INDlygMedicaments})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1294, 594)
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
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemStabilityDate, Me.INDlyItemObservations})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(414, 574)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemStabilityDate
        '
        Me.INDlyItemStabilityDate.AllowHide = False
        Me.INDlyItemStabilityDate.Control = Me.INDdteStabilityDate
        Me.INDlyItemStabilityDate.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemStabilityDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStabilityDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStabilityDate.Name = "INDlyItemStabilityDate"
        Me.INDlyItemStabilityDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemStabilityDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStabilityDate.Text = "Fecha"
        Me.INDlyItemStabilityDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStabilityDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemStabilityDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemStabilityDate.TextToControlDistance = 5
        '
        'INDlyItemObservations
        '
        Me.INDlyItemObservations.Control = Me.INDmemoObservations
        Me.INDlyItemObservations.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemObservations.Name = "INDlyItemObservations"
        Me.INDlyItemObservations.Size = New System.Drawing.Size(390, 341)
        Me.INDlyItemObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservations.Text = "Observaciones"
        Me.INDlyItemObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemObservations.TextToControlDistance = 5
        '
        'INDlygMedicaments
        '
        Me.INDlygMedicaments.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygMedicaments.AppearanceGroup.Options.UseFont = True
        Me.INDlygMedicaments.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygMedicaments.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygMedicaments.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMedicaments.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygMedicaments.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygMedicaments.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygMedicaments.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMedicaments.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygMedicaments.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMedicaments.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygMedicaments.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMedicaments.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygMedicaments, False)
        Me.INDlygMedicaments.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemMedicaments, Me.INDlyItemAddMedicaments})
        Me.INDlygMedicaments.Location = New System.Drawing.Point(414, 0)
        Me.INDlygMedicaments.Name = "INDlygMedicaments"
        Me.INDlygMedicaments.Size = New System.Drawing.Size(860, 574)
        Me.INDlygMedicaments.Text = "Medicamentos"
        '
        'INDlyItemMedicaments
        '
        Me.INDlyItemMedicaments.Control = Me.INDgcMedicaments
        Me.INDlyItemMedicaments.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemMedicaments.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemMedicaments.MinSize = New System.Drawing.Size(828, 1)
        Me.INDlyItemMedicaments.Name = "INDlyItemMedicaments"
        Me.INDlyItemMedicaments.Size = New System.Drawing.Size(836, 485)
        Me.INDlyItemMedicaments.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMedicaments.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemMedicaments.TextVisible = False
        '
        'INDlyItemAddMedicaments
        '
        Me.INDlyItemAddMedicaments.Control = Me.INDbtnMedicaments
        Me.INDlyItemAddMedicaments.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddMedicaments.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddMedicaments.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddMedicaments.Name = "INDlyItemAddMedicaments"
        Me.INDlyItemAddMedicaments.Size = New System.Drawing.Size(836, 36)
        Me.INDlyItemAddMedicaments.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddMedicaments.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddMedicaments.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmStabilityTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1498, 725)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmStabilityTable.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmStabilityTable"
        Me.Opacity = 1.0R
        Me.Tag = "2192"
        Me.Text = "Tabla de Estabilidad"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcMedicaments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewMedicaments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteStabilityDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteStabilityDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStabilityDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygMedicaments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMedicaments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddMedicaments, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteStabilityDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemStabilityDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcMedicaments As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewMedicaments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygMedicaments As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemMedicaments As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnMedicaments As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddMedicaments As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
