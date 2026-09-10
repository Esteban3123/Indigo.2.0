Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCashReceivableLiquidation
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
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCashReceivableLiquidation))
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccPaymentMethod = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrPopupPaymentMethod1 = New Presentation.Billing.CtrPopupPaymentMethod()
        Me.INDMeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSleThirdParty = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSleThirdParty = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn231 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn234 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn235 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn236 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcMethodPayment = New DevExpress.XtraGrid.GridControl()
        Me.INDGvMethodPayment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColPaymentMethodType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRpType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumnValueAdvance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDSleCostCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDPceMethodPayment = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDsleCash = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGleTypeFundRaising = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleBankAccount = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCash = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeFundRaising = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBankAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit22 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDpccPaymentMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccPaymentMethod.SuspendLayout()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdParty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSleThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcMethodPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvMethodPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceMethodPayment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCash.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTypeFundRaising.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBankAccount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeFundRaising, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1698, 503)
        Me.INDPanelControlBase.TabIndex = 0
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1698, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1698, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDpccPaymentMethod)
        Me.INDlcRoot.Controls.Add(Me.INDMeDetail)
        Me.INDlcRoot.Controls.Add(Me.INDSleThirdParty)
        Me.INDlcRoot.Controls.Add(Me.INDGcMethodPayment)
        Me.INDlcRoot.Controls.Add(Me.INDSleCostCenter)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Controls.Add(Me.INDPceMethodPayment)
        Me.INDlcRoot.Controls.Add(Me.INDsleCash)
        Me.INDlcRoot.Controls.Add(Me.INDSleCurrency)
        Me.INDlcRoot.Controls.Add(Me.INDGleTypeFundRaising)
        Me.INDlcRoot.Controls.Add(Me.INDsleBankAccount)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 8)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.LayoutControlGroup1
        Me.INDlcRoot.Size = New System.Drawing.Size(1494, 493)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDpccPaymentMethod
        '
        Me.INDpccPaymentMethod.Controls.Add(Me.CtrPopupPaymentMethod1)
        Me.INDpccPaymentMethod.Location = New System.Drawing.Point(471, 225)
        Me.INDpccPaymentMethod.Name = "INDpccPaymentMethod"
        Me.INDpccPaymentMethod.Size = New System.Drawing.Size(420, 165)
        Me.INDpccPaymentMethod.TabIndex = 21
        '
        'CtrPopupPaymentMethod1
        '
        Me.CtrPopupPaymentMethod1.BankAccountXPO = Nothing
        Me.CtrPopupPaymentMethod1.BankXPO = Nothing
        Me.CtrPopupPaymentMethod1.CardXPO = Nothing
        Me.CtrPopupPaymentMethod1.ClosePopupChangePaymentMethod = False
        Me.CtrPopupPaymentMethod1.DepositDate = Nothing
        Me.CtrPopupPaymentMethod1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrPopupPaymentMethod1.EditedValueFromTheForm = False
        Me.CtrPopupPaymentMethod1.listPaymentMethodsAdded = Nothing
        Me.CtrPopupPaymentMethod1.Location = New System.Drawing.Point(0, 0)
        Me.CtrPopupPaymentMethod1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CtrPopupPaymentMethod1.Name = "CtrPopupPaymentMethod1"
        Me.CtrPopupPaymentMethod1.OperatingUnitId = 0
        Me.CtrPopupPaymentMethod1.Size = New System.Drawing.Size(420, 165)
        Me.CtrPopupPaymentMethod1.TabIndex = 0
        Me.CtrPopupPaymentMethod1.TagForm = Nothing
        '
        'INDMeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDetail, False)
        Me.INDMeDetail.EnterMoveNextControl = True
        Me.INDMeDetail.Location = New System.Drawing.Point(24, 498)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDetail.Name = "INDMeDetail"
        Me.INDMeDetail.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDMeDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDetail.Properties.Appearance.Options.UseFont = True
        Me.INDMeDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDetail.Size = New System.Drawing.Size(386, 51)
        Me.INDMeDetail.StyleController = Me.INDlcRoot
        Me.INDMeDetail.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDetail, 0)
        '
        'INDSleThirdParty
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleThirdParty, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleThirdParty, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleThirdParty, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleThirdParty, False)
        Me.INDSleThirdParty.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleThirdParty, False)
        Me.INDSleThirdParty.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleThirdParty, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleThirdParty.Name = "INDSleThirdParty"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleThirdParty, False)
        Me.INDSleThirdParty.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleThirdParty.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleThirdParty.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleThirdParty.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleThirdParty.Properties.Appearance.Options.UseFont = True
        Me.INDSleThirdParty.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleThirdParty.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleThirdParty.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleThirdParty.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleThirdParty.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleThirdParty.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleThirdParty.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleThirdParty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleThirdParty.Properties.DisplayMember = "NitName"
        Me.INDSleThirdParty.Properties.NullText = ""
        Me.INDSleThirdParty.Properties.PopupFormMinSize = New System.Drawing.Size(900, 0)
        Me.INDSleThirdParty.Properties.PopupSizeable = False
        Me.INDSleThirdParty.Properties.PopupView = Me.INDGvSleThirdParty
        Me.INDSleThirdParty.Properties.ReadOnly = True
        Me.INDSleThirdParty.Properties.ShowFooter = False
        Me.INDSleThirdParty.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleThirdParty, True)
        Me.INDSleThirdParty.Size = New System.Drawing.Size(386, 28)
        Me.INDSleThirdParty.StyleController = Me.INDlcRoot
        Me.INDSleThirdParty.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleThirdParty, "532")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleThirdParty, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleThirdParty, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleThirdParty, False)
        '
        'INDGvSleThirdParty
        '
        Me.INDGvSleThirdParty.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSleThirdParty.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSleThirdParty.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSleThirdParty.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSleThirdParty.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSleThirdParty.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSleThirdParty.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSleThirdParty.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSleThirdParty.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSleThirdParty.Appearance.Row.Options.UseFont = True
        Me.INDGvSleThirdParty.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn29, Me.GridColumn30, Me.GridColumn231, Me.GridColumn234, Me.GridColumn235, Me.GridColumn236})
        Me.INDGvSleThirdParty.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSleThirdParty.Name = "INDGvSleThirdParty"
        Me.INDGvSleThirdParty.OptionsFind.FindFilterColumns = "Nit"
        Me.INDGvSleThirdParty.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSleThirdParty.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSleThirdParty.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSleThirdParty.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSleThirdParty.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSleThirdParty, False)
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Nit"
        Me.GridColumn29.FieldName = "Nit"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 0
        Me.GridColumn29.Width = 167
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Tipo Documento"
        Me.GridColumn30.FieldName = "PersonId.IdentificationTypeName"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 1
        Me.GridColumn30.Width = 240
        '
        'GridColumn231
        '
        Me.GridColumn231.Caption = "Nombre Completo"
        Me.GridColumn231.FieldName = "Name"
        Me.GridColumn231.Name = "GridColumn231"
        Me.GridColumn231.Visible = True
        Me.GridColumn231.VisibleIndex = 2
        Me.GridColumn231.Width = 383
        '
        'GridColumn234
        '
        Me.GridColumn234.Caption = "Ciudad"
        Me.GridColumn234.FieldName = "PersonId.IdentificacionCityId.Descripcion"
        Me.GridColumn234.Name = "GridColumn234"
        Me.GridColumn234.Visible = True
        Me.GridColumn234.VisibleIndex = 3
        Me.GridColumn234.Width = 187
        '
        'GridColumn235
        '
        Me.GridColumn235.Caption = "Tipo Ret."
        Me.GridColumn235.FieldName = "RetentionTypeName"
        Me.GridColumn235.Name = "GridColumn235"
        Me.GridColumn235.Visible = True
        Me.GridColumn235.VisibleIndex = 4
        Me.GridColumn235.Width = 203
        '
        'GridColumn236
        '
        Me.GridColumn236.Caption = "Tipo Contr"
        Me.GridColumn236.FieldName = "ContributionTypeName"
        Me.GridColumn236.Name = "GridColumn236"
        Me.GridColumn236.Visible = True
        Me.GridColumn236.VisibleIndex = 5
        Me.GridColumn236.Width = 234
        '
        'INDGcMethodPayment
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcMethodPayment, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcMethodPayment, Nothing)
        Me.INDGcMethodPayment.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcMethodPayment, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcMethodPayment, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcMethodPayment, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcMethodPayment, False)
        Me.INDGcMethodPayment.Location = New System.Drawing.Point(438, 89)
        Me.INDGcMethodPayment.MainView = Me.INDGvMethodPayment
        Me.INDGcMethodPayment.Name = "INDGcMethodPayment"
        Me.INDGcMethodPayment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.INDRpType})
        Me.INDGcMethodPayment.Size = New System.Drawing.Size(496, 460)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcMethodPayment, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcMethodPayment.TabIndex = 7
        Me.INDGcMethodPayment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvMethodPayment})
        '
        'INDGvMethodPayment
        '
        Me.INDGvMethodPayment.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvMethodPayment.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvMethodPayment.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvMethodPayment.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvMethodPayment.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvMethodPayment.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMethodPayment.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvMethodPayment.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMethodPayment.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvMethodPayment.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvMethodPayment.Appearance.Row.Options.UseFont = True
        Me.INDGvMethodPayment.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvMethodPayment.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvMethodPayment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColPaymentMethodType, Me.GridColumnValueAdvance})
        Me.INDGvMethodPayment.GridControl = Me.INDGcMethodPayment
        Me.INDGvMethodPayment.Name = "INDGvMethodPayment"
        Me.INDGvMethodPayment.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvMethodPayment.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvMethodPayment.OptionsView.ShowAutoFilterRow = True
        Me.INDGvMethodPayment.OptionsView.ShowDetailButtons = False
        Me.INDGvMethodPayment.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvMethodPayment, False)
        '
        'INDColPaymentMethodType
        '
        Me.INDColPaymentMethodType.AppearanceCell.Options.UseTextOptions = True
        Me.INDColPaymentMethodType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDColPaymentMethodType.Caption = "Tipo"
        Me.INDColPaymentMethodType.ColumnEdit = Me.INDRpType
        Me.INDColPaymentMethodType.FieldName = "PaymentMethodTypes"
        Me.INDColPaymentMethodType.Name = "INDColPaymentMethodType"
        Me.INDColPaymentMethodType.OptionsColumn.AllowEdit = False
        Me.INDColPaymentMethodType.OptionsColumn.AllowFocus = False
        Me.INDColPaymentMethodType.Visible = True
        Me.INDColPaymentMethodType.VisibleIndex = 0
        Me.INDColPaymentMethodType.Width = 219
        '
        'INDRpType
        '
        Me.INDRpType.AutoHeight = False
        Me.INDRpType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Efectivo", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cheque", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tarjeta", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Consignación", CType(4, Byte), -1)})
        Me.INDRpType.Name = "INDRpType"
        '
        'GridColumnValueAdvance
        '
        Me.GridColumnValueAdvance.Caption = "Valor"
        Me.GridColumnValueAdvance.DisplayFormat.FormatString = "c2"
        Me.GridColumnValueAdvance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnValueAdvance.FieldName = "Value"
        Me.GridColumnValueAdvance.Name = "GridColumnValueAdvance"
        Me.GridColumnValueAdvance.OptionsColumn.AllowEdit = False
        Me.GridColumnValueAdvance.OptionsColumn.AllowFocus = False
        Me.GridColumnValueAdvance.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "Total: {0:c2}")})
        Me.GridColumnValueAdvance.Visible = True
        Me.GridColumnValueAdvance.VisibleIndex = 1
        Me.GridColumnValueAdvance.Width = 149
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "c0"
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.EditFormat.FormatString = "c0"
        Me.RepositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.Mask.EditMask = "c0"
        Me.RepositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = True
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'INDSleCostCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCostCenter, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCostCenter, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCostCenter, False)
        Me.INDSleCostCenter.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCostCenter, False)
        Me.INDSleCostCenter.Location = New System.Drawing.Point(24, 439)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCostCenter.Name = "INDSleCostCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCostCenter, False)
        Me.INDSleCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleCostCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCostCenter.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCostCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCostCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleCostCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleCostCenter.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCostCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleCostCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleCostCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCostCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCostCenter.Properties.DisplayMember = "CodeName"
        Me.INDSleCostCenter.Properties.NullText = ""
        Me.INDSleCostCenter.Properties.PopupSizeable = False
        Me.INDSleCostCenter.Properties.PopupView = Me.SearchLookUpEdit3View
        Me.INDSleCostCenter.Properties.ShowFooter = False
        Me.INDSleCostCenter.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCostCenter, True)
        Me.INDSleCostCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDSleCostCenter.StyleController = Me.INDlcRoot
        Me.INDSleCostCenter.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCostCenter, "517")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCostCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCostCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCostCenter, False)
        '
        'SearchLookUpEdit3View
        '
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn27, Me.GridColumn28})
        Me.SearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit3View.Name = "SearchLookUpEdit3View"
        Me.SearchLookUpEdit3View.OptionsFind.FindFilterColumns = "Codigo"
        Me.SearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit3View, False)
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Código"
        Me.GridColumn27.FieldName = "Codigo"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 0
        Me.GridColumn27.Width = 308
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Nombre"
        Me.GridColumn28.FieldName = "Descripcion"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 1
        Me.GridColumn28.Width = 1004
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, False)
        Me.INDbteCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Billing.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDPceMethodPayment
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceMethodPayment, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceMethodPayment, Nothing)
        Me.INDPceMethodPayment.Location = New System.Drawing.Point(438, 53)
        Me.INDPceMethodPayment.MaximumSize = New System.Drawing.Size(496, 0)
        Me.INDPceMethodPayment.MinimumSize = New System.Drawing.Size(496, 32)
        Me.INDPceMethodPayment.Name = "INDPceMethodPayment"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceMethodPayment, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceMethodPayment, False)
        Me.INDPceMethodPayment.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceMethodPayment.Properties.Appearance.Options.UseFont = True
        Me.INDPceMethodPayment.Properties.AutoHeight = False
        Me.INDPceMethodPayment.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions2.Image = CType(resources.GetObject("EditorButtonImageOptions2.Image"), System.Drawing.Image)
        Me.INDPceMethodPayment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDPceMethodPayment.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDPceMethodPayment.Properties.PopupControl = Me.INDpccPaymentMethod
        Me.INDPceMethodPayment.Properties.PopupSizeable = False
        Me.INDPceMethodPayment.Properties.ShowPopupCloseButton = False
        Me.INDPceMethodPayment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDPceMethodPayment.Size = New System.Drawing.Size(496, 32)
        Me.INDPceMethodPayment.StyleController = Me.INDlcRoot
        Me.INDPceMethodPayment.TabIndex = 5
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceMethodPayment, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceMethodPayment, Nothing)
        '
        'INDsleCash
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCash, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCash, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCash, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCash, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCash, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCash, False)
        Me.INDsleCash.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCash, False)
        Me.INDsleCash.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCash, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCash.Name = "INDsleCash"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCash, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCash, False)
        Me.INDsleCash.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleCash.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCash.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCash.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCash.Properties.Appearance.Options.UseFont = True
        Me.INDsleCash.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCash.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCash.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCash.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCash.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCash.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCash.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCash.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCash.Properties.DisplayMember = "CodeName"
        Me.INDsleCash.Properties.NullText = ""
        Me.INDsleCash.Properties.PopupSizeable = False
        Me.INDsleCash.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleCash.Properties.ShowFooter = False
        Me.INDsleCash.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCash, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCash, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCash, True)
        Me.INDsleCash.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCash.StyleController = Me.INDlcRoot
        Me.INDsleCash.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCash, "626")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCash, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCash, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCash, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCash, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 299
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 502
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cuenta Contable"
        Me.GridColumn3.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 591
        '
        'INDSleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCurrency, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCurrency, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCurrency, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCurrency, False)
        Me.INDSleCurrency.Enabled = False
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCurrency, False)
        Me.INDSleCurrency.Location = New System.Drawing.Point(24, 379)
        Me.INDSleCurrency.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCurrency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCurrency.Name = "INDSleCurrency"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCurrency, False)
        Me.INDSleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDSleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCurrency.Properties.NullText = ""
        Me.INDSleCurrency.Properties.PopupSizeable = False
        Me.INDSleCurrency.Properties.PopupView = Me.GridView1
        Me.INDSleCurrency.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCurrency, True)
        Me.INDSleCurrency.Size = New System.Drawing.Size(386, 28)
        Me.INDSleCurrency.StyleController = Me.INDlcRoot
        Me.INDSleCurrency.TabIndex = 22
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCurrency, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCurrency, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCurrency, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 284
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDGleTypeFundRaising
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeFundRaising, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleTypeFundRaising, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleTypeFundRaising, False)
        Me.INDGleTypeFundRaising.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeFundRaising, True)
        Me.INDGleTypeFundRaising.Location = New System.Drawing.Point(24, 195)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleTypeFundRaising, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleTypeFundRaising.Name = "INDGleTypeFundRaising"
        Me.INDGleTypeFundRaising.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDGleTypeFundRaising.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeFundRaising.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDGleTypeFundRaising.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleTypeFundRaising.Properties.Appearance.Options.UseFont = True
        Me.INDGleTypeFundRaising.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleTypeFundRaising.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeFundRaising.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeFundRaising.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleTypeFundRaising.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleTypeFundRaising.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleTypeFundRaising.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleTypeFundRaising.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTypeFundRaising.Properties.DisplayMember = "Item2"
        Me.INDGleTypeFundRaising.Properties.ImmediatePopup = True
        Me.INDGleTypeFundRaising.Properties.NullText = ""
        Me.INDGleTypeFundRaising.Properties.PopupSizeable = False
        Me.INDGleTypeFundRaising.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleTypeFundRaising.Properties.ShowFooter = False
        Me.INDGleTypeFundRaising.Properties.ValueMember = "Item1"
        Me.INDGleTypeFundRaising.Size = New System.Drawing.Size(386, 28)
        Me.INDGleTypeFundRaising.StyleController = Me.INDlcRoot
        Me.INDGleTypeFundRaising.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeFundRaising, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleTypeFundRaising, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 299
        '
        'INDsleBankAccount
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBankAccount, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBankAccount, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleBankAccount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBankAccount, False)
        Me.INDsleBankAccount.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBankAccount, False)
        Me.INDsleBankAccount.Location = New System.Drawing.Point(24, 315)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBankAccount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBankAccount.Name = "INDsleBankAccount"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBankAccount, False)
        Me.INDsleBankAccount.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleBankAccount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBankAccount.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleBankAccount.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBankAccount.Properties.Appearance.Options.UseFont = True
        Me.INDsleBankAccount.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleBankAccount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleBankAccount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleBankAccount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleBankAccount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleBankAccount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleBankAccount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleBankAccount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleBankAccount.Properties.DisplayMember = "CodeBankAccount"
        Me.INDsleBankAccount.Properties.NullText = ""
        Me.INDsleBankAccount.Properties.PopupSizeable = False
        Me.INDsleBankAccount.Properties.PopupView = Me.SearchLookUpEdit1View1
        Me.INDsleBankAccount.Properties.ShowFooter = False
        Me.INDsleBankAccount.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBankAccount, True)
        Me.INDsleBankAccount.Size = New System.Drawing.Size(386, 28)
        Me.INDsleBankAccount.StyleController = Me.INDlcRoot
        Me.INDsleBankAccount.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBankAccount, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBankAccount, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBankAccount, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBankAccount, False)
        '
        'SearchLookUpEdit1View1
        '
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn21, Me.GridColumn5, Me.GridColumn31})
        Me.SearchLookUpEdit1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View1.Name = "SearchLookUpEdit1View1"
        Me.SearchLookUpEdit1View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View1, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Código"
        Me.GridColumn11.FieldName = "Code"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        Me.GridColumn11.Width = 299
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Nombre"
        Me.GridColumn21.FieldName = "IdBank.Name"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 1
        Me.GridColumn21.Width = 502
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Número cuenta"
        Me.GridColumn5.FieldName = "Number"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Cuenta Contable"
        Me.GridColumn31.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 3
        Me.GridColumn31.Width = 591
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3, Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1477, 573)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCash, Me.INDliCode, Me.INDliCostCenter, Me.INDliThirdParty, Me.INDLciDetail, Me.INDlciCurrency, Me.INDLciTypeFundRaising, Me.INDLciBankAccount})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(414, 553)
        Me.LayoutControlGroup3.Text = "Datos Principales"
        '
        'INDliCash
        '
        Me.INDliCash.Control = Me.INDsleCash
        Me.INDliCash.CustomizationFormText = "Caja"
        Me.INDliCash.Location = New System.Drawing.Point(0, 180)
        Me.INDliCash.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCash.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCash.Name = "INDliCash"
        Me.INDliCash.Size = New System.Drawing.Size(390, 60)
        Me.INDliCash.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCash.Text = "Caja"
        Me.INDliCash.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCash.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCash.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliCash.TextToControlDistance = 5
        Me.INDliCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDliCode
        '
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "Código"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.Size = New System.Drawing.Size(390, 60)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCode.TextSize = New System.Drawing.Size(50, 21)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDliCostCenter
        '
        Me.INDliCostCenter.Control = Me.INDSleCostCenter
        Me.INDliCostCenter.CustomizationFormText = "Centro Costo"
        Me.INDliCostCenter.Location = New System.Drawing.Point(0, 360)
        Me.INDliCostCenter.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCostCenter.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCostCenter.Name = "INDliCostCenter"
        Me.INDliCostCenter.Size = New System.Drawing.Size(390, 60)
        Me.INDliCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCostCenter.Text = "Centro Costo"
        Me.INDliCostCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCostCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCostCenter.TextSize = New System.Drawing.Size(50, 21)
        Me.INDliCostCenter.TextToControlDistance = 5
        '
        'INDliThirdParty
        '
        Me.INDliThirdParty.Control = Me.INDSleThirdParty
        Me.INDliThirdParty.CustomizationFormText = "Tercero"
        Me.INDliThirdParty.Location = New System.Drawing.Point(0, 60)
        Me.INDliThirdParty.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliThirdParty.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliThirdParty.Name = "INDliThirdParty"
        Me.INDliThirdParty.Size = New System.Drawing.Size(390, 60)
        Me.INDliThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliThirdParty.Text = "Tercero"
        Me.INDliThirdParty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliThirdParty.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliThirdParty.TextSize = New System.Drawing.Size(50, 21)
        Me.INDliThirdParty.TextToControlDistance = 5
        '
        'INDLciDetail
        '
        Me.INDLciDetail.Control = Me.INDMeDetail
        Me.INDLciDetail.CustomizationFormText = "Detalle"
        Me.INDLciDetail.Location = New System.Drawing.Point(0, 420)
        Me.INDLciDetail.MaxSize = New System.Drawing.Size(390, 80)
        Me.INDLciDetail.MinSize = New System.Drawing.Size(390, 80)
        Me.INDLciDetail.Name = "INDLciDetail"
        Me.INDLciDetail.ShowInCustomizationForm = False
        Me.INDLciDetail.Size = New System.Drawing.Size(390, 80)
        Me.INDLciDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDetail.Text = "Detalle"
        Me.INDLciDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDetail.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciDetail.TextToControlDistance = 5
        '
        'INDlciCurrency
        '
        Me.INDlciCurrency.Control = Me.INDSleCurrency
        Me.INDlciCurrency.Location = New System.Drawing.Point(0, 300)
        Me.INDlciCurrency.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciCurrency.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciCurrency.Name = "INDlciCurrency"
        Me.INDlciCurrency.ShowInCustomizationForm = False
        Me.INDlciCurrency.Size = New System.Drawing.Size(390, 60)
        Me.INDlciCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCurrency.Text = "Moneda"
        Me.INDlciCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCurrency.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlciCurrency.TextToControlDistance = 5
        '
        'INDLciTypeFundRaising
        '
        Me.INDLciTypeFundRaising.Control = Me.INDGleTypeFundRaising
        Me.INDLciTypeFundRaising.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTypeFundRaising.CustomizationFormText = "Tipo de Recaudo"
        Me.INDLciTypeFundRaising.Location = New System.Drawing.Point(0, 120)
        Me.INDLciTypeFundRaising.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciTypeFundRaising.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciTypeFundRaising.Name = "INDLciTypeFundRaising"
        Me.INDLciTypeFundRaising.Size = New System.Drawing.Size(390, 60)
        Me.INDLciTypeFundRaising.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeFundRaising.Text = "Tipo de Recaudo"
        Me.INDLciTypeFundRaising.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTypeFundRaising.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeFundRaising.TextSize = New System.Drawing.Size(82, 17)
        Me.INDLciTypeFundRaising.TextToControlDistance = 5
        '
        'INDLciBankAccount
        '
        Me.INDLciBankAccount.Control = Me.INDsleBankAccount
        Me.INDLciBankAccount.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciBankAccount.CustomizationFormText = "Cuenta Bancaria"
        Me.INDLciBankAccount.Location = New System.Drawing.Point(0, 240)
        Me.INDLciBankAccount.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciBankAccount.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciBankAccount.Name = "INDLciBankAccount"
        Me.INDLciBankAccount.Size = New System.Drawing.Size(390, 60)
        Me.INDLciBankAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBankAccount.Text = "Cuenta Bancaria"
        Me.INDLciBankAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBankAccount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBankAccount.TextSize = New System.Drawing.Size(82, 17)
        Me.INDLciBankAccount.TextToControlDistance = 5
        Me.INDLciBankAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1043, 553)
        Me.LayoutControlGroup2.Text = "Forma de Pago"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDPceMethodPayment
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(500, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(500, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1019, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcMethodPayment
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(500, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(500, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1019, 464)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 493)
        Me.CtrNavigationControl1.TabIndex = 1
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'RepositoryItemPopupContainerEdit22
        '
        Me.RepositoryItemPopupContainerEdit22.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit22.Name = "RepositoryItemPopupContainerEdit22"
        '
        'FrmCashReceivableLiquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1698, 639)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCashReceivableLiquidation"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Tag = "635"
        Me.Text = "Recibos de Caja"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDpccPaymentMethod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccPaymentMethod.ResumeLayout(False)
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdParty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSleThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcMethodPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvMethodPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceMethodPayment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCash.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTypeFundRaising.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBankAccount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeFundRaising, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleCash As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliCash As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPceMethodPayment As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDpccPaymentMethod As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrPopupPaymentMethod1 As Presentation.Billing.CtrPopupPaymentMethod
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleCostCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDliCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcMethodPayment As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvMethodPayment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColPaymentMethodType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRpType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumnValueAdvance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleThirdParty As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvSleThirdParty As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn231 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn234 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn235 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn236 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDliThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMeDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleTypeFundRaising As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciTypeFundRaising As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleBankAccount As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciBankAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit22 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
