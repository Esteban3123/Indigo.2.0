#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLineClearanceCriteria
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
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDcncUnitDoseType = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlycBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleUnitDoseType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDgcUnitDoseType = New DevExpress.XtraGrid.GridControl()
        Me.INDviewUnitDosesType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSeValidateCriteria = New DevExpress.XtraEditors.RadioGroup()
        Me.INDSeValidateOption = New DevExpress.XtraEditors.RadioGroup()
        Me.INDSeChemicalProduction = New DevExpress.XtraEditors.RadioGroup()
        Me.INDSeAuxProduction = New DevExpress.XtraEditors.RadioGroup()
        Me.INDlycBaseUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemValidateCriteria = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemValidateOption = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciUnitDoseType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrUnitDoseType1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemChemicalProduction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAuxProduction = New DevExpress.XtraLayout.LayoutControlItem()
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
        Me.RepositoryItemPopupContainerEdit22 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
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
        CType(Me.INDsleUnitDoseType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewUnitDosesType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeValidateCriteria.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeValidateOption.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeChemicalProduction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeAuxProduction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemValidateCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemValidateOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrUnitDoseType1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemChemicalProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuxProduction, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDcncUnitDoseType)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1296, 588)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(4)
        Me.ToolBars.Size = New System.Drawing.Size(1296, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(6)
        Me.BarraBotones.Size = New System.Drawing.Size(1296, 130)
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
        Me.INDcncUnitDoseType.Location = New System.Drawing.Point(2, 9)
        Me.INDcncUnitDoseType.Margin = New System.Windows.Forms.Padding(0)
        Me.INDcncUnitDoseType.Name = "INDcncUnitDoseType"
        Me.INDcncUnitDoseType.Size = New System.Drawing.Size(200, 577)
        Me.INDcncUnitDoseType.TabIndex = 2
        Me.INDcncUnitDoseType.UseDisabledStatePainter = False
        '
        'INDlycBase
        '
        Me.INDlycBase.Controls.Add(Me.INDsleUnitDoseType)
        Me.INDlycBase.Controls.Add(Me.INDtxtNombre)
        Me.INDlycBase.Controls.Add(Me.INDbtnCode)
        Me.INDlycBase.Controls.Add(Me.INDgcUnitDoseType)
        Me.INDlycBase.Controls.Add(Me.INDbtnAdd)
        Me.INDlycBase.Controls.Add(Me.INDSeValidateCriteria)
        Me.INDlycBase.Controls.Add(Me.INDSeValidateOption)
        Me.INDlycBase.Controls.Add(Me.INDSeChemicalProduction)
        Me.INDlycBase.Controls.Add(Me.INDSeAuxProduction)
        Me.INDlycBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycBase.Location = New System.Drawing.Point(202, 9)
        Me.INDlycBase.Name = "INDlycBase"
        Me.INDlycBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(434, 310, 1201, 569)
        Me.INDlycBase.Root = Me.INDlycBaseUnitDoseType
        Me.INDlycBase.Size = New System.Drawing.Size(1092, 577)
        Me.INDlycBase.TabIndex = 3
        Me.INDlycBase.Text = "LayoutControl1"
        '
        'INDsleUnitDoseType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleUnitDoseType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleUnitDoseType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleUnitDoseType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.INDsleUnitDoseType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.INDsleUnitDoseType.Location = New System.Drawing.Point(943, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleUnitDoseType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleUnitDoseType.Name = "INDsleUnitDoseType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleUnitDoseType, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.INDsleUnitDoseType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleUnitDoseType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleUnitDoseType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleUnitDoseType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleUnitDoseType.Properties.Appearance.Options.UseFont = True
        Me.INDsleUnitDoseType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleUnitDoseType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleUnitDoseType.Properties.DisplayMember = "CodeDescription"
        Me.INDsleUnitDoseType.Properties.NullText = ""
        Me.INDsleUnitDoseType.Properties.PopupSizeable = False
        Me.INDsleUnitDoseType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleUnitDoseType.Properties.ShowFooter = False
        Me.INDsleUnitDoseType.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleUnitDoseType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleUnitDoseType, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleUnitDoseType, True)
        Me.INDsleUnitDoseType.Size = New System.Drawing.Size(50, 28)
        Me.INDsleUnitDoseType.StyleController = Me.INDlycBase
        Me.INDsleUnitDoseType.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleUnitDoseType, "2067")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleUnitDoseType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleUnitDoseType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleUnitDoseType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleUnitDoseType, False)
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
        'INDtxtNombre
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNombre, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNombre, False)
        Me.INDtxtNombre.EnterMoveNextControl = True
        Me.INDtxtNombre.Location = New System.Drawing.Point(-30, 139)
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
        Me.INDtxtNombre.Properties.MaxLength = 300
        Me.INDtxtNombre.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtNombre.StyleController = Me.INDlycBase
        Me.INDtxtNombre.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNombre, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(-30, 79)
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
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
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
        Me.INDgcUnitDoseType.Location = New System.Drawing.Point(808, 85)
        Me.INDgcUnitDoseType.MainView = Me.INDviewUnitDosesType
        Me.INDgcUnitDoseType.Name = "INDgcUnitDoseType"
        Me.INDgcUnitDoseType.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1})
        Me.INDgcUnitDoseType.Size = New System.Drawing.Size(260, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcUnitDoseType, DevExpress.XtraLayout.SizeConstraintsType.Custom)
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
        Me.INDbtnAdd.Location = New System.Drawing.Point(997, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAdd, True)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(71, 28)
        Me.INDbtnAdd.StyleController = Me.INDlycBase
        Me.INDbtnAdd.TabIndex = 4
        Me.INDbtnAdd.Text = "Agregar"
        '
        'INDSeValidateCriteria
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDSeValidateCriteria, False)
        Me.INDSeValidateCriteria.Location = New System.Drawing.Point(175, 173)
        Me.INDSeValidateCriteria.MaximumSize = New System.Drawing.Size(386, 0)
        Me.INDSeValidateCriteria.Name = "INDSeValidateCriteria"
        Me.INDSeValidateCriteria.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeValidateCriteria.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValidateCriteria.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeValidateCriteria.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeValidateCriteria.Properties.Appearance.Options.UseFont = True
        Me.INDSeValidateCriteria.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeValidateCriteria.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValidateCriteria.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeValidateCriteria.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeValidateCriteria.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeValidateCriteria.Properties.Columns = 2
        Me.INDSeValidateCriteria.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDSeValidateCriteria.Size = New System.Drawing.Size(181, 36)
        Me.INDSeValidateCriteria.StyleController = Me.INDlycBase
        Me.INDSeValidateCriteria.TabIndex = 2
        '
        'INDSeValidateOption
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDSeValidateOption, False)
        Me.INDSeValidateOption.Location = New System.Drawing.Point(175, 213)
        Me.INDSeValidateOption.MaximumSize = New System.Drawing.Size(386, 0)
        Me.INDSeValidateOption.Name = "INDSeValidateOption"
        Me.INDSeValidateOption.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeValidateOption.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValidateOption.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeValidateOption.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeValidateOption.Properties.Appearance.Options.UseFont = True
        Me.INDSeValidateOption.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeValidateOption.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValidateOption.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeValidateOption.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeValidateOption.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeValidateOption.Properties.Columns = 2
        Me.INDSeValidateOption.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDSeValidateOption.Size = New System.Drawing.Size(181, 36)
        Me.INDSeValidateOption.StyleController = Me.INDlycBase
        Me.INDSeValidateOption.TabIndex = 2
        '
        'INDSeChemicalProduction
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDSeChemicalProduction, False)
        Me.INDSeChemicalProduction.Location = New System.Drawing.Point(594, 58)
        Me.INDSeChemicalProduction.Name = "INDSeChemicalProduction"
        Me.INDSeChemicalProduction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeChemicalProduction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeChemicalProduction.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeChemicalProduction.Properties.Appearance.Options.UseFont = True
        Me.INDSeChemicalProduction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeChemicalProduction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeChemicalProduction.Properties.Columns = 2
        Me.INDSeChemicalProduction.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDSeChemicalProduction.Size = New System.Drawing.Size(181, 36)
        Me.INDSeChemicalProduction.StyleController = Me.INDlycBase
        Me.INDSeChemicalProduction.TabIndex = 0
        Me.INDSeChemicalProduction.ToolTip = "Este Campo es Necesario"
        '
        'INDSeAuxProduction
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDSeAuxProduction, False)
        Me.INDSeAuxProduction.Location = New System.Drawing.Point(594, 98)
        Me.INDSeAuxProduction.Name = "INDSeAuxProduction"
        Me.INDSeAuxProduction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeAuxProduction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeAuxProduction.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeAuxProduction.Properties.Appearance.Options.UseFont = True
        Me.INDSeAuxProduction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeAuxProduction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeAuxProduction.Properties.Columns = 2
        Me.INDSeAuxProduction.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDSeAuxProduction.Size = New System.Drawing.Size(181, 36)
        Me.INDSeAuxProduction.StyleController = Me.INDlycBase
        Me.INDSeAuxProduction.TabIndex = 1
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
        Me.INDlycBaseUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrUnitDoseType, Me.LayoutControlGroup2, Me.INDlyGrUnitDoseType1})
        Me.INDlycBaseUnitDoseType.Name = "Root"
        Me.INDlycBaseUnitDoseType.Size = New System.Drawing.Size(1146, 560)
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
        Me.INDlyGrUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemValidateCriteria, Me.INDlyItemDescription, Me.INDlyItemValidateOption})
        Me.INDlyGrUnitDoseType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrUnitDoseType.Name = "INDlyGrUnitDoseType"
        Me.INDlyGrUnitDoseType.Size = New System.Drawing.Size(414, 540)
        Me.INDlyGrUnitDoseType.Text = "Datos Principales"
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
        'INDlyItemValidateCriteria
        '
        Me.INDlyItemValidateCriteria.Control = Me.INDSeValidateCriteria
        Me.INDlyItemValidateCriteria.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemValidateCriteria.MaxSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemValidateCriteria.MinSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemValidateCriteria.Name = "INDlyItemValidateCriteria"
        Me.INDlyItemValidateCriteria.Size = New System.Drawing.Size(390, 40)
        Me.INDlyItemValidateCriteria.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemValidateCriteria.Text = "Se valida el criterio?"
        Me.INDlyItemValidateCriteria.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemValidateCriteria.TextSize = New System.Drawing.Size(200, 21)
        Me.INDlyItemValidateCriteria.TextToControlDistance = 5
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDtxtNombre
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Nombre del criterio"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'INDlyItemValidateOption
        '
        Me.INDlyItemValidateOption.Control = Me.INDSeValidateOption
        Me.INDlyItemValidateOption.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemValidateOption.CustomizationFormText = "Unidad Funcional"
        Me.INDlyItemValidateOption.Location = New System.Drawing.Point(0, 160)
        Me.INDlyItemValidateOption.MaxSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemValidateOption.MinSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemValidateOption.Name = "INDlyItemValidateOption"
        Me.INDlyItemValidateOption.Size = New System.Drawing.Size(390, 327)
        Me.INDlyItemValidateOption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemValidateOption.Text = "Seleccione la opcion a validar"
        Me.INDlyItemValidateOption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemValidateOption.TextSize = New System.Drawing.Size(200, 21)
        Me.INDlyItemValidateOption.TextToControlDistance = 5
        Me.INDlyItemValidateOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(838, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(288, 540)
        Me.LayoutControlGroup2.Text = "Tipos de Dosis unitarias"
        '
        'INDLciUnitDoseType
        '
        Me.INDLciUnitDoseType.Control = Me.INDgcUnitDoseType
        Me.INDLciUnitDoseType.CustomizationFormText = "Dosis unitarias"
        Me.INDLciUnitDoseType.Location = New System.Drawing.Point(0, 32)
        Me.INDLciUnitDoseType.Name = "INDLciUnitDoseType"
        Me.INDLciUnitDoseType.Size = New System.Drawing.Size(264, 455)
        Me.INDLciUnitDoseType.Text = "Dosis unitarias"
        Me.INDLciUnitDoseType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciUnitDoseType.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDbtnAdd
        Me.LayoutControlItem5.CustomizationFormText = "Agregar Dosis unitarias"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(189, 0)
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
        Me.LayoutControlItem2.Control = Me.INDsleUnitDoseType
        Me.LayoutControlItem2.CustomizationFormText = "Tipos Dosis Unitarias"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(189, 32)
        Me.LayoutControlItem2.Text = "Dosis Unitarias"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem2.TextToControlDistance = 0
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
        Me.INDlyGrUnitDoseType1.CustomizationFormText = "Datos Principales"
        Me.INDlyGrUnitDoseType1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemChemicalProduction, Me.INDlyItemAuxProduction})
        Me.INDlyGrUnitDoseType1.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGrUnitDoseType1.Name = "INDlyGrUnitDoseType1"
        Me.INDlyGrUnitDoseType1.Padding = New DevExpress.XtraLayout.Utils.Padding(13, 13, 13, 13)
        Me.INDlyGrUnitDoseType1.Size = New System.Drawing.Size(424, 540)
        Me.INDlyGrUnitDoseType1.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3)
        Me.INDlyGrUnitDoseType1.Text = "Perfil Responsable"
        '
        'INDlyItemChemicalProduction
        '
        Me.INDlyItemChemicalProduction.Control = Me.INDSeChemicalProduction
        Me.INDlyItemChemicalProduction.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemChemicalProduction.CustomizationFormText = "Código"
        Me.INDlyItemChemicalProduction.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemChemicalProduction.MaxSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemChemicalProduction.MinSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemChemicalProduction.Name = "INDlyItemChemicalProduction"
        Me.INDlyItemChemicalProduction.Size = New System.Drawing.Size(390, 40)
        Me.INDlyItemChemicalProduction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemChemicalProduction.Text = "Químico de producción"
        Me.INDlyItemChemicalProduction.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemChemicalProduction.TextSize = New System.Drawing.Size(200, 21)
        Me.INDlyItemChemicalProduction.TextToControlDistance = 5
        '
        'INDlyItemAuxProduction
        '
        Me.INDlyItemAuxProduction.Control = Me.INDSeAuxProduction
        Me.INDlyItemAuxProduction.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemAuxProduction.CustomizationFormText = "Nombre de Línea de Producción"
        Me.INDlyItemAuxProduction.Location = New System.Drawing.Point(0, 40)
        Me.INDlyItemAuxProduction.MaxSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemAuxProduction.MinSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemAuxProduction.Name = "INDlyItemAuxProduction"
        Me.INDlyItemAuxProduction.Size = New System.Drawing.Size(390, 437)
        Me.INDlyItemAuxProduction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuxProduction.Text = "Auxiliar de producción"
        Me.INDlyItemAuxProduction.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuxProduction.TextSize = New System.Drawing.Size(200, 21)
        Me.INDlyItemAuxProduction.TextToControlDistance = 5
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
        'RepositoryItemPopupContainerEdit22
        '
        Me.RepositoryItemPopupContainerEdit22.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit22.Name = "RepositoryItemPopupContainerEdit22"
        '
        'FrmLineClearanceCriteria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1296, 725)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmLineClearanceCriteria"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "89030"
        Me.Text = "Criterios de desepeje de linea"
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
        CType(Me.INDsleUnitDoseType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewUnitDosesType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeValidateCriteria.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeValidateOption.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeChemicalProduction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeAuxProduction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemValidateCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemValidateOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrUnitDoseType1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemChemicalProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuxProduction, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDcncUnitDoseType As CtrNavigationControlPanel
    Friend WithEvents INDlycBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycBaseUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents INDlyItemValidateCriteria As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDsleUnitDoseType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView3 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDlyGrUnitDoseType1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemChemicalProduction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAuxProduction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit22 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDlyItemValidateOption As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeValidateCriteria As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDSeValidateOption As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDSeChemicalProduction As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDSeAuxProduction As DevExpress.XtraEditors.RadioGroup
End Class
