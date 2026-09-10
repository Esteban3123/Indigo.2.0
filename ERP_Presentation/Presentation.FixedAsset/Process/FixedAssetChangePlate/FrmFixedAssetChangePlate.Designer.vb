Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetChangePlate
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetChangePlate))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyChangePlate = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpopupDetails = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyPopup = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtNewPlate = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtOldPlate = New DevExpress.XtraEditors.TextEdit()
        Me.INDslePhysicalAsset = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchPhysical = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPhysicalAsset = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemOldPlate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNewPlate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddAssets = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpceDetails = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPopupDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
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
        CType(Me.INDlyChangePlate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyChangePlate.SuspendLayout()
        CType(Me.INDpopupDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupDetails.SuspendLayout()
        CType(Me.INDlyPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPopup.SuspendLayout()
        CType(Me.INDtxtNewPlate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtOldPlate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePhysicalAsset.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchPhysical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPhysicalAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOldPlate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNewPlate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        CType(Me.INDpceDetails.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPopupDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDPanelControlBase.Controls.Add(Me.INDlyChangePlate)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1262, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1262, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1262, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyChangePlate
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyChangePlate
        '
        Me.INDlyChangePlate.Controls.Add(Me.INDpopupDetails)
        Me.INDlyChangePlate.Controls.Add(Me.INDpceDetails)
        Me.INDlyChangePlate.Controls.Add(Me.INDgcDetails)
        Me.INDlyChangePlate.Controls.Add(Me.INDmemoObservations)
        Me.INDlyChangePlate.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyChangePlate.Controls.Add(Me.INDbtnCode)
        Me.INDlyChangePlate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyChangePlate.Location = New System.Drawing.Point(202, 7)
        Me.INDlyChangePlate.Name = "INDlyChangePlate"
        Me.INDlyChangePlate.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(126, 353, 402, 428)
        Me.INDlyChangePlate.Root = Me.LayoutControlGroup1
        Me.INDlyChangePlate.Size = New System.Drawing.Size(1058, 557)
        Me.INDlyChangePlate.TabIndex = 2
        Me.INDlyChangePlate.Text = "LayoutControl1"
        '
        'INDpopupDetails
        '
        Me.INDpopupDetails.Controls.Add(Me.INDlyPopup)
        Me.INDpopupDetails.Controls.Add(Me.INDpanelButtons)
        Me.INDpopupDetails.Location = New System.Drawing.Point(466, 197)
        Me.INDpopupDetails.Name = "INDpopupDetails"
        Me.INDpopupDetails.Size = New System.Drawing.Size(412, 257)
        Me.INDpopupDetails.TabIndex = 10
        '
        'INDlyPopup
        '
        Me.INDlyPopup.Controls.Add(Me.INDtxtNewPlate)
        Me.INDlyPopup.Controls.Add(Me.INDtxtOldPlate)
        Me.INDlyPopup.Controls.Add(Me.INDslePhysicalAsset)
        Me.INDlyPopup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPopup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyPopup.Name = "INDlyPopup"
        Me.INDlyPopup.Root = Me.LayoutControlGroup2
        Me.INDlyPopup.Size = New System.Drawing.Size(412, 217)
        Me.INDlyPopup.TabIndex = 1
        Me.INDlyPopup.Text = "LayoutControl1"
        '
        'INDtxtNewPlate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNewPlate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNewPlate, False)
        Me.INDtxtNewPlate.EnterMoveNextControl = True
        Me.INDtxtNewPlate.Location = New System.Drawing.Point(12, 158)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNewPlate, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtNewPlate.Name = "INDtxtNewPlate"
        Me.INDtxtNewPlate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtNewPlate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNewPlate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNewPlate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNewPlate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNewPlate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNewPlate.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtNewPlate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtNewPlate.Properties.MaxLength = 50
        Me.INDtxtNewPlate.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtNewPlate.StyleController = Me.INDlyPopup
        Me.INDtxtNewPlate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNewPlate, 0)
        '
        'INDtxtOldPlate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtOldPlate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtOldPlate, False)
        Me.INDtxtOldPlate.EnterMoveNextControl = True
        Me.INDtxtOldPlate.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtOldPlate, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtOldPlate.Name = "INDtxtOldPlate"
        Me.INDtxtOldPlate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtOldPlate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtOldPlate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtOldPlate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtOldPlate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtOldPlate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtOldPlate.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtOldPlate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtOldPlate.Properties.MaxLength = 50
        Me.INDtxtOldPlate.Properties.ReadOnly = True
        Me.INDtxtOldPlate.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtOldPlate.StyleController = Me.INDlyPopup
        Me.INDtxtOldPlate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtOldPlate, 0)
        '
        'INDslePhysicalAsset
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePhysicalAsset, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePhysicalAsset, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePhysicalAsset, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDslePhysicalAsset, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.INDslePhysicalAsset.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.INDslePhysicalAsset.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePhysicalAsset, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePhysicalAsset.Name = "INDslePhysicalAsset"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.INDslePhysicalAsset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDslePhysicalAsset.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDslePhysicalAsset.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDslePhysicalAsset.Properties.Appearance.Options.UseBackColor = True
        Me.INDslePhysicalAsset.Properties.Appearance.Options.UseFont = True
        Me.INDslePhysicalAsset.Properties.Appearance.Options.UseForeColor = True
        Me.INDslePhysicalAsset.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDslePhysicalAsset.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDslePhysicalAsset.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDslePhysicalAsset.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDslePhysicalAsset.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDslePhysicalAsset.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDslePhysicalAsset.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslePhysicalAsset.Properties.DisplayMember = "AssetDescription"
        Me.INDslePhysicalAsset.Properties.NullText = ""
        Me.INDslePhysicalAsset.Properties.PopupSizeable = False
        Me.INDslePhysicalAsset.Properties.PopupView = Me.INDviewSearchPhysical
        Me.INDslePhysicalAsset.Properties.ShowFooter = False
        Me.INDslePhysicalAsset.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePhysicalAsset, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePhysicalAsset, True)
        Me.INDslePhysicalAsset.Size = New System.Drawing.Size(386, 28)
        Me.INDslePhysicalAsset.StyleController = Me.INDlyPopup
        Me.INDslePhysicalAsset.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePhysicalAsset, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePhysicalAsset, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePhysicalAsset, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePhysicalAsset, False)
        '
        'INDviewSearchPhysical
        '
        Me.INDviewSearchPhysical.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchPhysical.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchPhysical.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchPhysical.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchPhysical.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewSearchPhysical.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchPhysical.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchPhysical.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchPhysical.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchPhysical.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchPhysical.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchPhysical.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewSearchPhysical.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchPhysical.Name = "INDviewSearchPhysical"
        Me.INDviewSearchPhysical.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchPhysical.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchPhysical.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchPhysical.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchPhysical.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchPhysical, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Articulo"
        Me.GridColumn4.FieldName = "ItemId.CodeDescription"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 672
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Serie"
        Me.GridColumn5.FieldName = "Serie"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 338
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Placa"
        Me.GridColumn6.FieldName = "Plate"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 382
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
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPhysicalAsset, Me.INDlyItemOldPlate, Me.INDlyItemNewPlate})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(412, 217)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemPhysicalAsset
        '
        Me.INDlyItemPhysicalAsset.Control = Me.INDslePhysicalAsset
        Me.INDlyItemPhysicalAsset.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPhysicalAsset.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPhysicalAsset.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPhysicalAsset.Name = "INDlyItemPhysicalAsset"
        Me.INDlyItemPhysicalAsset.Size = New System.Drawing.Size(392, 60)
        Me.INDlyItemPhysicalAsset.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPhysicalAsset.Text = "Activo"
        Me.INDlyItemPhysicalAsset.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPhysicalAsset.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPhysicalAsset.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPhysicalAsset.TextToControlDistance = 5
        '
        'INDlyItemOldPlate
        '
        Me.INDlyItemOldPlate.Control = Me.INDtxtOldPlate
        Me.INDlyItemOldPlate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemOldPlate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemOldPlate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemOldPlate.Name = "INDlyItemOldPlate"
        Me.INDlyItemOldPlate.Size = New System.Drawing.Size(392, 60)
        Me.INDlyItemOldPlate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemOldPlate.Text = "Placa Antigua"
        Me.INDlyItemOldPlate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemOldPlate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemOldPlate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemOldPlate.TextToControlDistance = 5
        '
        'INDlyItemNewPlate
        '
        Me.INDlyItemNewPlate.Control = Me.INDtxtNewPlate
        Me.INDlyItemNewPlate.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemNewPlate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNewPlate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNewPlate.Name = "INDlyItemNewPlate"
        Me.INDlyItemNewPlate.Size = New System.Drawing.Size(392, 77)
        Me.INDlyItemNewPlate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNewPlate.Text = "Placa Nueva"
        Me.INDlyItemNewPlate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNewPlate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemNewPlate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemNewPlate.TextToControlDistance = 5
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAddAssets)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(0, 217)
        Me.INDpanelButtons.MaximumSize = New System.Drawing.Size(0, 40)
        Me.INDpanelButtons.MinimumSize = New System.Drawing.Size(0, 40)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(412, 40)
        Me.INDpanelButtons.TabIndex = 0
        '
        'INDbtnAddAssets
        '
        Me.INDbtnAddAssets.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAssets.Appearance.Options.UseFont = True
        Me.INDbtnAddAssets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddAssets.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAssets, True)
        Me.INDbtnAddAssets.Name = "INDbtnAddAssets"
        Me.INDbtnAddAssets.Size = New System.Drawing.Size(408, 36)
        Me.INDbtnAddAssets.TabIndex = 0
        Me.INDbtnAddAssets.Text = "Agregar"
        '
        'INDpceDetails
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceDetails, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceDetails, Nothing)
        Me.INDpceDetails.Location = New System.Drawing.Point(438, 53)
        Me.INDpceDetails.MaximumSize = New System.Drawing.Size(596, 0)
        Me.INDpceDetails.MinimumSize = New System.Drawing.Size(596, 32)
        Me.INDpceDetails.Name = "INDpceDetails"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceDetails, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceDetails, False)
        Me.INDpceDetails.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceDetails.Properties.Appearance.Options.UseFont = True
        Me.INDpceDetails.Properties.AutoHeight = False
        Me.INDpceDetails.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDpceDetails.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceDetails.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceDetails.Properties.PopupControl = Me.INDpopupDetails
        Me.INDpceDetails.Properties.PopupSizeable = False
        Me.INDpceDetails.Properties.ShowPopupCloseButton = False
        Me.INDpceDetails.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceDetails.Size = New System.Drawing.Size(596, 32)
        Me.INDpceDetails.StyleController = Me.INDlyChangePlate
        Me.INDpceDetails.TabIndex = 3
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceDetails, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceDetails, Nothing)
        '
        'INDgcDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetails, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetails, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetails, False)
        Me.INDgcDetails.Location = New System.Drawing.Point(438, 89)
        Me.INDgcDetails.MainView = Me.INDviewDetails
        Me.INDgcDetails.Name = "INDgcDetails"
        Me.INDgcDetails.Size = New System.Drawing.Size(596, 444)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetails, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetails.TabIndex = 4
        Me.INDgcDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDetails})
        '
        'INDviewDetails
        '
        Me.INDviewDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDetails.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDetails.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDetails.Appearance.Row.Options.UseFont = True
        Me.INDviewDetails.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDetails.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.INDviewDetails.GridControl = Me.INDgcDetails
        Me.INDviewDetails.Name = "INDviewDetails"
        Me.INDviewDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDetails.OptionsView.ShowDetailButtons = False
        Me.INDviewDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDetails, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Activo"
        Me.GridColumn1.FieldName = "PhysicalAssetDescription"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Placa Antigua"
        Me.GridColumn2.FieldName = "OldPlate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Placa Nueva"
        Me.GridColumn3.FieldName = "NewPlate"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'INDmemoObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoObservations, False)
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
        Me.INDmemoObservations.Properties.MaxLength = 300
        Me.INDmemoObservations.Size = New System.Drawing.Size(386, 70)
        Me.INDmemoObservations.StyleController = Me.INDlyChangePlate
        Me.INDmemoObservations.TabIndex = 2
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
        Me.INDdteDocumentDate.StyleController = Me.INDlyChangePlate
        Me.INDdteDocumentDate.TabIndex = 1
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
        EditorButtonImageOptions2.Image = CType(resources.GetObject("EditorButtonImageOptions2.Image"), System.Drawing.Image)
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyChangePlate
        Me.INDbtnCode.TabIndex = 0
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygDetails})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1058, 557)
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
        'INDlygDetails
        '
        Me.INDlygDetails.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceGroup.Options.UseFont = True
        Me.INDlygDetails.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDetails, False)
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDetails, Me.INDlyItemPopupDetails})
        Me.INDlygDetails.Location = New System.Drawing.Point(414, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(624, 537)
        Me.INDlygDetails.Text = "Detalles"
        '
        'INDlyItemDetails
        '
        Me.INDlyItemDetails.Control = Me.INDgcDetails
        Me.INDlyItemDetails.CustomizationFormText = "Listado Detalles"
        Me.INDlyItemDetails.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemDetails.MaxSize = New System.Drawing.Size(600, 0)
        Me.INDlyItemDetails.MinSize = New System.Drawing.Size(600, 24)
        Me.INDlyItemDetails.Name = "INDlyItemDetails"
        Me.INDlyItemDetails.Size = New System.Drawing.Size(600, 448)
        Me.INDlyItemDetails.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDetails.TextVisible = False
        '
        'INDlyItemPopupDetails
        '
        Me.INDlyItemPopupDetails.Control = Me.INDpceDetails
        Me.INDlyItemPopupDetails.CustomizationFormText = "Agregar Detalles"
        Me.INDlyItemPopupDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPopupDetails.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemPopupDetails.MinSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemPopupDetails.Name = "INDlyItemPopupDetails"
        Me.INDlyItemPopupDetails.Size = New System.Drawing.Size(600, 36)
        Me.INDlyItemPopupDetails.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPopupDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemPopupDetails.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'FrmFixedAssetChangePlate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetChangePlate"
        Me.Opacity = 1.0R
        Me.Tag = "1788"
        Me.Text = "Cambio de Placa"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyChangePlate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyChangePlate.ResumeLayout(False)
        CType(Me.INDpopupDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupDetails.ResumeLayout(False)
        CType(Me.INDlyPopup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPopup.ResumeLayout(False)
        CType(Me.INDtxtNewPlate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtOldPlate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePhysicalAsset.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchPhysical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPhysicalAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOldPlate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNewPlate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        CType(Me.INDpceDetails.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPopupDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents INDlyChangePlate As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceDetails As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyItemPopupDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDpopupDetails As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddAssets As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyPopup As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDslePhysicalAsset As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchPhysical As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemPhysicalAsset As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtOldPlate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemOldPlate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtNewPlate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemNewPlate As DevExpress.XtraLayout.LayoutControlItem
End Class
