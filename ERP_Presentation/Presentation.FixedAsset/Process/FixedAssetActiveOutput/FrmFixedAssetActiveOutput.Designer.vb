Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetActiveOutput
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetActiveOutput))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyActiveOutput = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcAssets = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAssets = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolActivePart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolOutputType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAddAssets = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAssets = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAddAssets = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAssets = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.INDlyActiveOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyActiveOutput.SuspendLayout()
        CType(Me.INDgcAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyActiveOutput)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1493, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1493, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1493, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyActiveOutput
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyActiveOutput
        '
        Me.INDlyActiveOutput.Controls.Add(Me.INDgcAssets)
        Me.INDlyActiveOutput.Controls.Add(Me.INDbtnAddAssets)
        Me.INDlyActiveOutput.Controls.Add(Me.INDmemoObservations)
        Me.INDlyActiveOutput.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyActiveOutput.Controls.Add(Me.INDbtnCode)
        Me.INDlyActiveOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyActiveOutput.Location = New System.Drawing.Point(202, 7)
        Me.INDlyActiveOutput.Name = "INDlyActiveOutput"
        Me.INDlyActiveOutput.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(631, 501, 250, 350)
        Me.INDlyActiveOutput.Root = Me.LayoutControlGroup1
        Me.INDlyActiveOutput.Size = New System.Drawing.Size(1289, 557)
        Me.INDlyActiveOutput.TabIndex = 1
        Me.INDlyActiveOutput.Text = "LayoutControl1"
        '
        'INDgcAssets
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAssets, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAssets, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAssets, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAssets, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAssets, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAssets, False)
        Me.INDgcAssets.Location = New System.Drawing.Point(438, 89)
        Me.INDgcAssets.MainView = Me.INDviewAssets
        Me.INDgcAssets.Name = "INDgcAssets"
        Me.INDgcAssets.Size = New System.Drawing.Size(824, 444)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAssets, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcAssets.TabIndex = 8
        Me.INDgcAssets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAssets})
        '
        'INDviewAssets
        '
        Me.INDviewAssets.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAssets.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewAssets.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAssets.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAssets.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAssets.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAssets.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAssets.Appearance.Row.Options.UseFont = True
        Me.INDviewAssets.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAssets.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAssets.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolActivePart, Me.GridColumn1, Me.GridColumn4, Me.GridColumn2, Me.INDcolOutputType})
        Me.INDviewAssets.GridControl = Me.INDgcAssets
        Me.INDviewAssets.Name = "INDviewAssets"
        Me.INDviewAssets.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAssets.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAssets.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAssets.OptionsView.ShowDetailButtons = False
        Me.INDviewAssets.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAssets, False)
        '
        'INDcolActivePart
        '
        Me.INDcolActivePart.Caption = "Tipo Activo"
        Me.INDcolActivePart.FieldName = "ActiveType"
        Me.INDcolActivePart.Name = "INDcolActivePart"
        Me.INDcolActivePart.OptionsColumn.AllowEdit = False
        Me.INDcolActivePart.OptionsColumn.AllowFocus = False
        Me.INDcolActivePart.Visible = True
        Me.INDcolActivePart.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Activo"
        Me.GridColumn1.FieldName = "PhysicalAssetDescription"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Parte"
        Me.GridColumn4.FieldName = "PhysicalAssetPartDescription"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Cuenta Contable"
        Me.GridColumn2.FieldName = "MainAccountNumberName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'INDcolOutputType
        '
        Me.INDcolOutputType.Caption = "Tipo Salida"
        Me.INDcolOutputType.FieldName = "OutputType"
        Me.INDcolOutputType.Name = "INDcolOutputType"
        Me.INDcolOutputType.OptionsColumn.AllowEdit = False
        Me.INDcolOutputType.OptionsColumn.AllowFocus = False
        Me.INDcolOutputType.Visible = True
        Me.INDcolOutputType.VisibleIndex = 4
        '
        'INDbtnAddAssets
        '
        Me.INDbtnAddAssets.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAssets.Appearance.Options.UseFont = True
        Me.INDbtnAddAssets.Location = New System.Drawing.Point(438, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAssets, True)
        Me.INDbtnAddAssets.Name = "INDbtnAddAssets"
        Me.INDbtnAddAssets.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddAssets.StyleController = Me.INDlyActiveOutput
        Me.INDbtnAddAssets.TabIndex = 7
        Me.INDbtnAddAssets.Text = "Agregar"
        '
        'INDmemoObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoObservations, False)
        Me.INDmemoObservations.EnterMoveNextControl = True
        Me.INDmemoObservations.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoObservations.Name = "INDmemoObservations"
        Me.INDmemoObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmemoObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemoObservations.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemoObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoObservations.Properties.MaxLength = 500
        Me.INDmemoObservations.Size = New System.Drawing.Size(386, 70)
        Me.INDmemoObservations.StyleController = Me.INDlyActiveOutput
        Me.INDmemoObservations.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoObservations, 0)
        '
        'INDdteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.INDdteDocumentDate.EditValue = Nothing
        Me.INDdteDocumentDate.EnterMoveNextControl = True
        Me.INDdteDocumentDate.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteDocumentDate.Name = "INDdteDocumentDate"
        Me.INDdteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteDocumentDate.StyleController = Me.INDlyActiveOutput
        Me.INDdteDocumentDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDocumentDate, 0)
        Me.INDdteDocumentDate.ToolTip = "Este Campo es Necesario"
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
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyActiveOutput
        Me.INDbtnCode.TabIndex = 4
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygAssets})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1289, 557)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPrincipalData
        '
        Me.INDlygPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalData, False)
        Me.INDlygPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDocumentDate, Me.INDlyItemObservations})
        Me.INDlygPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalData.Name = "INDlygPrincipalData"
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 537)
        Me.INDlygPrincipalData.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemDocumentDate
        '
        Me.INDlyItemDocumentDate.Control = Me.INDdteDocumentDate
        Me.INDlyItemDocumentDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemDocumentDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.Name = "INDlyItemDocumentDate"
        Me.INDlyItemDocumentDate.ShowInCustomizationForm = False
        Me.INDlyItemDocumentDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocumentDate.Text = "Fecha Documento"
        Me.INDlyItemDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDocumentDate.TextToControlDistance = 5
        '
        'INDlyItemObservations
        '
        Me.INDlyItemObservations.Control = Me.INDmemoObservations
        Me.INDlyItemObservations.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemObservations.Name = "INDlyItemObservations"
        Me.INDlyItemObservations.Size = New System.Drawing.Size(390, 364)
        Me.INDlyItemObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservations.Text = "Observaciones"
        Me.INDlyItemObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemObservations.TextToControlDistance = 5
        '
        'INDlygAssets
        '
        Me.INDlygAssets.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAssets.AppearanceGroup.Options.UseFont = True
        Me.INDlygAssets.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAssets.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAssets.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAssets.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAssets.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAssets.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAssets.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAssets.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAssets.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAssets.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAssets.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAssets.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAssets, False)
        Me.INDlygAssets.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAddAssets, Me.INDlyItemAssets})
        Me.INDlygAssets.Location = New System.Drawing.Point(414, 0)
        Me.INDlygAssets.Name = "INDlygAssets"
        Me.INDlygAssets.Size = New System.Drawing.Size(855, 537)
        Me.INDlygAssets.Text = "Detalles"
        '
        'INDlyItemAddAssets
        '
        Me.INDlyItemAddAssets.Control = Me.INDbtnAddAssets
        Me.INDlyItemAddAssets.CustomizationFormText = "Agregar Detalles"
        Me.INDlyItemAddAssets.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddAssets.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddAssets.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddAssets.Name = "INDlyItemAddAssets"
        Me.INDlyItemAddAssets.Size = New System.Drawing.Size(831, 36)
        Me.INDlyItemAddAssets.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddAssets.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAddAssets.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddAssets.TextToControlDistance = 0
        Me.INDlyItemAddAssets.TextVisible = False
        '
        'INDlyItemAssets
        '
        Me.INDlyItemAssets.Control = Me.INDgcAssets
        Me.INDlyItemAssets.CustomizationFormText = "Listado de Detalles"
        Me.INDlyItemAssets.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemAssets.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemAssets.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemAssets.Name = "INDlyItemAssets"
        Me.INDlyItemAssets.Size = New System.Drawing.Size(831, 448)
        Me.INDlyItemAssets.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAssets.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAssets.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'FrmFixedAssetActiveOutput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1493, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetActiveOutput"
        Me.Opacity = 1.0R
        Me.Tag = "1778"
        Me.Text = "Salida de Activos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyActiveOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyActiveOutput.ResumeLayout(False)
        CType(Me.INDgcAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyActiveOutput As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDdteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAddAssets As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlygAssets As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemAddAssets As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcAssets As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAssets As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemAssets As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolOutputType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolActivePart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
