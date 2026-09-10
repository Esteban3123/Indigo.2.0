Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetTransfer
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
        Dim AppearanceObject11 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject12 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetTransfer))
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyTransfer = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAddAssets = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcAssets = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAssets = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleTargetResponsible = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleSourceResponsible = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleTransferType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleSourceLocation = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleTargetLocation = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygTransfer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTransferType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSourceLocation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTargetLocation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSourceResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTargetResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAssets = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAssets = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddAssets = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
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
        CType(Me.INDlyTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyTransfer.SuspendLayout()
        CType(Me.INDgcAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTargetResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSourceResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTransferType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSourceLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTargetLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTransferType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSourceLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTargetLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSourceResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTargetResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyTransfer)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1465, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyTransfer
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyTransfer
        '
        Me.INDlyTransfer.Controls.Add(Me.INDbtnAddAssets)
        Me.INDlyTransfer.Controls.Add(Me.INDgcAssets)
        Me.INDlyTransfer.Controls.Add(Me.INDsleTargetResponsible)
        Me.INDlyTransfer.Controls.Add(Me.INDsleSourceResponsible)
        Me.INDlyTransfer.Controls.Add(Me.INDsleTransferType)
        Me.INDlyTransfer.Controls.Add(Me.INDmemoObservations)
        Me.INDlyTransfer.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyTransfer.Controls.Add(Me.INDbtnCode)
        Me.INDlyTransfer.Controls.Add(Me.INDsleSourceLocation)
        Me.INDlyTransfer.Controls.Add(Me.INDsleTargetLocation)
        Me.INDlyTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyTransfer.Location = New System.Drawing.Point(202, 7)
        Me.INDlyTransfer.Name = "INDlyTransfer"
        Me.INDlyTransfer.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2146, 231, 479, 544)
        Me.INDlyTransfer.Root = Me.LayoutControlGroup1
        Me.INDlyTransfer.Size = New System.Drawing.Size(1261, 557)
        Me.INDlyTransfer.TabIndex = 1
        Me.INDlyTransfer.Text = "LayoutControl1"
        '
        'INDbtnAddAssets
        '
        Me.INDbtnAddAssets.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAssets.Appearance.Options.UseFont = True
        Me.INDbtnAddAssets.Location = New System.Drawing.Point(852, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAssets, True)
        Me.INDbtnAddAssets.Name = "INDbtnAddAssets"
        Me.INDbtnAddAssets.Size = New System.Drawing.Size(596, 32)
        Me.INDbtnAddAssets.StyleController = Me.INDlyTransfer
        Me.INDbtnAddAssets.TabIndex = 8
        Me.INDbtnAddAssets.Text = "Agregar"
        '
        'INDgcAssets
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAssets, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAssets, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAssets, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAssets, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAssets, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAssets, False)
        Me.INDgcAssets.Location = New System.Drawing.Point(852, 89)
        Me.INDgcAssets.MainView = Me.INDviewAssets
        Me.INDgcAssets.Name = "INDgcAssets"
        Me.INDgcAssets.Size = New System.Drawing.Size(596, 427)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAssets, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcAssets.TabIndex = 9
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
        Me.INDviewAssets.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewAssets.GridControl = Me.INDgcAssets
        Me.INDviewAssets.Name = "INDviewAssets"
        Me.INDviewAssets.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAssets.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAssets.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAssets.OptionsView.ShowDetailButtons = False
        Me.INDviewAssets.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAssets, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Articulo"
        Me.GridColumn3.FieldName = "ItemCodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Placa"
        Me.GridColumn5.FieldName = "Plate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Serie"
        Me.GridColumn6.FieldName = "Serie"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        '
        'INDsleTargetResponsible
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTargetResponsible, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTargetResponsible, AppearanceObject12)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTargetResponsible, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTargetResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.INDsleTargetResponsible.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.INDsleTargetResponsible.Location = New System.Drawing.Point(438, 319)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTargetResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTargetResponsible.Name = "INDsleTargetResponsible"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTargetResponsible, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.INDsleTargetResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleTargetResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTargetResponsible.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTargetResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTargetResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDsleTargetResponsible.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTargetResponsible.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleTargetResponsible.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleTargetResponsible.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleTargetResponsible.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleTargetResponsible.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleTargetResponsible.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleTargetResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTargetResponsible.Properties.DisplayMember = "CodeNitName"
        Me.INDsleTargetResponsible.Properties.NullText = ""
        Me.INDsleTargetResponsible.Properties.PopupSizeable = False
        Me.INDsleTargetResponsible.Properties.PopupView = Me.GridView2
        Me.INDsleTargetResponsible.Properties.ShowFooter = False
        Me.INDsleTargetResponsible.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTargetResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTargetResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTargetResponsible, True)
        Me.INDsleTargetResponsible.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTargetResponsible.StyleController = Me.INDlyTransfer
        Me.INDsleTargetResponsible.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTargetResponsible, "1711")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTargetResponsible, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTargetResponsible, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTargetResponsible, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTargetResponsible, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 259
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tercero"
        Me.GridColumn2.FieldName = "ThirdPartyId.NitName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 1133
        '
        'INDsleSourceResponsible
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleSourceResponsible, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleSourceResponsible, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSourceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleSourceResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.INDsleSourceResponsible.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.INDsleSourceResponsible.Location = New System.Drawing.Point(438, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSourceResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSourceResponsible.Name = "INDsleSourceResponsible"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleSourceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.INDsleSourceResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleSourceResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleSourceResponsible.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleSourceResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSourceResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDsleSourceResponsible.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleSourceResponsible.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleSourceResponsible.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleSourceResponsible.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSourceResponsible.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleSourceResponsible.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleSourceResponsible.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleSourceResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSourceResponsible.Properties.DisplayMember = "CodeNitName"
        Me.INDsleSourceResponsible.Properties.NullText = ""
        Me.INDsleSourceResponsible.Properties.PopupSizeable = False
        Me.INDsleSourceResponsible.Properties.PopupView = Me.GridView1
        Me.INDsleSourceResponsible.Properties.ShowFooter = False
        Me.INDsleSourceResponsible.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleSourceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleSourceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleSourceResponsible, True)
        Me.INDsleSourceResponsible.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSourceResponsible.StyleController = Me.INDlyTransfer
        Me.INDsleSourceResponsible.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleSourceResponsible, "1711")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSourceResponsible, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleSourceResponsible, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleSourceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleSourceResponsible, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn18})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Código"
        Me.GridColumn16.FieldName = "Code"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        Me.GridColumn16.Width = 259
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Tercero"
        Me.GridColumn18.FieldName = "ThirdPartyId.NitName"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 1
        Me.GridColumn18.Width = 1133
        '
        'INDsleTransferType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTransferType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTransferType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTransferType, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTransferType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTransferType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTransferType, False)
        Me.INDsleTransferType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTransferType, False)
        Me.INDsleTransferType.Location = New System.Drawing.Point(438, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTransferType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTransferType.Name = "INDsleTransferType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTransferType, False)
        Me.INDsleTransferType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleTransferType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTransferType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTransferType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTransferType.Properties.Appearance.Options.UseFont = True
        Me.INDsleTransferType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTransferType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleTransferType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleTransferType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleTransferType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleTransferType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleTransferType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleTransferType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTransferType.Properties.DisplayMember = "Item2"
        Me.INDsleTransferType.Properties.NullText = ""
        Me.INDsleTransferType.Properties.PopupSizeable = False
        Me.INDsleTransferType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleTransferType.Properties.ShowFooter = False
        Me.INDsleTransferType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTransferType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTransferType, True)
        Me.INDsleTransferType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTransferType.StyleController = Me.INDlyTransfer
        Me.INDsleTransferType.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTransferType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTransferType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTransferType, "{0} - {1}")
        Me.INDsleTransferType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTransferType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTransferType, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
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
        Me.INDmemoObservations.StyleController = Me.INDlyTransfer
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
        Me.INDdteDocumentDate.StyleController = Me.INDlyTransfer
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
        Me.INDbtnCode.StyleController = Me.INDlyTransfer
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDsleSourceLocation
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleSourceLocation, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleSourceLocation, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleSourceLocation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleSourceLocation, False)
        Me.INDsleSourceLocation.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleSourceLocation, False)
        Me.INDsleSourceLocation.Location = New System.Drawing.Point(438, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSourceLocation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSourceLocation.Name = "INDsleSourceLocation"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleSourceLocation, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleSourceLocation, False)
        Me.INDsleSourceLocation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleSourceLocation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleSourceLocation.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleSourceLocation.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSourceLocation.Properties.Appearance.Options.UseFont = True
        Me.INDsleSourceLocation.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleSourceLocation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleSourceLocation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleSourceLocation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSourceLocation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleSourceLocation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleSourceLocation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleSourceLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSourceLocation.Properties.DisplayMember = "CodeName"
        Me.INDsleSourceLocation.Properties.NullText = ""
        Me.INDsleSourceLocation.Properties.PopupSizeable = False
        Me.INDsleSourceLocation.Properties.PopupView = Me.GridView3
        Me.INDsleSourceLocation.Properties.ShowFooter = False
        Me.INDsleSourceLocation.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleSourceLocation, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleSourceLocation, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleSourceLocation, True)
        Me.INDsleSourceLocation.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSourceLocation.StyleController = Me.INDlyTransfer
        Me.INDsleSourceLocation.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleSourceLocation, "1100")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSourceLocation, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleSourceLocation, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleSourceLocation, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleSourceLocation, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Code"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 294
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Name"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 1098
        '
        'INDsleTargetLocation
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTargetLocation, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTargetLocation, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTargetLocation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTargetLocation, False)
        Me.INDsleTargetLocation.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTargetLocation, False)
        Me.INDsleTargetLocation.Location = New System.Drawing.Point(438, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTargetLocation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTargetLocation.Name = "INDsleTargetLocation"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTargetLocation, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTargetLocation, False)
        Me.INDsleTargetLocation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleTargetLocation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTargetLocation.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTargetLocation.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTargetLocation.Properties.Appearance.Options.UseFont = True
        Me.INDsleTargetLocation.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTargetLocation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleTargetLocation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleTargetLocation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleTargetLocation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleTargetLocation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleTargetLocation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleTargetLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTargetLocation.Properties.DisplayMember = "CodeName"
        Me.INDsleTargetLocation.Properties.NullText = ""
        Me.INDsleTargetLocation.Properties.PopupSizeable = False
        Me.INDsleTargetLocation.Properties.PopupView = Me.GridView4
        Me.INDsleTargetLocation.Properties.ShowFooter = False
        Me.INDsleTargetLocation.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTargetLocation, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTargetLocation, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTargetLocation, True)
        Me.INDsleTargetLocation.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTargetLocation.StyleController = Me.INDlyTransfer
        Me.INDsleTargetLocation.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTargetLocation, "1100")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTargetLocation, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTargetLocation, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTargetLocation, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTargetLocation, False)
        '
        'GridView4
        '
        Me.GridView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView4.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.GroupRow.Options.UseFont = True
        Me.GridView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView4, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Código"
        Me.GridColumn9.FieldName = "Code"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 294
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Nombre"
        Me.GridColumn10.FieldName = "Name"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        Me.GridColumn10.Width = 1098
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygTransfer, Me.INDlygAssets})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1472, 540)
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
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 520)
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
        Me.INDlyItemObservations.Size = New System.Drawing.Size(390, 347)
        Me.INDlyItemObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservations.Text = "Observaciones"
        Me.INDlyItemObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemObservations.TextToControlDistance = 5
        '
        'INDlygTransfer
        '
        Me.INDlygTransfer.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygTransfer.AppearanceGroup.Options.UseFont = True
        Me.INDlygTransfer.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygTransfer.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygTransfer.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTransfer.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygTransfer.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygTransfer.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygTransfer.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTransfer.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygTransfer.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTransfer.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygTransfer.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTransfer.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygTransfer, False)
        Me.INDlygTransfer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTransferType, Me.INDlyItemSourceLocation, Me.INDlyItemTargetLocation, Me.INDlyItemSourceResponsible, Me.INDlyItemTargetResponsible})
        Me.INDlygTransfer.Location = New System.Drawing.Point(414, 0)
        Me.INDlygTransfer.Name = "INDlygTransfer"
        Me.INDlygTransfer.Size = New System.Drawing.Size(414, 520)
        Me.INDlygTransfer.Text = "Traslado"
        '
        'INDlyItemTransferType
        '
        Me.INDlyItemTransferType.Control = Me.INDsleTransferType
        Me.INDlyItemTransferType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemTransferType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTransferType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTransferType.Name = "INDlyItemTransferType"
        Me.INDlyItemTransferType.ShowInCustomizationForm = False
        Me.INDlyItemTransferType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemTransferType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTransferType.Text = "Tipo Traslado"
        Me.INDlyItemTransferType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTransferType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTransferType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTransferType.TextToControlDistance = 5
        '
        'INDlyItemSourceLocation
        '
        Me.INDlyItemSourceLocation.Control = Me.INDsleSourceLocation
        Me.INDlyItemSourceLocation.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemSourceLocation.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSourceLocation.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSourceLocation.Name = "INDlyItemSourceLocation"
        Me.INDlyItemSourceLocation.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemSourceLocation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSourceLocation.Text = "Localización Origen"
        Me.INDlyItemSourceLocation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSourceLocation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSourceLocation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSourceLocation.TextToControlDistance = 5
        Me.INDlyItemSourceLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemTargetLocation
        '
        Me.INDlyItemTargetLocation.Control = Me.INDsleTargetLocation
        Me.INDlyItemTargetLocation.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemTargetLocation.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTargetLocation.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTargetLocation.Name = "INDlyItemTargetLocation"
        Me.INDlyItemTargetLocation.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemTargetLocation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTargetLocation.Text = "Localización Destino"
        Me.INDlyItemTargetLocation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTargetLocation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTargetLocation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTargetLocation.TextToControlDistance = 5
        Me.INDlyItemTargetLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemSourceResponsible
        '
        Me.INDlyItemSourceResponsible.Control = Me.INDsleSourceResponsible
        Me.INDlyItemSourceResponsible.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemSourceResponsible.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSourceResponsible.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSourceResponsible.Name = "INDlyItemSourceResponsible"
        Me.INDlyItemSourceResponsible.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemSourceResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSourceResponsible.Text = "Responsable Origen"
        Me.INDlyItemSourceResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSourceResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSourceResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSourceResponsible.TextToControlDistance = 5
        Me.INDlyItemSourceResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemTargetResponsible
        '
        Me.INDlyItemTargetResponsible.Control = Me.INDsleTargetResponsible
        Me.INDlyItemTargetResponsible.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemTargetResponsible.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTargetResponsible.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTargetResponsible.Name = "INDlyItemTargetResponsible"
        Me.INDlyItemTargetResponsible.Size = New System.Drawing.Size(390, 227)
        Me.INDlyItemTargetResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTargetResponsible.Text = "Responsable Destino"
        Me.INDlyItemTargetResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTargetResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTargetResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTargetResponsible.TextToControlDistance = 5
        Me.INDlyItemTargetResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.INDlygAssets.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAssets, Me.INDlyItemAddAssets})
        Me.INDlygAssets.Location = New System.Drawing.Point(828, 0)
        Me.INDlygAssets.Name = "INDlygAssets"
        Me.INDlygAssets.Size = New System.Drawing.Size(624, 520)
        Me.INDlygAssets.Text = "Activos"
        '
        'INDlyItemAssets
        '
        Me.INDlyItemAssets.Control = Me.INDgcAssets
        Me.INDlyItemAssets.CustomizationFormText = "Listado de Activos"
        Me.INDlyItemAssets.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemAssets.MaxSize = New System.Drawing.Size(600, 0)
        Me.INDlyItemAssets.MinSize = New System.Drawing.Size(600, 24)
        Me.INDlyItemAssets.Name = "INDlyItemAssets"
        Me.INDlyItemAssets.Size = New System.Drawing.Size(600, 431)
        Me.INDlyItemAssets.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAssets.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAssets.TextVisible = False
        '
        'INDlyItemAddAssets
        '
        Me.INDlyItemAddAssets.Control = Me.INDbtnAddAssets
        Me.INDlyItemAddAssets.CustomizationFormText = "Agregar Activos"
        Me.INDlyItemAddAssets.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddAssets.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemAddAssets.MinSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemAddAssets.Name = "INDlyItemAddAssets"
        Me.INDlyItemAddAssets.Size = New System.Drawing.Size(600, 36)
        Me.INDlyItemAddAssets.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddAssets.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddAssets.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit2
        '
        'FrmFixedAssetTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetTransfer"
        Me.Opacity = 1.0R
        Me.Tag = "1777"
        Me.Text = "Traslado de Activos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyTransfer.ResumeLayout(False)
        CType(Me.INDgcAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTargetResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSourceResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTransferType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSourceLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTargetLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTransferType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSourceLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTargetLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSourceResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTargetResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyTransfer As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDdteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleTransferType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygTransfer As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemTransferType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSourceLocation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTargetLocation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleSourceResponsible As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemSourceResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleTargetResponsible As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemTargetResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcAssets As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAssets As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygAssets As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemAssets As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnAddAssets As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddAssets As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleSourceLocation As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleTargetLocation As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
End Class
