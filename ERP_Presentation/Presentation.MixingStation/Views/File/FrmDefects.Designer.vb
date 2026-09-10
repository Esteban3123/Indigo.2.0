#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDefects
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDcncUnitDoseType = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlycBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSpinWeight = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsleddlDosis = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleddlCategory = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDgcUnitDoseType = New DevExpress.XtraGrid.GridControl()
        Me.INDviewUnitDosesType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleddlType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView32 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrgProductionChemical = New DevExpress.XtraEditors.RadioGroup()
        Me.INDrgQualityChemical = New DevExpress.XtraEditors.RadioGroup()
        Me.INDlycBaseUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrUnitDoseType1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyProductionChemical = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyQualityChemical = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciUnitDoseType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView3 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit31 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit22 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit32 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcncUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycBase.SuspendLayout()
        CType(Me.INDSpinWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleddlDosis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleddlCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewUnitDosesType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleddlType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrgProductionChemical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrgQualityChemical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrUnitDoseType1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyProductionChemical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyQualityChemical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit32, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDcncUnitDoseType)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1471, 601)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1471, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1471, 130)
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDcncUnitDoseType
        '
        Me.INDcncUnitDoseType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDcncUnitDoseType.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDcncUnitDoseType.LayoutControl = Me.INDlycBase
        Me.INDcncUnitDoseType.Location = New System.Drawing.Point(2, 7)
        Me.INDcncUnitDoseType.Margin = New System.Windows.Forms.Padding(0)
        Me.INDcncUnitDoseType.Name = "INDcncUnitDoseType"
        Me.INDcncUnitDoseType.Size = New System.Drawing.Size(200, 592)
        Me.INDcncUnitDoseType.TabIndex = 2
        Me.INDcncUnitDoseType.UseDisabledStatePainter = False
        '
        'INDlycBase
        '
        Me.INDlycBase.Controls.Add(Me.INDSpinWeight)
        Me.INDlycBase.Controls.Add(Me.INDsleddlDosis)
        Me.INDlycBase.Controls.Add(Me.INDsleddlCategory)
        Me.INDlycBase.Controls.Add(Me.INDtxtNombre)
        Me.INDlycBase.Controls.Add(Me.INDbtnCode)
        Me.INDlycBase.Controls.Add(Me.INDgcUnitDoseType)
        Me.INDlycBase.Controls.Add(Me.INDbtnAdd)
        Me.INDlycBase.Controls.Add(Me.INDsleddlType)
        Me.INDlycBase.Controls.Add(Me.INDrgProductionChemical)
        Me.INDlycBase.Controls.Add(Me.INDrgQualityChemical)
        Me.INDlycBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycBase.Location = New System.Drawing.Point(202, 7)
        Me.INDlycBase.Name = "INDlycBase"
        Me.INDlycBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(434, 310, 1201, 569)
        Me.INDlycBase.Root = Me.INDlycBaseUnitDoseType
        Me.INDlycBase.Size = New System.Drawing.Size(1267, 592)
        Me.INDlycBase.TabIndex = 3
        Me.INDlycBase.Text = "LayoutControl1"
        '
        'INDSpinWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpinWeight, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpinWeight, False)
        Me.INDSpinWeight.CausesValidation = False
        Me.INDSpinWeight.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSpinWeight.Location = New System.Drawing.Point(24, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpinWeight, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpinWeight.Name = "INDSpinWeight"
        Me.INDSpinWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpinWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSpinWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpinWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpinWeight.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpinWeight.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpinWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpinWeight.Properties.MaxLength = 4
        Me.INDSpinWeight.Properties.MaxValue = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.INDSpinWeight.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSpinWeight.Size = New System.Drawing.Size(376, 28)
        Me.INDSpinWeight.StyleController = Me.INDlycBase
        Me.INDSpinWeight.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpinWeight, 0)
        '
        'INDsleddlDosis
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleddlDosis, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleddlDosis, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleddlDosis, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleddlDosis, False)
        Me.INDsleddlDosis.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleddlDosis, False)
        Me.INDsleddlDosis.Location = New System.Drawing.Point(1027, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleddlDosis, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleddlDosis.Name = "INDsleddlDosis"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleddlDosis, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleddlDosis, False)
        Me.INDsleddlDosis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleddlDosis.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleddlDosis.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleddlDosis.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleddlDosis.Properties.Appearance.Options.UseFont = True
        Me.INDsleddlDosis.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleddlDosis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleddlDosis.Properties.DisplayMember = "CodeDescription"
        Me.INDsleddlDosis.Properties.NullText = ""
        Me.INDsleddlDosis.Properties.PopupSizeable = False
        Me.INDsleddlDosis.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleddlDosis.Properties.ShowFooter = False
        Me.INDsleddlDosis.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleddlDosis, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleddlDosis, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleddlDosis, True)
        Me.INDsleddlDosis.Size = New System.Drawing.Size(141, 28)
        Me.INDsleddlDosis.StyleController = Me.INDlycBase
        Me.INDsleddlDosis.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleddlDosis, "2067")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleddlDosis, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleddlDosis, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleddlDosis, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn12})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 355
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Descripción"
        Me.GridColumn12.FieldName = "Description"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        Me.GridColumn12.Width = 1027
        '
        'INDsleddlCategory
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleddlCategory, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleddlCategory, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleddlCategory, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleddlCategory, False)
        Me.INDsleddlCategory.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleddlCategory, False)
        Me.INDsleddlCategory.Location = New System.Drawing.Point(24, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleddlCategory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleddlCategory.MaximumSize = New System.Drawing.Size(386, 0)
        Me.INDsleddlCategory.Name = "INDsleddlCategory"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleddlCategory, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleddlCategory, False)
        Me.INDsleddlCategory.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDsleddlCategory.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleddlCategory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleddlCategory.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleddlCategory.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleddlCategory.Properties.Appearance.Options.UseFont = True
        Me.INDsleddlCategory.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleddlCategory.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleddlCategory.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleddlCategory.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleddlCategory.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleddlCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleddlCategory.Properties.DisplayMember = "Description"
        Me.INDsleddlCategory.Properties.NullText = ""
        Me.INDsleddlCategory.Properties.PopupSizeable = False
        Me.INDsleddlCategory.Properties.PopupView = Me.GridView3
        Me.INDsleddlCategory.Properties.ShowFooter = False
        Me.INDsleddlCategory.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleddlCategory, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleddlCategory, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleddlCategory, True)
        Me.INDsleddlCategory.Size = New System.Drawing.Size(376, 28)
        Me.INDsleddlCategory.StyleController = Me.INDlycBase
        Me.INDsleddlCategory.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleddlCategory, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleddlCategory, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleddlCategory, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleddlCategory, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleddlCategory, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Codigo"
        Me.GridColumn4.FieldName = "Code"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Categoria"
        Me.GridColumn5.FieldName = "Description"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        '
        'INDtxtNombre
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNombre, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNombre, False)
        Me.INDtxtNombre.EnterMoveNextControl = True
        Me.INDtxtNombre.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNombre, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtNombre.Name = "INDtxtNombre"
        Me.INDtxtNombre.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtNombre.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNombre.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNombre.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNombre.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNombre.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNombre.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtNombre.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtNombre.Properties.MaxLength = 160
        Me.INDtxtNombre.Size = New System.Drawing.Size(376, 28)
        Me.INDtxtNombre.StyleController = Me.INDlycBase
        Me.INDtxtNombre.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNombre, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.MixingStation.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.Mask.EditMask = "[0-9]+"
        Me.INDbtnCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbtnCode.Size = New System.Drawing.Size(376, 28)
        Me.INDbtnCode.StyleController = Me.INDlycBase
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDgcUnitDoseType
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcUnitDoseType, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcUnitDoseType, Nothing)
        Me.INDgcUnitDoseType.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcUnitDoseType, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcUnitDoseType, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcUnitDoseType, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcUnitDoseType, False)
        Me.INDgcUnitDoseType.Location = New System.Drawing.Point(912, 85)
        Me.INDgcUnitDoseType.MainView = Me.INDviewUnitDosesType
        Me.INDgcUnitDoseType.Name = "INDgcUnitDoseType"
        Me.INDgcUnitDoseType.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1})
        Me.INDgcUnitDoseType.Size = New System.Drawing.Size(331, 483)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcUnitDoseType, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcUnitDoseType.TabIndex = 5
        Me.INDgcUnitDoseType.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewUnitDosesType})
        '
        'INDviewUnitDosesType
        '
        Me.INDviewUnitDosesType.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewUnitDosesType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewUnitDosesType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewUnitDosesType.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewUnitDosesType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewUnitDosesType.Appearance.Row.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewUnitDosesType.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewUnitDosesType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn2})
        Me.INDviewUnitDosesType.GridControl = Me.INDgcUnitDoseType
        Me.INDviewUnitDosesType.Name = "INDviewUnitDosesType"
        Me.INDviewUnitDosesType.OptionsCustomization.AllowGroup = False
        Me.INDviewUnitDosesType.OptionsCustomization.AllowSort = False
        Me.INDviewUnitDosesType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewUnitDosesType.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewUnitDosesType.OptionsView.ShowAutoFilterRow = True
        Me.INDviewUnitDosesType.OptionsView.ShowGroupPanel = False
        Me.INDviewUnitDosesType.Tag = 137
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDviewUnitDosesType, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "UnitDoseTypeCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 116
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "UnitDoseTypeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 317
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline)
        Me.RepositoryItemButtonEdit1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.RepositoryItemButtonEdit1.Appearance.Options.UseFont = True
        Me.RepositoryItemButtonEdit1.Appearance.Options.UseForeColor = True
        Me.RepositoryItemButtonEdit1.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemButtonEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        Me.RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAdd.Appearance.Options.UseFont = True
        Me.INDbtnAdd.Location = New System.Drawing.Point(1172, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAdd, True)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(71, 28)
        Me.INDbtnAdd.StyleController = Me.INDlycBase
        Me.INDbtnAdd.TabIndex = 4
        Me.INDbtnAdd.Text = "Agregar"
        '
        'INDsleddlType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleddlType, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleddlType, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleddlType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleddlType, False)
        Me.INDsleddlType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleddlType, False)
        Me.INDsleddlType.Location = New System.Drawing.Point(24, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleddlType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleddlType.MaximumSize = New System.Drawing.Size(386, 0)
        Me.INDsleddlType.Name = "INDsleddlType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleddlType, False)
        Me.INDsleddlType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleddlType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleddlType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleddlType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleddlType.Properties.Appearance.Options.UseFont = True
        Me.INDsleddlType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleddlType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleddlType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleddlType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleddlType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleddlType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleddlType.Properties.DisplayMember = "Item2"
        Me.INDsleddlType.Properties.NullText = ""
        Me.INDsleddlType.Properties.PopupSizeable = False
        Me.INDsleddlType.Properties.PopupView = Me.GridView32
        Me.INDsleddlType.Properties.ShowFooter = False
        Me.INDsleddlType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleddlType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleddlType, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleddlType, True)
        Me.INDsleddlType.Size = New System.Drawing.Size(376, 28)
        Me.INDsleddlType.StyleController = Me.INDlycBase
        Me.INDsleddlType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleddlType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleddlType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleddlType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleddlType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleddlType, False)
        '
        'GridView32
        '
        Me.GridView32.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView32.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView32.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView32.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView32.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView32.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView32.Appearance.GroupRow.Options.UseFont = True
        Me.GridView32.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView32.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView32.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView32.Appearance.Row.Options.UseFont = True
        Me.GridView32.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn42})
        Me.GridView32.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView32.Name = "GridView32"
        Me.GridView32.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView32.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView32.OptionsView.EnableAppearanceOddRow = True
        Me.GridView32.OptionsView.ShowAutoFilterRow = True
        Me.GridView32.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView32, False)
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "Tipo"
        Me.GridColumn42.FieldName = "Item2"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.OptionsColumn.AllowEdit = False
        Me.GridColumn42.OptionsColumn.AllowFocus = False
        Me.GridColumn42.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn42.Visible = True
        Me.GridColumn42.VisibleIndex = 0
        '
        'INDrgProductionChemical
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgProductionChemical, False)
        Me.INDrgProductionChemical.EnterMoveNextControl = True
        Me.INDrgProductionChemical.Location = New System.Drawing.Point(633, 53)
        Me.INDrgProductionChemical.Name = "INDrgProductionChemical"
        Me.INDrgProductionChemical.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgProductionChemical.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgProductionChemical.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgProductionChemical.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgProductionChemical.Properties.Appearance.Options.UseFont = True
        Me.INDrgProductionChemical.Properties.Appearance.Options.UseForeColor = True
        Me.INDrgProductionChemical.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgProductionChemical.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgProductionChemical.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgProductionChemical.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgProductionChemical.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgProductionChemical.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDrgProductionChemical.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDrgProductionChemical.Size = New System.Drawing.Size(251, 32)
        Me.INDrgProductionChemical.StyleController = Me.INDlycBase
        Me.INDrgProductionChemical.TabIndex = 7
        Me.INDrgProductionChemical.ToolTip = "Este Campo es Necesario"
        '
        'INDrgQualityChemical
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgQualityChemical, False)
        Me.INDrgQualityChemical.EnterMoveNextControl = True
        Me.INDrgQualityChemical.Location = New System.Drawing.Point(633, 89)
        Me.INDrgQualityChemical.Name = "INDrgQualityChemical"
        Me.INDrgQualityChemical.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgQualityChemical.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgQualityChemical.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgQualityChemical.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgQualityChemical.Properties.Appearance.Options.UseFont = True
        Me.INDrgQualityChemical.Properties.Appearance.Options.UseForeColor = True
        Me.INDrgQualityChemical.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgQualityChemical.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgQualityChemical.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgQualityChemical.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgQualityChemical.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgQualityChemical.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDrgQualityChemical.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si", False), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No", False)})
        Me.INDrgQualityChemical.Properties.ReadOnly = True
        Me.INDrgQualityChemical.Size = New System.Drawing.Size(251, 32)
        Me.INDrgQualityChemical.StyleController = Me.INDlycBase
        Me.INDrgQualityChemical.TabIndex = 7
        Me.INDrgQualityChemical.ToolTip = "Este Campo es Necesario"
        '
        'INDlycBaseUnitDoseType
        '
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycBaseUnitDoseType, False)
        Me.INDlycBaseUnitDoseType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycBaseUnitDoseType.GroupBordersVisible = False
        Me.INDlycBaseUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrUnitDoseType, Me.INDlyGrUnitDoseType1, Me.LayoutControlGroup2})
        Me.INDlycBaseUnitDoseType.Name = "Root"
        Me.INDlycBaseUnitDoseType.Size = New System.Drawing.Size(1267, 592)
        Me.INDlycBaseUnitDoseType.TextVisible = False
        '
        'INDlyGrUnitDoseType
        '
        Me.INDlyGrUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrUnitDoseType, False)
        Me.INDlyGrUnitDoseType.CustomizationFormText = "Datos Principales"
        Me.INDlyGrUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemCategory, Me.INDlyItemName, Me.INDlyItemType, Me.INDlyItemWeight})
        Me.INDlyGrUnitDoseType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrUnitDoseType.Name = "INDlyGrUnitDoseType"
        Me.INDlyGrUnitDoseType.Size = New System.Drawing.Size(404, 572)
        Me.INDlyGrUnitDoseType.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(380, 64)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemCategory
        '
        Me.INDlyItemCategory.AllowHide = False
        Me.INDlyItemCategory.Control = Me.INDsleddlCategory
        Me.INDlyItemCategory.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemCategory.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemCategory.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemCategory.Name = "INDlyItemCategory"
        Me.INDlyItemCategory.ShowInCustomizationForm = False
        Me.INDlyItemCategory.Size = New System.Drawing.Size(380, 64)
        Me.INDlyItemCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCategory.Text = "Categoría"
        Me.INDlyItemCategory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCategory.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCategory.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemCategory.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtNombre
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(380, 64)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemType
        '
        Me.INDlyItemType.Control = Me.INDsleddlType
        Me.INDlyItemType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemType.CustomizationFormText = "Tipo"
        Me.INDlyItemType.Location = New System.Drawing.Point(0, 256)
        Me.INDlyItemType.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemType.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemType.Name = "INDlyItemType"
        Me.INDlyItemType.Size = New System.Drawing.Size(380, 263)
        Me.INDlyItemType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemType.Text = "Tipo"
        Me.INDlyItemType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemType.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemType.TextToControlDistance = 5
        '
        'INDlyItemWeight
        '
        Me.INDlyItemWeight.Control = Me.INDSpinWeight
        Me.INDlyItemWeight.CustomizationFormText = "Orden"
        Me.INDlyItemWeight.Location = New System.Drawing.Point(0, 192)
        Me.INDlyItemWeight.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemWeight.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemWeight.Name = "INDlyItemWeight"
        Me.INDlyItemWeight.Size = New System.Drawing.Size(380, 64)
        Me.INDlyItemWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWeight.Text = "Orden"
        Me.INDlyItemWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemWeight.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemWeight.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemWeight.TextToControlDistance = 5
        '
        'INDlyGrUnitDoseType1
        '
        Me.INDlyGrUnitDoseType1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType1.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrUnitDoseType1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType1.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrUnitDoseType1, False)
        Me.INDlyGrUnitDoseType1.CustomizationFormText = "Perfil Responsable"
        Me.INDlyGrUnitDoseType1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyProductionChemical, Me.INDlyQualityChemical})
        Me.INDlyGrUnitDoseType1.Location = New System.Drawing.Point(404, 0)
        Me.INDlyGrUnitDoseType1.Name = "INDlyGrUnitDoseType1"
        Me.INDlyGrUnitDoseType1.Size = New System.Drawing.Size(484, 572)
        Me.INDlyGrUnitDoseType1.Text = "Perfil Responsable"
        '
        'INDlyProductionChemical
        '
        Me.INDlyProductionChemical.Control = Me.INDrgProductionChemical
        Me.INDlyProductionChemical.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyProductionChemical.CustomizationFormText = "INDrgProductionChemical"
        Me.INDlyProductionChemical.Location = New System.Drawing.Point(0, 0)
        Me.INDlyProductionChemical.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDlyProductionChemical.MinSize = New System.Drawing.Size(460, 36)
        Me.INDlyProductionChemical.Name = "INDlyProductionChemical"
        Me.INDlyProductionChemical.Size = New System.Drawing.Size(460, 36)
        Me.INDlyProductionChemical.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyProductionChemical.Text = "Químico de producción"
        Me.INDlyProductionChemical.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyProductionChemical.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDlyProductionChemical.TextSize = New System.Drawing.Size(200, 21)
        Me.INDlyProductionChemical.TextToControlDistance = 5
        '
        'INDlyQualityChemical
        '
        Me.INDlyQualityChemical.Control = Me.INDrgQualityChemical
        Me.INDlyQualityChemical.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyQualityChemical.CustomizationFormText = "INDrgQualityChemical"
        Me.INDlyQualityChemical.Location = New System.Drawing.Point(0, 36)
        Me.INDlyQualityChemical.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDlyQualityChemical.MinSize = New System.Drawing.Size(460, 36)
        Me.INDlyQualityChemical.Name = "INDlyQualityChemical"
        Me.INDlyQualityChemical.Size = New System.Drawing.Size(460, 483)
        Me.INDlyQualityChemical.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyQualityChemical.Text = "Químico de calidad"
        Me.INDlyQualityChemical.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyQualityChemical.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDlyQualityChemical.TextSize = New System.Drawing.Size(200, 21)
        Me.INDlyQualityChemical.TextToControlDistance = 5
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
        Me.LayoutControlGroup2.CustomizationFormText = "Tipos de Dosis unitarias"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciUnitDoseType, Me.LayoutControlItem5, Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(888, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(359, 572)
        Me.LayoutControlGroup2.Text = "Tipos de Dosis unitarias"
        '
        'INDLciUnitDoseType
        '
        Me.INDLciUnitDoseType.Control = Me.INDgcUnitDoseType
        Me.INDLciUnitDoseType.CustomizationFormText = "Dosis unitarias"
        Me.INDLciUnitDoseType.Location = New System.Drawing.Point(0, 32)
        Me.INDLciUnitDoseType.Name = "INDLciUnitDoseType"
        Me.INDLciUnitDoseType.Size = New System.Drawing.Size(335, 487)
        Me.INDLciUnitDoseType.Text = "Dosis unitarias"
        Me.INDLciUnitDoseType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciUnitDoseType.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDbtnAdd
        Me.LayoutControlItem5.CustomizationFormText = "Agregar Dosis unitarias"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(260, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Agregar Dosis unitarias"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsleddlDosis
        Me.LayoutControlItem2.CustomizationFormText = "Tipos Dosis Unitarias"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(260, 32)
        Me.LayoutControlItem2.Text = "Dosis Unitarias"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem2.TextToControlDistance = 0
        '
        'IndigoGridView3
        '
        Me.IndigoGridView3.RaiseMenuPopUp = True
        Me.IndigoGridView3.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'RepositoryItemPopupContainerEdit31
        '
        Me.RepositoryItemPopupContainerEdit31.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit31.Name = "RepositoryItemPopupContainerEdit31"
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'RepositoryItemPopupContainerEdit22
        '
        Me.RepositoryItemPopupContainerEdit22.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit22.Name = "RepositoryItemPopupContainerEdit22"
        '
        'RepositoryItemPopupContainerEdit32
        '
        Me.RepositoryItemPopupContainerEdit32.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit32.Name = "RepositoryItemPopupContainerEdit32"
        '
        'FrmDefects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1471, 736)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmDefects"
        Me.Opacity = 1.0R
        Me.Tag = "2073"
        Me.Text = "Configuración Líneas de producción"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcncUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycBase.ResumeLayout(False)
        CType(Me.INDSpinWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleddlDosis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleddlCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewUnitDosesType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleddlType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrgProductionChemical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrgQualityChemical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrUnitDoseType1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyProductionChemical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyQualityChemical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit32, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDcncUnitDoseType As CtrNavigationControlPanel
    Friend WithEvents INDlycBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycBaseUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents IndigoRadioGroup1 As IndigoRadioGroup
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDgcUnitDoseType As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDviewUnitDosesType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciUnitDoseType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleddlCategory As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemCategory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDsleddlDosis As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView3 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDlyGrUnitDoseType1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit22 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit32 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleddlType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView32 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Public WithEvents INDlyItemType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrgProductionChemical As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDrgQualityChemical As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDlyProductionChemical As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyQualityChemical As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSpinWeight As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemWeight As DevExpress.XtraLayout.LayoutControlItem
End Class
