Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmGeneralLedgerIVA
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGeneralLedgerIVA))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtPercentage = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleAccountSale = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn18911 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19011 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19211 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19311 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleAccountCreditControlFiscal = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View111 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn189111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleAccountDebitControlFiscal = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View112 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn189112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleAccountPurchaseService = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View113 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn189113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRgApplyTaxDevolution = New DevExpress.XtraEditors.RadioGroup()
        Me.INDGlePaymentMethodTypes = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGleViewPaymentsMethods = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvPaymentsMUnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPaymentsMethods = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LcgGeneralLedgerIVA = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAccountSale = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAccountPurchaseService = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciApplyTaxDevolution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPaymentMethodTypes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LcgGeneralLedgerIVAFiscal = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAccountDebitControlFiscal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAccountCreditControlFiscal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit22 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit211 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit113 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit114 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridView3 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit23 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit231 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit115 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit232 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit116 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit233 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit117 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDTxtPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountSale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountCreditControlFiscal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountDebitControlFiscal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountPurchaseService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgApplyTaxDevolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGlePaymentMethodTypes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleViewPaymentsMethods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgGeneralLedgerIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccountSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccountPurchaseService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciApplyTaxDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPaymentMethodTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgGeneralLedgerIVAFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccountDebitControlFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccountCreditControlFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit211, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit114, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit231, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit115, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit232, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit116, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit233, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit117, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 594)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 130)
        '
        'INDLcBase
        '
        Me.INDLcBase.AllowCustomization = False
        Me.INDLcBase.Controls.Add(Me.INDTxtPercentage)
        Me.INDLcBase.Controls.Add(Me.INDTxtName)
        Me.INDLcBase.Controls.Add(Me.INDBteCode)
        Me.INDLcBase.Controls.Add(Me.INDsleAccountSale)
        Me.INDLcBase.Controls.Add(Me.INDsleAccountCreditControlFiscal)
        Me.INDLcBase.Controls.Add(Me.INDsleAccountDebitControlFiscal)
        Me.INDLcBase.Controls.Add(Me.INDsleAccountPurchaseService)
        Me.INDLcBase.Controls.Add(Me.INDRgApplyTaxDevolution)
        Me.INDLcBase.Controls.Add(Me.INDGlePaymentMethodTypes)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, False)
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 585)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDTxtPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPercentage, True)
        Me.INDTxtPercentage.EnterMoveNextControl = True
        Me.INDTxtPercentage.Location = New System.Drawing.Point(24, 214)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDTxtPercentage.Name = "INDTxtPercentage"
        Me.INDTxtPercentage.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPercentage.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPercentage.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtPercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtPercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtPercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPercentage.Properties.Mask.EditMask = "P"
        Me.INDTxtPercentage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtPercentage.StyleController = Me.INDLcBase
        Me.INDTxtPercentage.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPercentage, 0)
        Me.INDTxtPercentage.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        Me.INDTxtName.Location = New System.Drawing.Point(24, 150)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.MaxLength = 200
        Me.INDTxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtName.StyleController = Me.INDLcBase
        Me.INDTxtName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        Me.INDTxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.INDBteCode.Location = New System.Drawing.Point(24, 86)
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
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBteCode.StyleController = Me.INDLcBase
        Me.INDBteCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        Me.INDBteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDsleAccountSale
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountSale, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountSale, False)
        Me.INDsleAccountSale.EnterMoveNextControl = True
        Me.INDsleAccountSale.Location = New System.Drawing.Point(24, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountSale, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountSale.Name = "INDsleAccountSale"
        Me.INDsleAccountSale.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleAccountSale.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountSale.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleAccountSale.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountSale.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountSale.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAccountSale.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleAccountSale.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleAccountSale.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAccountSale.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAccountSale.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAccountSale.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAccountSale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountSale.Properties.DisplayMember = "NumberName"
        Me.INDsleAccountSale.Properties.NullText = ""
        Me.INDsleAccountSale.Properties.PopupFormMinSize = New System.Drawing.Size(800, 0)
        Me.INDsleAccountSale.Properties.PopupSizeable = False
        Me.INDsleAccountSale.Properties.PopupView = Me.SearchLookUpEdit1View11
        Me.INDsleAccountSale.Properties.ShowClearButton = False
        Me.INDsleAccountSale.Properties.ShowFooter = False
        Me.INDsleAccountSale.Properties.ValueMember = "Id"
        Me.INDsleAccountSale.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAccountSale.StyleController = Me.INDLcBase
        Me.INDsleAccountSale.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountSale, 0)
        '
        'SearchLookUpEdit1View11
        '
        Me.SearchLookUpEdit1View11.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View11.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View11.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View11.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View11.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View11.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View11.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View11.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View11.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View11.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View11.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18911, Me.GridColumn19011, Me.GridColumn19111, Me.GridColumn19211, Me.GridColumn19311})
        Me.SearchLookUpEdit1View11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View11.Name = "SearchLookUpEdit1View11"
        Me.SearchLookUpEdit1View11.OptionsFind.FindFilterColumns = "Number"
        Me.SearchLookUpEdit1View11.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View11.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View11.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View11.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View11.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View11.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View11, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View11, False)
        '
        'GridColumn18911
        '
        Me.GridColumn18911.Caption = "Id"
        Me.GridColumn18911.FieldName = "Id"
        Me.GridColumn18911.Name = "GridColumn18911"
        '
        'GridColumn19011
        '
        Me.GridColumn19011.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19011.Caption = "Código"
        Me.GridColumn19011.FieldName = "Number"
        Me.GridColumn19011.Name = "GridColumn19011"
        Me.GridColumn19011.Visible = True
        Me.GridColumn19011.VisibleIndex = 0
        Me.GridColumn19011.Width = 300
        '
        'GridColumn19111
        '
        Me.GridColumn19111.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19111.Caption = "Nombre"
        Me.GridColumn19111.FieldName = "Name"
        Me.GridColumn19111.Name = "GridColumn19111"
        Me.GridColumn19111.Visible = True
        Me.GridColumn19111.VisibleIndex = 1
        Me.GridColumn19111.Width = 350
        '
        'GridColumn19211
        '
        Me.GridColumn19211.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19211.Caption = "Maneja Tercero"
        Me.GridColumn19211.FieldName = "HandlesThirdParty"
        Me.GridColumn19211.Name = "GridColumn19211"
        Me.GridColumn19211.Visible = True
        Me.GridColumn19211.VisibleIndex = 2
        Me.GridColumn19211.Width = 350
        '
        'GridColumn19311
        '
        Me.GridColumn19311.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19311.Caption = "Maneja Centro de Costo"
        Me.GridColumn19311.FieldName = "HandlesCostCenter"
        Me.GridColumn19311.Name = "GridColumn19311"
        Me.GridColumn19311.Visible = True
        Me.GridColumn19311.VisibleIndex = 3
        Me.GridColumn19311.Width = 350
        '
        'INDsleAccountCreditControlFiscal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountCreditControlFiscal, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountCreditControlFiscal, False)
        Me.INDsleAccountCreditControlFiscal.EnterMoveNextControl = True
        Me.INDsleAccountCreditControlFiscal.Location = New System.Drawing.Point(438, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountCreditControlFiscal, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountCreditControlFiscal.Name = "INDsleAccountCreditControlFiscal"
        Me.INDsleAccountCreditControlFiscal.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleAccountCreditControlFiscal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountCreditControlFiscal.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleAccountCreditControlFiscal.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountCreditControlFiscal.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountCreditControlFiscal.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAccountCreditControlFiscal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleAccountCreditControlFiscal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleAccountCreditControlFiscal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAccountCreditControlFiscal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAccountCreditControlFiscal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAccountCreditControlFiscal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAccountCreditControlFiscal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountCreditControlFiscal.Properties.DisplayMember = "NumberName"
        Me.INDsleAccountCreditControlFiscal.Properties.NullText = ""
        Me.INDsleAccountCreditControlFiscal.Properties.PopupFormMinSize = New System.Drawing.Size(800, 0)
        Me.INDsleAccountCreditControlFiscal.Properties.PopupSizeable = False
        Me.INDsleAccountCreditControlFiscal.Properties.PopupView = Me.SearchLookUpEdit1View111
        Me.INDsleAccountCreditControlFiscal.Properties.ShowFooter = False
        Me.INDsleAccountCreditControlFiscal.Properties.ValueMember = "Id"
        Me.INDsleAccountCreditControlFiscal.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAccountCreditControlFiscal.StyleController = Me.INDLcBase
        Me.INDsleAccountCreditControlFiscal.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountCreditControlFiscal, 0)
        '
        'SearchLookUpEdit1View111
        '
        Me.SearchLookUpEdit1View111.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View111.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View111.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View111.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View111.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View111.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View111.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View111.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View111.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View111.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View111.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn189111, Me.GridColumn190111, Me.GridColumn191111, Me.GridColumn192111, Me.GridColumn193111})
        Me.SearchLookUpEdit1View111.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View111.Name = "SearchLookUpEdit1View111"
        Me.SearchLookUpEdit1View111.OptionsFind.FindFilterColumns = "Number"
        Me.SearchLookUpEdit1View111.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View111.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View111.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View111.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View111.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View111.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View111, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View111, False)
        '
        'GridColumn189111
        '
        Me.GridColumn189111.Caption = "Id"
        Me.GridColumn189111.FieldName = "Id"
        Me.GridColumn189111.Name = "GridColumn189111"
        '
        'GridColumn190111
        '
        Me.GridColumn190111.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn190111.Caption = "Código"
        Me.GridColumn190111.FieldName = "Number"
        Me.GridColumn190111.Name = "GridColumn190111"
        Me.GridColumn190111.Visible = True
        Me.GridColumn190111.VisibleIndex = 0
        Me.GridColumn190111.Width = 300
        '
        'GridColumn191111
        '
        Me.GridColumn191111.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn191111.Caption = "Nombre"
        Me.GridColumn191111.FieldName = "Name"
        Me.GridColumn191111.Name = "GridColumn191111"
        Me.GridColumn191111.Visible = True
        Me.GridColumn191111.VisibleIndex = 1
        Me.GridColumn191111.Width = 350
        '
        'GridColumn192111
        '
        Me.GridColumn192111.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn192111.Caption = "Maneja Tercero"
        Me.GridColumn192111.FieldName = "HandlesThirdParty"
        Me.GridColumn192111.Name = "GridColumn192111"
        Me.GridColumn192111.Visible = True
        Me.GridColumn192111.VisibleIndex = 2
        Me.GridColumn192111.Width = 350
        '
        'GridColumn193111
        '
        Me.GridColumn193111.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn193111.Caption = "Maneja Centro de Costo"
        Me.GridColumn193111.FieldName = "HandlesCostCenter"
        Me.GridColumn193111.Name = "GridColumn193111"
        Me.GridColumn193111.Visible = True
        Me.GridColumn193111.VisibleIndex = 3
        Me.GridColumn193111.Width = 350
        '
        'INDsleAccountDebitControlFiscal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountDebitControlFiscal, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountDebitControlFiscal, False)
        Me.INDsleAccountDebitControlFiscal.EnterMoveNextControl = True
        Me.INDsleAccountDebitControlFiscal.Location = New System.Drawing.Point(438, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountDebitControlFiscal, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountDebitControlFiscal.Name = "INDsleAccountDebitControlFiscal"
        Me.INDsleAccountDebitControlFiscal.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleAccountDebitControlFiscal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountDebitControlFiscal.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleAccountDebitControlFiscal.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountDebitControlFiscal.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountDebitControlFiscal.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAccountDebitControlFiscal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleAccountDebitControlFiscal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleAccountDebitControlFiscal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAccountDebitControlFiscal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAccountDebitControlFiscal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAccountDebitControlFiscal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAccountDebitControlFiscal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountDebitControlFiscal.Properties.DisplayMember = "NumberName"
        Me.INDsleAccountDebitControlFiscal.Properties.NullText = ""
        Me.INDsleAccountDebitControlFiscal.Properties.PopupFormMinSize = New System.Drawing.Size(800, 0)
        Me.INDsleAccountDebitControlFiscal.Properties.PopupSizeable = False
        Me.INDsleAccountDebitControlFiscal.Properties.PopupView = Me.SearchLookUpEdit1View112
        Me.INDsleAccountDebitControlFiscal.Properties.ShowFooter = False
        Me.INDsleAccountDebitControlFiscal.Properties.ValueMember = "Id"
        Me.INDsleAccountDebitControlFiscal.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAccountDebitControlFiscal.StyleController = Me.INDLcBase
        Me.INDsleAccountDebitControlFiscal.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountDebitControlFiscal, 0)
        '
        'SearchLookUpEdit1View112
        '
        Me.SearchLookUpEdit1View112.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View112.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View112.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View112.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View112.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View112.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View112.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View112.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View112.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View112.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View112.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn189112, Me.GridColumn190112, Me.GridColumn191112, Me.GridColumn192112, Me.GridColumn193112})
        Me.SearchLookUpEdit1View112.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View112.Name = "SearchLookUpEdit1View112"
        Me.SearchLookUpEdit1View112.OptionsFind.FindFilterColumns = "Number"
        Me.SearchLookUpEdit1View112.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View112.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View112.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View112.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View112.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View112.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View112, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View112, False)
        '
        'GridColumn189112
        '
        Me.GridColumn189112.Caption = "Id"
        Me.GridColumn189112.FieldName = "Id"
        Me.GridColumn189112.Name = "GridColumn189112"
        '
        'GridColumn190112
        '
        Me.GridColumn190112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn190112.Caption = "Código"
        Me.GridColumn190112.FieldName = "Number"
        Me.GridColumn190112.Name = "GridColumn190112"
        Me.GridColumn190112.Visible = True
        Me.GridColumn190112.VisibleIndex = 0
        Me.GridColumn190112.Width = 300
        '
        'GridColumn191112
        '
        Me.GridColumn191112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn191112.Caption = "Nombre"
        Me.GridColumn191112.FieldName = "Name"
        Me.GridColumn191112.Name = "GridColumn191112"
        Me.GridColumn191112.Visible = True
        Me.GridColumn191112.VisibleIndex = 1
        Me.GridColumn191112.Width = 350
        '
        'GridColumn192112
        '
        Me.GridColumn192112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn192112.Caption = "Maneja Tercero"
        Me.GridColumn192112.FieldName = "HandlesThirdParty"
        Me.GridColumn192112.Name = "GridColumn192112"
        Me.GridColumn192112.Visible = True
        Me.GridColumn192112.VisibleIndex = 2
        Me.GridColumn192112.Width = 350
        '
        'GridColumn193112
        '
        Me.GridColumn193112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn193112.Caption = "Maneja Centro de Costo"
        Me.GridColumn193112.FieldName = "HandlesCostCenter"
        Me.GridColumn193112.Name = "GridColumn193112"
        Me.GridColumn193112.Visible = True
        Me.GridColumn193112.VisibleIndex = 3
        Me.GridColumn193112.Width = 350
        '
        'INDsleAccountPurchaseService
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountPurchaseService, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountPurchaseService, False)
        Me.INDsleAccountPurchaseService.EnterMoveNextControl = True
        Me.INDsleAccountPurchaseService.Location = New System.Drawing.Point(24, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountPurchaseService, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountPurchaseService.Name = "INDsleAccountPurchaseService"
        Me.INDsleAccountPurchaseService.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleAccountPurchaseService.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountPurchaseService.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleAccountPurchaseService.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountPurchaseService.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountPurchaseService.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAccountPurchaseService.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleAccountPurchaseService.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleAccountPurchaseService.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAccountPurchaseService.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAccountPurchaseService.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAccountPurchaseService.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAccountPurchaseService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountPurchaseService.Properties.DisplayMember = "NumberName"
        Me.INDsleAccountPurchaseService.Properties.NullText = ""
        Me.INDsleAccountPurchaseService.Properties.PopupFormMinSize = New System.Drawing.Size(800, 0)
        Me.INDsleAccountPurchaseService.Properties.PopupSizeable = False
        Me.INDsleAccountPurchaseService.Properties.PopupView = Me.SearchLookUpEdit1View113
        Me.INDsleAccountPurchaseService.Properties.ShowClearButton = False
        Me.INDsleAccountPurchaseService.Properties.ShowFooter = False
        Me.INDsleAccountPurchaseService.Properties.ValueMember = "Id"
        Me.INDsleAccountPurchaseService.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAccountPurchaseService.StyleController = Me.INDLcBase
        Me.INDsleAccountPurchaseService.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountPurchaseService, 0)
        '
        'SearchLookUpEdit1View113
        '
        Me.SearchLookUpEdit1View113.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View113.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View113.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View113.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View113.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View113.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View113.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View113.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View113.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View113.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View113.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn189113, Me.GridColumn190113, Me.GridColumn191113, Me.GridColumn192113, Me.GridColumn193113})
        Me.SearchLookUpEdit1View113.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View113.Name = "SearchLookUpEdit1View113"
        Me.SearchLookUpEdit1View113.OptionsFind.FindFilterColumns = "Number"
        Me.SearchLookUpEdit1View113.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View113.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View113.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View113.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View113.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View113.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View113, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View113, False)
        '
        'GridColumn189113
        '
        Me.GridColumn189113.Caption = "Id"
        Me.GridColumn189113.FieldName = "Id"
        Me.GridColumn189113.Name = "GridColumn189113"
        '
        'GridColumn190113
        '
        Me.GridColumn190113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn190113.Caption = "Código"
        Me.GridColumn190113.FieldName = "Number"
        Me.GridColumn190113.Name = "GridColumn190113"
        Me.GridColumn190113.Visible = True
        Me.GridColumn190113.VisibleIndex = 0
        Me.GridColumn190113.Width = 300
        '
        'GridColumn191113
        '
        Me.GridColumn191113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn191113.Caption = "Nombre"
        Me.GridColumn191113.FieldName = "Name"
        Me.GridColumn191113.Name = "GridColumn191113"
        Me.GridColumn191113.Visible = True
        Me.GridColumn191113.VisibleIndex = 1
        Me.GridColumn191113.Width = 350
        '
        'GridColumn192113
        '
        Me.GridColumn192113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn192113.Caption = "Maneja Tercero"
        Me.GridColumn192113.FieldName = "HandlesThirdParty"
        Me.GridColumn192113.Name = "GridColumn192113"
        Me.GridColumn192113.Visible = True
        Me.GridColumn192113.VisibleIndex = 2
        Me.GridColumn192113.Width = 350
        '
        'GridColumn193113
        '
        Me.GridColumn193113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn193113.Caption = "Maneja Centro de Costo"
        Me.GridColumn193113.FieldName = "HandlesCostCenter"
        Me.GridColumn193113.Name = "GridColumn193113"
        Me.GridColumn193113.Visible = True
        Me.GridColumn193113.VisibleIndex = 3
        Me.GridColumn193113.Width = 350
        '
        'INDRgApplyTaxDevolution
        '
        Me.INDRgApplyTaxDevolution.EnterMoveNextControl = True
        Me.INDRgApplyTaxDevolution.Location = New System.Drawing.Point(24, 399)
        Me.INDRgApplyTaxDevolution.Name = "INDRgApplyTaxDevolution"
        Me.INDRgApplyTaxDevolution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgApplyTaxDevolution.Properties.Appearance.Options.UseFont = True
        Me.INDRgApplyTaxDevolution.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgApplyTaxDevolution.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgApplyTaxDevolution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgApplyTaxDevolution.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgApplyTaxDevolution.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgApplyTaxDevolution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgApplyTaxDevolution.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDRgApplyTaxDevolution.Size = New System.Drawing.Size(386, 34)
        Me.INDRgApplyTaxDevolution.StyleController = Me.INDLcBase
        ToolTipTitleItem1.Text = "Afecta Presupuesto"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "Define si afecta presupuesto."
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.INDRgApplyTaxDevolution.SuperTip = SuperToolTip1
        Me.INDRgApplyTaxDevolution.TabIndex = 2
        '
        'INDGlePaymentMethodTypes
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGlePaymentMethodTypes, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGlePaymentMethodTypes, False)
        Me.INDGlePaymentMethodTypes.Location = New System.Drawing.Point(24, 463)
        Me.IndigoTextEdit1.SetMascara(Me.INDGlePaymentMethodTypes, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGlePaymentMethodTypes.Name = "INDGlePaymentMethodTypes"
        Me.INDGlePaymentMethodTypes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGlePaymentMethodTypes.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGlePaymentMethodTypes.Properties.Appearance.Options.UseFont = True
        Me.INDGlePaymentMethodTypes.Properties.Appearance.Options.UseForeColor = True
        Me.INDGlePaymentMethodTypes.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGlePaymentMethodTypes.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGlePaymentMethodTypes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGlePaymentMethodTypes.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGlePaymentMethodTypes.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGlePaymentMethodTypes.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGlePaymentMethodTypes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGlePaymentMethodTypes.Properties.NullText = ""
        Me.INDGlePaymentMethodTypes.Properties.PopupSizeable = False
        Me.INDGlePaymentMethodTypes.Properties.PopupView = Me.INDGleViewPaymentsMethods
        Me.INDGlePaymentMethodTypes.Properties.ShowFooter = False
        Me.INDGlePaymentMethodTypes.Size = New System.Drawing.Size(386, 28)
        Me.INDGlePaymentMethodTypes.StyleController = Me.INDLcBase
        Me.INDGlePaymentMethodTypes.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGlePaymentMethodTypes, 0)
        '
        'INDGleViewPaymentsMethods
        '
        Me.INDGleViewPaymentsMethods.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGleViewPaymentsMethods.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGleViewPaymentsMethods.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGleViewPaymentsMethods.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGleViewPaymentsMethods.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleViewPaymentsMethods.Appearance.GroupRow.Options.UseFont = True
        Me.INDGleViewPaymentsMethods.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleViewPaymentsMethods.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGleViewPaymentsMethods.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGleViewPaymentsMethods.Appearance.Row.Options.UseFont = True
        Me.INDGleViewPaymentsMethods.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvPaymentsMUnboundSelection, Me.INDColPaymentsMethods})
        Me.INDGleViewPaymentsMethods.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGleViewPaymentsMethods.Name = "INDGleViewPaymentsMethods"
        Me.INDGleViewPaymentsMethods.OptionsFind.FindFilterColumns = "Number"
        Me.INDGleViewPaymentsMethods.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGleViewPaymentsMethods.OptionsSelection.MultiSelect = True
        Me.INDGleViewPaymentsMethods.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGleViewPaymentsMethods.OptionsView.EnableAppearanceOddRow = True
        Me.INDGleViewPaymentsMethods.OptionsView.ShowAutoFilterRow = True
        Me.INDGleViewPaymentsMethods.OptionsView.ShowDetailButtons = False
        Me.INDGleViewPaymentsMethods.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGleViewPaymentsMethods, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGleViewPaymentsMethods, False)
        '
        'INDGvPaymentsMUnboundSelection
        '
        Me.INDGvPaymentsMUnboundSelection.Caption = " "
        Me.INDGvPaymentsMUnboundSelection.FieldName = "INDGvPaymentsMUnboundSelection"
        Me.INDGvPaymentsMUnboundSelection.Name = "INDGvPaymentsMUnboundSelection"
        Me.INDGvPaymentsMUnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvPaymentsMUnboundSelection.Visible = True
        Me.INDGvPaymentsMUnboundSelection.VisibleIndex = 0
        Me.INDGvPaymentsMUnboundSelection.Width = 20
        '
        'INDColPaymentsMethods
        '
        Me.INDColPaymentsMethods.Caption = "Formas de pago"
        Me.INDColPaymentsMethods.FieldName = "Item2"
        Me.INDColPaymentsMethods.Name = "INDColPaymentsMethods"
        Me.INDColPaymentsMethods.Visible = True
        Me.INDColPaymentsMethods.VisibleIndex = 1
        Me.INDColPaymentsMethods.Width = 282
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.CustomizationFormText = "IVA"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LcgGeneralLedgerIVA, Me.LcgGeneralLedgerIVAFiscal})
        Me.INDLcgBase.Name = "Root"
        Me.INDLcgBase.Size = New System.Drawing.Size(848, 568)
        Me.INDLcgBase.TextVisible = False
        '
        'LcgGeneralLedgerIVA
        '
        Me.LcgGeneralLedgerIVA.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgGeneralLedgerIVA.AppearanceGroup.Options.UseFont = True
        Me.LcgGeneralLedgerIVA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgGeneralLedgerIVA.AppearanceItemCaption.Options.UseFont = True
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVA.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgGeneralLedgerIVA, False)
        Me.LcgGeneralLedgerIVA.CustomizationFormText = "Datos Principales"
        Me.LcgGeneralLedgerIVA.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcName, Me.INDLcCode, Me.INDLcPercentage, Me.INDlyItemAccountSale, Me.INDlyItemAccountPurchaseService, Me.INDLciApplyTaxDevolution, Me.INDLciPaymentMethodTypes})
        Me.LcgGeneralLedgerIVA.Location = New System.Drawing.Point(0, 0)
        Me.LcgGeneralLedgerIVA.Name = "LcgGeneralLedgerIVA"
        Me.LcgGeneralLedgerIVA.Size = New System.Drawing.Size(414, 548)
        Me.LcgGeneralLedgerIVA.Text = "Datos Principales"
        '
        'INDLcName
        '
        Me.INDLcName.AllowHide = False
        Me.INDLcName.Control = Me.INDTxtName
        Me.INDLcName.CustomizationFormText = "Nombre"
        Me.INDLcName.Location = New System.Drawing.Point(0, 64)
        Me.INDLcName.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLcName.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLcName.Name = "INDLcName"
        Me.INDLcName.ShowInCustomizationForm = False
        Me.INDLcName.Size = New System.Drawing.Size(390, 64)
        Me.INDLcName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcName.Text = "Nombre"
        Me.INDLcName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLcName.TextToControlDistance = 12
        '
        'INDLcCode
        '
        Me.INDLcCode.AllowHide = False
        Me.INDLcCode.Control = Me.INDBteCode
        Me.INDLcCode.CustomizationFormText = "Código"
        Me.INDLcCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLcCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLcCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLcCode.Name = "INDLcCode"
        Me.INDLcCode.ShowInCustomizationForm = False
        Me.INDLcCode.Size = New System.Drawing.Size(390, 64)
        Me.INDLcCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcCode.Text = "Código"
        Me.INDLcCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLcCode.TextToControlDistance = 12
        '
        'INDLcPercentage
        '
        Me.INDLcPercentage.AllowHide = False
        Me.INDLcPercentage.Control = Me.INDTxtPercentage
        Me.INDLcPercentage.CustomizationFormText = "Porcentaje"
        Me.INDLcPercentage.Location = New System.Drawing.Point(0, 128)
        Me.INDLcPercentage.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLcPercentage.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLcPercentage.Name = "INDLcPercentage"
        Me.INDLcPercentage.ShowInCustomizationForm = False
        Me.INDLcPercentage.Size = New System.Drawing.Size(390, 64)
        Me.INDLcPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcPercentage.Text = "Porcentaje"
        Me.INDLcPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcPercentage.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLcPercentage.TextToControlDistance = 12
        '
        'INDlyItemAccountSale
        '
        Me.INDlyItemAccountSale.Control = Me.INDsleAccountSale
        Me.INDlyItemAccountSale.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemAccountSale.CustomizationFormText = "Cuenta IVA Ventas"
        Me.INDlyItemAccountSale.Location = New System.Drawing.Point(0, 256)
        Me.INDlyItemAccountSale.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountSale.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountSale.Name = "INDlyItemAccountSale"
        Me.INDlyItemAccountSale.ShowInCustomizationForm = False
        Me.INDlyItemAccountSale.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountSale.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccountSale.Text = "Cuenta IVA Ventas"
        Me.INDlyItemAccountSale.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAccountSale.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAccountSale.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAccountSale.TextToControlDistance = 5
        '
        'INDlyItemAccountPurchaseService
        '
        Me.INDlyItemAccountPurchaseService.Control = Me.INDsleAccountPurchaseService
        Me.INDlyItemAccountPurchaseService.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemAccountPurchaseService.CustomizationFormText = "Cuenta IVA Compra/Servicio"
        Me.INDlyItemAccountPurchaseService.Location = New System.Drawing.Point(0, 192)
        Me.INDlyItemAccountPurchaseService.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountPurchaseService.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountPurchaseService.Name = "INDlyItemAccountPurchaseService"
        Me.INDlyItemAccountPurchaseService.ShowInCustomizationForm = False
        Me.INDlyItemAccountPurchaseService.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountPurchaseService.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccountPurchaseService.Text = "Cuenta IVA Compra/Servicio"
        Me.INDlyItemAccountPurchaseService.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAccountPurchaseService.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAccountPurchaseService.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAccountPurchaseService.TextToControlDistance = 5
        '
        'INDLciApplyTaxDevolution
        '
        Me.INDLciApplyTaxDevolution.Control = Me.INDRgApplyTaxDevolution
        Me.INDLciApplyTaxDevolution.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciApplyTaxDevolution.CustomizationFormText = "Aplica Devolución de IVA"
        Me.INDLciApplyTaxDevolution.Location = New System.Drawing.Point(0, 320)
        Me.INDLciApplyTaxDevolution.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciApplyTaxDevolution.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciApplyTaxDevolution.Name = "INDLciApplyTaxDevolution"
        Me.INDLciApplyTaxDevolution.ShowInCustomizationForm = False
        Me.INDLciApplyTaxDevolution.Size = New System.Drawing.Size(390, 64)
        Me.INDLciApplyTaxDevolution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciApplyTaxDevolution.Text = "Aplica Devolución de IVA"
        Me.INDLciApplyTaxDevolution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciApplyTaxDevolution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciApplyTaxDevolution.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciApplyTaxDevolution.TextToControlDistance = 5
        '
        'INDLciPaymentMethodTypes
        '
        Me.INDLciPaymentMethodTypes.Control = Me.INDGlePaymentMethodTypes
        Me.INDLciPaymentMethodTypes.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPaymentMethodTypes.CustomizationFormText = "Formas de pago"
        Me.INDLciPaymentMethodTypes.Location = New System.Drawing.Point(0, 384)
        Me.INDLciPaymentMethodTypes.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLciPaymentMethodTypes.Name = "INDLciPaymentMethodTypes"
        Me.INDLciPaymentMethodTypes.ShowInCustomizationForm = False
        Me.INDLciPaymentMethodTypes.Size = New System.Drawing.Size(390, 111)
        Me.INDLciPaymentMethodTypes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPaymentMethodTypes.Text = "Formas de pago"
        Me.INDLciPaymentMethodTypes.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPaymentMethodTypes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPaymentMethodTypes.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciPaymentMethodTypes.TextToControlDistance = 5
        Me.INDLciPaymentMethodTypes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LcgGeneralLedgerIVAFiscal
        '
        Me.LcgGeneralLedgerIVAFiscal.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgGeneralLedgerIVAFiscal.AppearanceGroup.Options.UseFont = True
        Me.LcgGeneralLedgerIVAFiscal.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgGeneralLedgerIVAFiscal.AppearanceItemCaption.Options.UseFont = True
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgGeneralLedgerIVAFiscal.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgGeneralLedgerIVAFiscal, False)
        Me.LcgGeneralLedgerIVAFiscal.CustomizationFormText = "Iva Control Fiscal"
        Me.LcgGeneralLedgerIVAFiscal.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAccountDebitControlFiscal, Me.INDlyItemAccountCreditControlFiscal})
        Me.LcgGeneralLedgerIVAFiscal.Location = New System.Drawing.Point(414, 0)
        Me.LcgGeneralLedgerIVAFiscal.Name = "LcgGeneralLedgerIVAFiscal"
        Me.LcgGeneralLedgerIVAFiscal.Size = New System.Drawing.Size(414, 548)
        Me.LcgGeneralLedgerIVAFiscal.Text = "Iva Control Fiscal"
        '
        'INDlyItemAccountDebitControlFiscal
        '
        Me.INDlyItemAccountDebitControlFiscal.Control = Me.INDsleAccountDebitControlFiscal
        Me.INDlyItemAccountDebitControlFiscal.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemAccountDebitControlFiscal.CustomizationFormText = "Cuanta Control Fiscal Débito"
        Me.INDlyItemAccountDebitControlFiscal.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAccountDebitControlFiscal.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountDebitControlFiscal.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountDebitControlFiscal.Name = "INDlyItemAccountDebitControlFiscal"
        Me.INDlyItemAccountDebitControlFiscal.ShowInCustomizationForm = False
        Me.INDlyItemAccountDebitControlFiscal.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountDebitControlFiscal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccountDebitControlFiscal.Text = "Cuenta Control Fiscal Débito"
        Me.INDlyItemAccountDebitControlFiscal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAccountDebitControlFiscal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAccountDebitControlFiscal.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAccountDebitControlFiscal.TextToControlDistance = 5
        '
        'INDlyItemAccountCreditControlFiscal
        '
        Me.INDlyItemAccountCreditControlFiscal.Control = Me.INDsleAccountCreditControlFiscal
        Me.INDlyItemAccountCreditControlFiscal.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemAccountCreditControlFiscal.CustomizationFormText = "Cuanta Control Fiscal Crédito"
        Me.INDlyItemAccountCreditControlFiscal.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemAccountCreditControlFiscal.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountCreditControlFiscal.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountCreditControlFiscal.Name = "INDlyItemAccountCreditControlFiscal"
        Me.INDlyItemAccountCreditControlFiscal.ShowInCustomizationForm = False
        Me.INDlyItemAccountCreditControlFiscal.Size = New System.Drawing.Size(390, 431)
        Me.INDlyItemAccountCreditControlFiscal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccountCreditControlFiscal.Text = "Cuenta Control Fiscal Crédito"
        Me.INDlyItemAccountCreditControlFiscal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAccountCreditControlFiscal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAccountCreditControlFiscal.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAccountCreditControlFiscal.TextToControlDistance = 5
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcBase
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 585)
        Me.CtrNavigationControlPanel1.TabIndex = 2
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        Me.IndigoGridView2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'RepositoryItemPopupContainerEdit111
        '
        Me.RepositoryItemPopupContainerEdit111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit111.Name = "RepositoryItemPopupContainerEdit111"
        '
        'RepositoryItemPopupContainerEdit1111
        '
        Me.RepositoryItemPopupContainerEdit1111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1111.Name = "RepositoryItemPopupContainerEdit1111"
        '
        'RepositoryItemPopupContainerEdit22
        '
        Me.RepositoryItemPopupContainerEdit22.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit22.Name = "RepositoryItemPopupContainerEdit22"
        '
        'RepositoryItemPopupContainerEdit112
        '
        Me.RepositoryItemPopupContainerEdit112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit112.Name = "RepositoryItemPopupContainerEdit112"
        '
        'RepositoryItemPopupContainerEdit211
        '
        Me.RepositoryItemPopupContainerEdit211.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit211.Name = "RepositoryItemPopupContainerEdit211"
        '
        'RepositoryItemPopupContainerEdit113
        '
        Me.RepositoryItemPopupContainerEdit113.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit113.Name = "RepositoryItemPopupContainerEdit113"
        '
        'RepositoryItemPopupContainerEdit114
        '
        Me.RepositoryItemPopupContainerEdit114.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit114.Name = "RepositoryItemPopupContainerEdit114"
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'IndigoGridView3
        '
        Me.IndigoGridView3.RaiseMenuPopUp = True
        Me.IndigoGridView3.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit23
        '
        'RepositoryItemPopupContainerEdit23
        '
        Me.RepositoryItemPopupContainerEdit23.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit23.Name = "RepositoryItemPopupContainerEdit23"
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit231
        '
        Me.RepositoryItemPopupContainerEdit231.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit231.Name = "RepositoryItemPopupContainerEdit231"
        '
        'RepositoryItemPopupContainerEdit115
        '
        Me.RepositoryItemPopupContainerEdit115.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit115.Name = "RepositoryItemPopupContainerEdit115"
        '
        'RepositoryItemPopupContainerEdit232
        '
        Me.RepositoryItemPopupContainerEdit232.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit232.Name = "RepositoryItemPopupContainerEdit232"
        '
        'RepositoryItemPopupContainerEdit116
        '
        Me.RepositoryItemPopupContainerEdit116.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit116.Name = "RepositoryItemPopupContainerEdit116"
        '
        'RepositoryItemPopupContainerEdit233
        '
        Me.RepositoryItemPopupContainerEdit233.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit233.Name = "RepositoryItemPopupContainerEdit233"
        '
        'RepositoryItemPopupContainerEdit117
        '
        Me.RepositoryItemPopupContainerEdit117.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit117.Name = "RepositoryItemPopupContainerEdit117"
        '
        'FrmGeneralLedgerIVA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGeneralLedgerIVA"
        Me.Opacity = 1.0R
        Me.Tag = "1509"
        Me.Text = "IVA"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDTxtPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountSale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountCreditControlFiscal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountDebitControlFiscal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountPurchaseService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgApplyTaxDevolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGlePaymentMethodTypes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleViewPaymentsMethods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgGeneralLedgerIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccountSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccountPurchaseService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciApplyTaxDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPaymentMethodTypes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgGeneralLedgerIVAFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccountDebitControlFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccountCreditControlFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit211, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit114, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit231, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit115, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit232, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit116, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit233, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit117, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LcgGeneralLedgerIVA As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDTxtPercentage As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LcgGeneralLedgerIVAFiscal As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit211 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit22 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleAccountSale As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn18911 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19011 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19211 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19311 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemAccountSale As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit113 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit114 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleAccountCreditControlFiscal As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View111 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn189111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleAccountDebitControlFiscal As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View112 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn189112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemAccountDebitControlFiscal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAccountCreditControlFiscal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView3 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit23 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit115 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit231 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit116 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit232 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleAccountPurchaseService As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View113 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn189113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemAccountPurchaseService As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRgApplyTaxDevolution As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDLciApplyTaxDevolution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGlePaymentMethodTypes As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGleViewPaymentsMethods As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColPaymentsMethods As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciPaymentMethodTypes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit233 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit117 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvPaymentsMUnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
End Class
