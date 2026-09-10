Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmUploadBankStatements
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUploadBankStatements))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcMain = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccAddMovements = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAddProductionCenter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtTransactionDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtReference1 = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtReference2 = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtBankCheck = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtDebitValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtCreditValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtBankConsecutive = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTransactionCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDtseTransactionDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleDocument = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTransactionDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTransactionDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBankCheck = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDebitValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCreditValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReference1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReference2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBankConsecutive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTransactionCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBtnImportFileBankStatementsDetailAPI = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpceAddDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcBankStatements = New DevExpress.XtraGrid.GridControl()
        Me.INDGvBankStatements = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepDocument = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDCdnPeriod = New Presentation.Controls.CtrDateNavigator()
        Me.INDEsbBankStatementsDetail = New Presentation.Controls.ExportStructureButton()
        Me.INDBtnImportFileBankStatementsDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleEntityAccount = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvEntityAccountXpo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtInitialBalance = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtEndingBalance = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliEntityAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInitialBalance = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEndingBalance = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgBankStatements = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDtxtMaximumAmount = New DevExpress.XtraEditors.TextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoGridView = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMain.SuspendLayout()
        CType(Me.INDpccAddMovements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccAddMovements.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDtxtTransactionDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtReference1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtReference2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtBankCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDebitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCreditValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtBankConsecutive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTransactionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtseTransactionDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtseTransactionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTransactionDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTransactionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBankCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDebitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCreditValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReference1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReference2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBankConsecutive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTransactionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDpceAddDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcBankStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBankStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityAccount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvEntityAccountXpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtInitialBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtEndingBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntityAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInitialBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEndingBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBankStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtMaximumAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcMain)
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
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcMain
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcMain
        '
        Me.INDLcMain.Controls.Add(Me.INDpccAddMovements)
        Me.INDLcMain.Controls.Add(Me.INDBtnImportFileBankStatementsDetailAPI)
        Me.INDLcMain.Controls.Add(Me.LayoutControl1)
        Me.INDLcMain.Controls.Add(Me.INDGcBankStatements)
        Me.INDLcMain.Controls.Add(Me.INDBteCode)
        Me.INDLcMain.Controls.Add(Me.INDCdnPeriod)
        Me.INDLcMain.Controls.Add(Me.INDEsbBankStatementsDetail)
        Me.INDLcMain.Controls.Add(Me.INDBtnImportFileBankStatementsDetail)
        Me.INDLcMain.Controls.Add(Me.INDsleEntityAccount)
        Me.INDLcMain.Controls.Add(Me.INDTxtInitialBalance)
        Me.INDLcMain.Controls.Add(Me.INDTxtEndingBalance)
        Me.INDLcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcMain.Location = New System.Drawing.Point(202, 7)
        Me.INDLcMain.Name = "INDLcMain"
        Me.INDLcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(290, 368, 648, 460)
        Me.INDLcMain.Root = Me.LayoutControlGroup1
        Me.INDLcMain.Size = New System.Drawing.Size(1261, 557)
        Me.INDLcMain.TabIndex = 1
        Me.INDLcMain.Text = "LayoutControl1"
        '
        'INDpccAddMovements
        '
        Me.INDpccAddMovements.Controls.Add(Me.LayoutControl2)
        Me.INDpccAddMovements.Location = New System.Drawing.Point(436, 75)
        Me.INDpccAddMovements.Name = "INDpccAddMovements"
        Me.INDpccAddMovements.Size = New System.Drawing.Size(446, 450)
        Me.INDpccAddMovements.TabIndex = 30
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsbAddProductionCenter)
        Me.LayoutControl2.Controls.Add(Me.INDtxtTransactionDescription)
        Me.LayoutControl2.Controls.Add(Me.INDtxtReference1)
        Me.LayoutControl2.Controls.Add(Me.INDtxtReference2)
        Me.LayoutControl2.Controls.Add(Me.INDtxtBankCheck)
        Me.LayoutControl2.Controls.Add(Me.INDtxtDebitValue)
        Me.LayoutControl2.Controls.Add(Me.INDtxtCreditValue)
        Me.LayoutControl2.Controls.Add(Me.INDtxtBankConsecutive)
        Me.LayoutControl2.Controls.Add(Me.INDtxtTransactionCode)
        Me.LayoutControl2.Controls.Add(Me.INDtseTransactionDate)
        Me.LayoutControl2.Controls.Add(Me.INDsleDocument)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup3
        Me.LayoutControl2.Size = New System.Drawing.Size(446, 450)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDsbAddProductionCenter
        '
        Me.INDsbAddProductionCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddProductionCenter.Appearance.Options.UseFont = True
        Me.INDsbAddProductionCenter.Location = New System.Drawing.Point(12, 372)
        Me.INDsbAddProductionCenter.Name = "INDsbAddProductionCenter"
        Me.INDsbAddProductionCenter.Size = New System.Drawing.Size(406, 32)
        Me.INDsbAddProductionCenter.StyleController = Me.LayoutControl2
        Me.INDsbAddProductionCenter.TabIndex = 5
        Me.INDsbAddProductionCenter.Text = "Agregar"
        '
        'INDtxtTransactionDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTransactionDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTransactionDescription, False)
        Me.INDtxtTransactionDescription.EditValue = ""
        Me.INDtxtTransactionDescription.EnterMoveNextControl = True
        Me.INDtxtTransactionDescription.Location = New System.Drawing.Point(12, 158)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTransactionDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTransactionDescription.MinimumSize = New System.Drawing.Size(409, 28)
        Me.INDtxtTransactionDescription.Name = "INDtxtTransactionDescription"
        Me.INDtxtTransactionDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTransactionDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTransactionDescription.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTransactionDescription.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTransactionDescription.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTransactionDescription.Properties.MaxLength = 60
        Me.INDtxtTransactionDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtTransactionDescription.Size = New System.Drawing.Size(409, 28)
        Me.INDtxtTransactionDescription.StyleController = Me.LayoutControl2
        Me.INDtxtTransactionDescription.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTransactionDescription, 0)
        '
        'INDtxtReference1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtReference1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtReference1, False)
        Me.INDtxtReference1.EditValue = ""
        Me.INDtxtReference1.EnterMoveNextControl = True
        Me.INDtxtReference1.Location = New System.Drawing.Point(12, 278)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtReference1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtReference1.Name = "INDtxtReference1"
        Me.INDtxtReference1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtReference1.Properties.Appearance.Options.UseFont = True
        Me.INDtxtReference1.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtReference1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtReference1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtReference1.Properties.MaxLength = 15
        Me.INDtxtReference1.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtReference1.StyleController = Me.LayoutControl2
        Me.INDtxtReference1.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtReference1, 0)
        '
        'INDtxtReference2
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtReference2, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtReference2, False)
        Me.INDtxtReference2.EditValue = ""
        Me.INDtxtReference2.EnterMoveNextControl = True
        Me.INDtxtReference2.Location = New System.Drawing.Point(217, 278)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtReference2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtReference2.Name = "INDtxtReference2"
        Me.INDtxtReference2.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtReference2.Properties.Appearance.Options.UseFont = True
        Me.INDtxtReference2.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtReference2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtReference2.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtReference2.Properties.MaxLength = 15
        Me.INDtxtReference2.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtReference2.StyleController = Me.LayoutControl2
        Me.INDtxtReference2.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtReference2, 0)
        '
        'INDtxtBankCheck
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtBankCheck, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtBankCheck, False)
        Me.INDtxtBankCheck.EnterMoveNextControl = True
        Me.INDtxtBankCheck.Location = New System.Drawing.Point(12, 338)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtBankCheck, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtBankCheck.Name = "INDtxtBankCheck"
        Me.INDtxtBankCheck.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtBankCheck.Properties.Appearance.Options.UseFont = True
        Me.INDtxtBankCheck.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtBankCheck.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtBankCheck.Properties.Mask.EditMask = "n0"
        Me.INDtxtBankCheck.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtBankCheck.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtBankCheck.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtBankCheck.StyleController = Me.LayoutControl2
        Me.INDtxtBankCheck.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtBankCheck, 0)
        '
        'INDtxtDebitValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDebitValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDebitValue, False)
        Me.INDtxtDebitValue.EditValue = New Decimal(New Integer() {0, 0, 0, 262144})
        Me.INDtxtDebitValue.EnterMoveNextControl = True
        Me.INDtxtDebitValue.Location = New System.Drawing.Point(12, 217)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDebitValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDebitValue.Name = "INDtxtDebitValue"
        Me.INDtxtDebitValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDebitValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDebitValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtDebitValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtDebitValue.Properties.Mask.EditMask = "c2"
        Me.INDtxtDebitValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtDebitValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtDebitValue.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtDebitValue.StyleController = Me.LayoutControl2
        Me.INDtxtDebitValue.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDebitValue, 0)
        '
        'INDtxtCreditValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCreditValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCreditValue, False)
        Me.INDtxtCreditValue.EditValue = New Decimal(New Integer() {0, 0, 0, 262144})
        Me.INDtxtCreditValue.EnterMoveNextControl = True
        Me.INDtxtCreditValue.Location = New System.Drawing.Point(217, 217)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCreditValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCreditValue.Name = "INDtxtCreditValue"
        Me.INDtxtCreditValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCreditValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCreditValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtCreditValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtCreditValue.Properties.Mask.EditMask = "c2"
        Me.INDtxtCreditValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtCreditValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtCreditValue.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtCreditValue.StyleController = Me.LayoutControl2
        Me.INDtxtCreditValue.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCreditValue, 0)
        '
        'INDtxtBankConsecutive
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtBankConsecutive, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtBankConsecutive, False)
        Me.INDtxtBankConsecutive.EditValue = ""
        Me.INDtxtBankConsecutive.EnterMoveNextControl = True
        Me.INDtxtBankConsecutive.Location = New System.Drawing.Point(217, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtBankConsecutive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtBankConsecutive.Name = "INDtxtBankConsecutive"
        Me.INDtxtBankConsecutive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtBankConsecutive.Properties.Appearance.Options.UseFont = True
        Me.INDtxtBankConsecutive.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtBankConsecutive.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtBankConsecutive.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtBankConsecutive.Properties.MaxLength = 60
        Me.INDtxtBankConsecutive.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtBankConsecutive.StyleController = Me.LayoutControl2
        Me.INDtxtBankConsecutive.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtBankConsecutive, 0)
        '
        'INDtxtTransactionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTransactionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTransactionCode, False)
        Me.INDtxtTransactionCode.EditValue = ""
        Me.INDtxtTransactionCode.EnterMoveNextControl = True
        Me.INDtxtTransactionCode.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTransactionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTransactionCode.Name = "INDtxtTransactionCode"
        Me.INDtxtTransactionCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTransactionCode.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTransactionCode.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTransactionCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTransactionCode.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTransactionCode.Properties.MaxLength = 60
        Me.INDtxtTransactionCode.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtTransactionCode.StyleController = Me.LayoutControl2
        Me.INDtxtTransactionCode.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTransactionCode, 0)
        '
        'INDtseTransactionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtseTransactionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtseTransactionDate, False)
        Me.INDtseTransactionDate.EditValue = Nothing
        Me.INDtseTransactionDate.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDtseTransactionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtseTransactionDate.MaximumSize = New System.Drawing.Size(201, 28)
        Me.INDtseTransactionDate.MinimumSize = New System.Drawing.Size(201, 28)
        Me.INDtseTransactionDate.Name = "INDtseTransactionDate"
        Me.INDtseTransactionDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtseTransactionDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtseTransactionDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtseTransactionDate.Properties.Appearance.Options.UseFont = True
        Me.INDtseTransactionDate.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtseTransactionDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtseTransactionDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtseTransactionDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtseTransactionDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDtseTransactionDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDtseTransactionDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtseTransactionDate.Size = New System.Drawing.Size(201, 28)
        Me.INDtseTransactionDate.StyleController = Me.LayoutControl2
        Me.INDtseTransactionDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtseTransactionDate, 0)
        '
        'INDsleDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleDocument, False)
        Me.INDsleDocument.Location = New System.Drawing.Point(217, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleDocument.MaximumSize = New System.Drawing.Size(201, 28)
        Me.INDsleDocument.MinimumSize = New System.Drawing.Size(201, 28)
        Me.INDsleDocument.Name = "INDsleDocument"
        Me.INDsleDocument.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleDocument.Properties.Appearance.Options.UseFont = True
        Me.INDsleDocument.Properties.Appearance.Options.UseTextOptions = True
        Me.INDsleDocument.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDsleDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleDocument.Properties.DisplayFormat.FormatString = "d"
        Me.INDsleDocument.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDsleDocument.Properties.DisplayMember = "Name"
        Me.INDsleDocument.Properties.EditFormat.FormatString = "d"
        Me.INDsleDocument.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDsleDocument.Properties.NullText = ""
        Me.INDsleDocument.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDsleDocument.Properties.ValueMember = "Id"
        Me.INDsleDocument.Size = New System.Drawing.Size(201, 28)
        Me.INDsleDocument.StyleController = Me.LayoutControl2
        Me.INDsleDocument.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleDocument, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Documento"
        Me.GridColumn11.FieldName = "Name"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
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
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.INDLciTransactionDescription, Me.INDLciTransactionDate, Me.INDLciBankCheck, Me.INDLciDebitValue, Me.INDLciCreditValue, Me.INDLciReference1, Me.INDLciReference2, Me.INDLciBankConsecutive, Me.INDLciTransactionCode, Me.INDLciDocument})
        Me.LayoutControlGroup3.Name = "Root"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(446, 450)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDsbAddProductionCenter
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 360)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem5.Name = "LayoutControlItem2"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(426, 70)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDLciTransactionDescription
        '
        Me.INDLciTransactionDescription.Control = Me.INDtxtTransactionDescription
        Me.INDLciTransactionDescription.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTransactionDescription.CustomizationFormText = "Descrpción de la transacción"
        Me.INDLciTransactionDescription.Location = New System.Drawing.Point(0, 120)
        Me.INDLciTransactionDescription.MaxSize = New System.Drawing.Size(410, 60)
        Me.INDLciTransactionDescription.MinSize = New System.Drawing.Size(410, 60)
        Me.INDLciTransactionDescription.Name = "INDLciTransactionDescription"
        Me.INDLciTransactionDescription.Size = New System.Drawing.Size(426, 60)
        Me.INDLciTransactionDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTransactionDescription.Text = "Descripción de la transacción"
        Me.INDLciTransactionDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTransactionDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTransactionDescription.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciTransactionDescription.TextToControlDistance = 5
        '
        'INDLciTransactionDate
        '
        Me.INDLciTransactionDate.Control = Me.INDtseTransactionDate
        Me.INDLciTransactionDate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTransactionDate.CustomizationFormText = "Consecutivo del banco"
        Me.INDLciTransactionDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTransactionDate.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciTransactionDate.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciTransactionDate.Name = "INDLciTransactionDate"
        Me.INDLciTransactionDate.Size = New System.Drawing.Size(205, 60)
        Me.INDLciTransactionDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTransactionDate.Text = "Fecha"
        Me.INDLciTransactionDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTransactionDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTransactionDate.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciTransactionDate.TextToControlDistance = 5
        '
        'INDLciBankCheck
        '
        Me.INDLciBankCheck.Control = Me.INDtxtBankCheck
        Me.INDLciBankCheck.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciBankCheck.CustomizationFormText = "Cheque"
        Me.INDLciBankCheck.Location = New System.Drawing.Point(0, 300)
        Me.INDLciBankCheck.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciBankCheck.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciBankCheck.Name = "INDLciBankCheck"
        Me.INDLciBankCheck.Size = New System.Drawing.Size(426, 60)
        Me.INDLciBankCheck.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBankCheck.Text = "Cheque"
        Me.INDLciBankCheck.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBankCheck.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBankCheck.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciBankCheck.TextToControlDistance = 5
        '
        'INDLciDebitValue
        '
        Me.INDLciDebitValue.Control = Me.INDtxtDebitValue
        Me.INDLciDebitValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDebitValue.CustomizationFormText = "Valor débito"
        Me.INDLciDebitValue.Location = New System.Drawing.Point(0, 180)
        Me.INDLciDebitValue.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciDebitValue.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciDebitValue.Name = "INDLciDebitValue"
        Me.INDLciDebitValue.Size = New System.Drawing.Size(205, 60)
        Me.INDLciDebitValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDebitValue.Text = "Valor Débito"
        Me.INDLciDebitValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDebitValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDebitValue.TextSize = New System.Drawing.Size(155, 20)
        Me.INDLciDebitValue.TextToControlDistance = 5
        '
        'INDLciCreditValue
        '
        Me.INDLciCreditValue.Control = Me.INDtxtCreditValue
        Me.INDLciCreditValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCreditValue.CustomizationFormText = "Valor crédito"
        Me.INDLciCreditValue.Location = New System.Drawing.Point(205, 180)
        Me.INDLciCreditValue.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciCreditValue.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciCreditValue.Name = "INDLciCreditValue"
        Me.INDLciCreditValue.Size = New System.Drawing.Size(221, 60)
        Me.INDLciCreditValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCreditValue.Text = "Valor Crédito"
        Me.INDLciCreditValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCreditValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCreditValue.TextSize = New System.Drawing.Size(155, 20)
        Me.INDLciCreditValue.TextToControlDistance = 5
        '
        'INDLciReference1
        '
        Me.INDLciReference1.Control = Me.INDtxtReference1
        Me.INDLciReference1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciReference1.CustomizationFormText = "Reference1"
        Me.INDLciReference1.Location = New System.Drawing.Point(0, 240)
        Me.INDLciReference1.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciReference1.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciReference1.Name = "INDLciReference1"
        Me.INDLciReference1.Size = New System.Drawing.Size(205, 60)
        Me.INDLciReference1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReference1.Text = "Referencia 1"
        Me.INDLciReference1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReference1.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReference1.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciReference1.TextToControlDistance = 5
        '
        'INDLciReference2
        '
        Me.INDLciReference2.Control = Me.INDtxtReference2
        Me.INDLciReference2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciReference2.CustomizationFormText = "Referencia 2"
        Me.INDLciReference2.Location = New System.Drawing.Point(205, 240)
        Me.INDLciReference2.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciReference2.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciReference2.Name = "INDLciReference2"
        Me.INDLciReference2.Size = New System.Drawing.Size(221, 60)
        Me.INDLciReference2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReference2.Text = "Referencia 2"
        Me.INDLciReference2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReference2.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReference2.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciReference2.TextToControlDistance = 5
        '
        'INDLciBankConsecutive
        '
        Me.INDLciBankConsecutive.Control = Me.INDtxtBankConsecutive
        Me.INDLciBankConsecutive.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciBankConsecutive.CustomizationFormText = "Consecutivo del banco"
        Me.INDLciBankConsecutive.Location = New System.Drawing.Point(205, 60)
        Me.INDLciBankConsecutive.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciBankConsecutive.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciBankConsecutive.Name = "INDLciBankConsecutive"
        Me.INDLciBankConsecutive.Size = New System.Drawing.Size(221, 60)
        Me.INDLciBankConsecutive.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBankConsecutive.Text = "Consecutivo del banco"
        Me.INDLciBankConsecutive.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBankConsecutive.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBankConsecutive.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciBankConsecutive.TextToControlDistance = 5
        '
        'INDLciTransactionCode
        '
        Me.INDLciTransactionCode.Control = Me.INDtxtTransactionCode
        Me.INDLciTransactionCode.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTransactionCode.CustomizationFormText = "Código de la transacción"
        Me.INDLciTransactionCode.Location = New System.Drawing.Point(0, 60)
        Me.INDLciTransactionCode.MaxSize = New System.Drawing.Size(205, 60)
        Me.INDLciTransactionCode.MinSize = New System.Drawing.Size(205, 60)
        Me.INDLciTransactionCode.Name = "INDLciTransactionCode"
        Me.INDLciTransactionCode.Size = New System.Drawing.Size(205, 60)
        Me.INDLciTransactionCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTransactionCode.Text = "Código de la transacción"
        Me.INDLciTransactionCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTransactionCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTransactionCode.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciTransactionCode.TextToControlDistance = 5
        '
        'INDLciDocument
        '
        Me.INDLciDocument.Control = Me.INDsleDocument
        Me.INDLciDocument.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDocument.CustomizationFormText = "Documento"
        Me.INDLciDocument.Location = New System.Drawing.Point(205, 0)
        Me.INDLciDocument.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLciDocument.Name = "INDLciDocument"
        Me.INDLciDocument.Size = New System.Drawing.Size(221, 60)
        Me.INDLciDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocument.Text = "Documento"
        Me.INDLciDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocument.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciDocument.TextToControlDistance = 5
        '
        'INDBtnImportFileBankStatementsDetailAPI
        '
        Me.INDBtnImportFileBankStatementsDetailAPI.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFileBankStatementsDetailAPI.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFileBankStatementsDetailAPI.Location = New System.Drawing.Point(1127, 53)
        Me.INDBtnImportFileBankStatementsDetailAPI.MaximumSize = New System.Drawing.Size(38, 36)
        Me.INDBtnImportFileBankStatementsDetailAPI.MinimumSize = New System.Drawing.Size(34, 32)
        Me.INDBtnImportFileBankStatementsDetailAPI.Name = "INDBtnImportFileBankStatementsDetailAPI"
        Me.INDBtnImportFileBankStatementsDetailAPI.Size = New System.Drawing.Size(34, 32)
        Me.INDBtnImportFileBankStatementsDetailAPI.StyleController = Me.INDLcMain
        Me.INDBtnImportFileBankStatementsDetailAPI.TabIndex = 29
        Me.INDBtnImportFileBankStatementsDetailAPI.ToolTip = "Importar pdf,xlsx,txt"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDpceAddDetail)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(438, 53)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(685, 32)
        Me.LayoutControl1.TabIndex = 28
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDpceAddDetail
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceAddDetail, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceAddDetail, Nothing)
        Me.INDpceAddDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDpceAddDetail.MinimumSize = New System.Drawing.Size(685, 32)
        Me.INDpceAddDetail.Name = "INDpceAddDetail"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceAddDetail, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceAddDetail, False)
        Me.INDpceAddDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceAddDetail.Properties.Appearance.Options.UseFont = True
        Me.INDpceAddDetail.Properties.AutoHeight = False
        Me.INDpceAddDetail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDpceAddDetail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceAddDetail.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceAddDetail.Properties.PopupControl = Me.INDpccAddMovements
        Me.INDpceAddDetail.Properties.PopupSizeable = False
        Me.INDpceAddDetail.Properties.ShowPopupCloseButton = False
        Me.INDpceAddDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAddDetail.Size = New System.Drawing.Size(685, 32)
        Me.INDpceAddDetail.StyleController = Me.LayoutControl1
        Me.INDpceAddDetail.TabIndex = 7
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceAddDetail, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceAddDetail, Nothing)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10})
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.Root.Size = New System.Drawing.Size(685, 32)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDpceAddDetail
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem10.Size = New System.Drawing.Size(685, 32)
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'INDGcBankStatements
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcBankStatements, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcBankStatements, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcBankStatements, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcBankStatements, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcBankStatements, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcBankStatements, False)
        Me.INDGcBankStatements.Location = New System.Drawing.Point(438, 89)
        Me.INDGcBankStatements.MainView = Me.INDGvBankStatements
        Me.INDGcBankStatements.Name = "INDGcBankStatements"
        Me.INDGcBankStatements.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectOption, Me.INDRepDocument})
        Me.INDGcBankStatements.Size = New System.Drawing.Size(799, 444)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcBankStatements, DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment)
        Me.INDGcBankStatements.TabIndex = 7
        Me.INDGcBankStatements.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvBankStatements})
        '
        'INDGvBankStatements
        '
        Me.INDGvBankStatements.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBankStatements.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBankStatements.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBankStatements.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBankStatements.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBankStatements.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBankStatements.Appearance.Row.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvBankStatements.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvBankStatements.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSelect, Me.GridColumn1, Me.GridColumn3, Me.GridColumn2, Me.GridColumn4, Me.GridColumn7, Me.GridColumn5, Me.GridColumn6, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10})
        Me.INDGvBankStatements.GridControl = Me.INDGcBankStatements
        Me.INDGvBankStatements.Name = "INDGvBankStatements"
        Me.INDGvBankStatements.OptionsSelection.MultiSelect = True
        Me.INDGvBankStatements.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBankStatements.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBankStatements.OptionsView.ShowAutoFilterRow = True
        Me.INDGvBankStatements.OptionsView.ShowDetailButtons = False
        Me.INDGvBankStatements.OptionsView.ShowFooter = True
        Me.INDGvBankStatements.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView.SetTemaIndigoMetro(Me.INDGvBankStatements, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvBankStatements, False)
        '
        'INDColSelect
        '
        Me.INDColSelect.Caption = "Sel."
        Me.INDColSelect.ColumnEdit = Me.INDrepCheckSelectOption
        Me.INDColSelect.FieldName = "SelectOption"
        Me.INDColSelect.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDColSelect.ImageOptions.Image = Global.Presentation.Treasury.My.Resources.Resources.undcheck
        Me.INDColSelect.Name = "INDColSelect"
        Me.INDColSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelect.OptionsColumn.AllowMove = False
        Me.INDColSelect.OptionsColumn.AllowShowHide = False
        Me.INDColSelect.OptionsColumn.AllowSize = False
        Me.INDColSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelect.OptionsColumn.FixedWidth = True
        Me.INDColSelect.OptionsFilter.AllowAutoFilter = False
        Me.INDColSelect.OptionsFilter.AllowFilter = False
        Me.INDColSelect.Visible = True
        Me.INDColSelect.VisibleIndex = 0
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = False
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Fecha Transacción"
        Me.GridColumn1.DisplayFormat.FormatString = "d"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "TransactionDate"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Consecutivo Banco"
        Me.GridColumn3.FieldName = "ConsecutiveBank"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código Transacción"
        Me.GridColumn2.FieldName = "TransactionCode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción Transacción"
        Me.GridColumn4.FieldName = "DescriptionTransaction"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo documento"
        Me.GridColumn7.ColumnEdit = Me.INDRepDocument
        Me.GridColumn7.FieldName = "DocumentType"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        '
        'INDRepDocument
        '
        Me.INDRepDocument.AutoHeight = False
        Me.INDRepDocument.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepDocument.DisplayMember = "Name"
        Me.INDRepDocument.Name = "INDRepDocument"
        Me.INDRepDocument.NullText = ""
        Me.INDRepDocument.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.INDRepDocument.ValueMember = "Id"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView.SetTemaIndigoMetro(Me.RepositoryItemGridLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemGridLookUpEdit1View, False)
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Documento"
        Me.GridColumn13.FieldName = "Name"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Valor Debito"
        Me.GridColumn5.DisplayFormat.FormatString = "C0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn5.FieldName = "ValueDebit"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueDebit", "{0:C2}")})
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 6
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Valor Credito"
        Me.GridColumn6.DisplayFormat.FormatString = "C0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn6.FieldName = "ValueCredit"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueCredit", "{0:C2}")})
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 7
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Cheque"
        Me.GridColumn8.FieldName = "BankCheck"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 8
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Ref Pago 1"
        Me.GridColumn9.FieldName = "PaymentReferenceOne"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 9
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Ref Pago 2"
        Me.GridColumn10.FieldName = "PaymentReferenceTwo"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 10
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.INDBteCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions2.Image = CType(resources.GetObject("EditorButtonImageOptions2.Image"), System.Drawing.Image)
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBteCode.StyleController = Me.INDLcMain
        Me.INDBteCode.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDCdnPeriod
        '
        Me.INDCdnPeriod.CtrCalendar = Nothing
        Me.INDCdnPeriod.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.INDCdnPeriod.Location = New System.Drawing.Point(24, 198)
        Me.INDCdnPeriod.Name = "INDCdnPeriod"
        Me.INDCdnPeriod.Size = New System.Drawing.Size(386, 66)
        Me.INDCdnPeriod.TabIndex = 0
        Me.INDCdnPeriod.WithEvent = True
        '
        'INDEsbBankStatementsDetail
        '
        Me.INDEsbBankStatementsDetail.ImageOptions.Image = CType(resources.GetObject("INDEsbBankStatementsDetail.ImageOptions.Image"), System.Drawing.Image)
        Me.INDEsbBankStatementsDetail.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbBankStatementsDetail.Location = New System.Drawing.Point(1165, 53)
        Me.INDEsbBankStatementsDetail.MaximumSize = New System.Drawing.Size(38, 36)
        Me.INDEsbBankStatementsDetail.MinimumSize = New System.Drawing.Size(34, 32)
        Me.INDEsbBankStatementsDetail.Name = "INDEsbBankStatementsDetail"
        Me.INDEsbBankStatementsDetail.Size = New System.Drawing.Size(34, 32)
        Me.INDEsbBankStatementsDetail.StyleController = Me.INDLcMain
        Me.INDEsbBankStatementsDetail.TabIndex = 26
        Me.INDEsbBankStatementsDetail.Text = "ExportStructureButton3"
        Me.INDEsbBankStatementsDetail.ToolTip = "Exportar Estructura"
        '
        'INDBtnImportFileBankStatementsDetail
        '
        Me.INDBtnImportFileBankStatementsDetail.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFileBankStatementsDetail.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFileBankStatementsDetail.Location = New System.Drawing.Point(1203, 53)
        Me.INDBtnImportFileBankStatementsDetail.Name = "INDBtnImportFileBankStatementsDetail"
        Me.INDBtnImportFileBankStatementsDetail.Size = New System.Drawing.Size(34, 32)
        Me.INDBtnImportFileBankStatementsDetail.StyleController = Me.INDLcMain
        Me.INDBtnImportFileBankStatementsDetail.TabIndex = 27
        Me.INDBtnImportFileBankStatementsDetail.ToolTip = "Importar Archivo"
        '
        'INDsleEntityAccount
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityAccount, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityAccount, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleEntityAccount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityAccount, False)
        Me.INDsleEntityAccount.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityAccount, False)
        Me.INDsleEntityAccount.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityAccount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityAccount.Name = "INDsleEntityAccount"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityAccount, False)
        Me.INDsleEntityAccount.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleEntityAccount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityAccount.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleEntityAccount.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityAccount.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityAccount.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleEntityAccount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntityAccount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntityAccount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleEntityAccount.Properties.DisplayMember = "CodeBankAccount"
        Me.INDsleEntityAccount.Properties.NullText = ""
        Me.INDsleEntityAccount.Properties.PopupSizeable = False
        Me.INDsleEntityAccount.Properties.PopupView = Me.INDgvEntityAccountXpo
        Me.INDsleEntityAccount.Properties.ShowFooter = False
        Me.INDsleEntityAccount.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityAccount, True)
        Me.INDsleEntityAccount.Size = New System.Drawing.Size(386, 28)
        Me.INDsleEntityAccount.StyleController = Me.INDLcMain
        Me.INDsleEntityAccount.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityAccount, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityAccount, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityAccount, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityAccount, False)
        '
        'INDgvEntityAccountXpo
        '
        Me.INDgvEntityAccountXpo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvEntityAccountXpo.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvEntityAccountXpo.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvEntityAccountXpo.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvEntityAccountXpo.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvEntityAccountXpo.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvEntityAccountXpo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvEntityAccountXpo.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvEntityAccountXpo.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvEntityAccountXpo.Appearance.Row.Options.UseFont = True
        Me.INDgvEntityAccountXpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12, Me.GridColumn21, Me.GridColumn31, Me.GridColumn41})
        Me.INDgvEntityAccountXpo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvEntityAccountXpo.Name = "INDgvEntityAccountXpo"
        Me.INDgvEntityAccountXpo.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvEntityAccountXpo.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvEntityAccountXpo.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvEntityAccountXpo.OptionsView.ShowAutoFilterRow = True
        Me.INDgvEntityAccountXpo.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView.SetTemaIndigoMetro(Me.INDgvEntityAccountXpo, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvEntityAccountXpo, False)
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Código"
        Me.GridColumn12.FieldName = "Code"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        Me.GridColumn12.Width = 311
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Nombre"
        Me.GridColumn21.FieldName = "IdBank.Name"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 1
        Me.GridColumn21.Width = 476
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "# Cuenta"
        Me.GridColumn31.FieldName = "Number"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 2
        Me.GridColumn31.Width = 300
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Cuenta Contable"
        Me.GridColumn41.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 3
        Me.GridColumn41.Width = 305
        '
        'INDTxtInitialBalance
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtInitialBalance, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtInitialBalance, False)
        Me.INDTxtInitialBalance.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtInitialBalance.EnterMoveNextControl = True
        Me.INDTxtInitialBalance.Location = New System.Drawing.Point(24, 294)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtInitialBalance, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtInitialBalance.Name = "INDTxtInitialBalance"
        Me.INDTxtInitialBalance.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtInitialBalance.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInitialBalance.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTxtInitialBalance.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtInitialBalance.Properties.Appearance.Options.UseFont = True
        Me.INDTxtInitialBalance.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtInitialBalance.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtInitialBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtInitialBalance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtInitialBalance.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDTxtInitialBalance.Properties.Mask.EditMask = "c"
        Me.INDTxtInitialBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtInitialBalance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtInitialBalance.Properties.MaxLength = 19
        Me.INDTxtInitialBalance.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtInitialBalance.StyleController = Me.INDLcMain
        Me.INDTxtInitialBalance.TabIndex = 49
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtInitialBalance, 0)
        '
        'INDTxtEndingBalance
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtEndingBalance, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtEndingBalance, False)
        Me.INDTxtEndingBalance.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtEndingBalance.EnterMoveNextControl = True
        Me.INDTxtEndingBalance.Location = New System.Drawing.Point(24, 354)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtEndingBalance, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtEndingBalance.Name = "INDTxtEndingBalance"
        Me.INDTxtEndingBalance.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtEndingBalance.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtEndingBalance.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTxtEndingBalance.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtEndingBalance.Properties.Appearance.Options.UseFont = True
        Me.INDTxtEndingBalance.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtEndingBalance.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtEndingBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtEndingBalance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtEndingBalance.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDTxtEndingBalance.Properties.Mask.EditMask = "c"
        Me.INDTxtEndingBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtEndingBalance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtEndingBalance.Properties.MaxLength = 19
        Me.INDTxtEndingBalance.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtEndingBalance.StyleController = Me.INDLcMain
        Me.INDTxtEndingBalance.TabIndex = 49
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtEndingBalance, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.INDLcgBankStatements})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 557)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLciPeriod, Me.INDliEntityAccount, Me.INDLciInitialBalance, Me.INDLciEndingBalance})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 537)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDBteCode
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.ShowInCustomizationForm = False
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Consecutivo"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDLciPeriod
        '
        Me.INDLciPeriod.Control = Me.INDCdnPeriod
        Me.INDLciPeriod.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPeriod.CustomizationFormText = "Fecha"
        Me.INDLciPeriod.Location = New System.Drawing.Point(0, 120)
        Me.INDLciPeriod.MaxSize = New System.Drawing.Size(390, 95)
        Me.INDLciPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPeriod.Name = "INDLciPeriod"
        Me.INDLciPeriod.Size = New System.Drawing.Size(390, 95)
        Me.INDLciPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPeriod.Text = "Fecha"
        Me.INDLciPeriod.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPeriod.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciPeriod.TextToControlDistance = 5
        '
        'INDliEntityAccount
        '
        Me.INDliEntityAccount.Control = Me.INDsleEntityAccount
        Me.INDliEntityAccount.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliEntityAccount.CustomizationFormText = "Cuenta Bancaria"
        Me.INDliEntityAccount.Location = New System.Drawing.Point(0, 60)
        Me.INDliEntityAccount.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliEntityAccount.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliEntityAccount.Name = "INDliEntityAccount"
        Me.INDliEntityAccount.ShowInCustomizationForm = False
        Me.INDliEntityAccount.Size = New System.Drawing.Size(390, 60)
        Me.INDliEntityAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntityAccount.Text = "Cuenta Bancaria"
        Me.INDliEntityAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntityAccount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliEntityAccount.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliEntityAccount.TextToControlDistance = 5
        '
        'INDLciInitialBalance
        '
        Me.INDLciInitialBalance.Control = Me.INDTxtInitialBalance
        Me.INDLciInitialBalance.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciInitialBalance.CustomizationFormText = "Saldo inicial"
        Me.INDLciInitialBalance.Enabled = False
        Me.INDLciInitialBalance.Location = New System.Drawing.Point(0, 215)
        Me.INDLciInitialBalance.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciInitialBalance.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciInitialBalance.Name = "INDLciInitialBalance"
        Me.INDLciInitialBalance.Size = New System.Drawing.Size(390, 60)
        Me.INDLciInitialBalance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInitialBalance.Text = "Saldo Inicial"
        Me.INDLciInitialBalance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInitialBalance.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciInitialBalance.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciInitialBalance.TextToControlDistance = 5
        '
        'INDLciEndingBalance
        '
        Me.INDLciEndingBalance.Control = Me.INDTxtEndingBalance
        Me.INDLciEndingBalance.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciEndingBalance.CustomizationFormText = "Saldo final"
        Me.INDLciEndingBalance.Enabled = False
        Me.INDLciEndingBalance.Location = New System.Drawing.Point(0, 275)
        Me.INDLciEndingBalance.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEndingBalance.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEndingBalance.Name = "INDLciEndingBalance"
        Me.INDLciEndingBalance.Size = New System.Drawing.Size(390, 209)
        Me.INDLciEndingBalance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEndingBalance.Text = "Saldo Final"
        Me.INDLciEndingBalance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEndingBalance.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEndingBalance.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciEndingBalance.TextToControlDistance = 5
        '
        'INDLcgBankStatements
        '
        Me.INDLcgBankStatements.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBankStatements.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBankStatements.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBankStatements, False)
        Me.INDLcgBankStatements.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem2, Me.LayoutControlItem4})
        Me.INDLcgBankStatements.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgBankStatements.Name = "INDLcgBankStatements"
        Me.INDLcgBankStatements.Size = New System.Drawing.Size(827, 537)
        Me.INDLcgBankStatements.Text = "Extractos Bancarios"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcBankStatements
        Me.LayoutControlItem3.CustomizationFormText = "Listado de Extractos Bancarios"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(803, 448)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDEsbBankStatementsDetail
        Me.LayoutControlItem6.CustomizationFormText = "Exportar Estructura"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(727, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem7.Control = Me.INDBtnImportFileBankStatementsDetail
        Me.LayoutControlItem7.CustomizationFormText = "Importar Informacion"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(765, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.LayoutControl1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(689, 36)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDBtnImportFileBankStatementsDetailAPI
        Me.LayoutControlItem4.Location = New System.Drawing.Point(689, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'INDtxtMaximumAmount
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtMaximumAmount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtMaximumAmount, False)
        Me.INDtxtMaximumAmount.EditValue = ""
        Me.INDtxtMaximumAmount.Location = New System.Drawing.Point(12, 158)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtMaximumAmount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtMaximumAmount.Name = "INDtxtMaximumAmount"
        Me.INDtxtMaximumAmount.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtMaximumAmount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMaximumAmount.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtMaximumAmount.Properties.Appearance.Options.UseFont = True
        Me.INDtxtMaximumAmount.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtMaximumAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtMaximumAmount.Properties.Mask.EditMask = "[0-9]+"
        Me.INDtxtMaximumAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtMaximumAmount.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtMaximumAmount.Size = New System.Drawing.Size(409, 28)
        Me.INDtxtMaximumAmount.StyleController = Me.LayoutControl2
        Me.INDtxtMaximumAmount.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtMaximumAmount, 0)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridView
        '
        Me.IndigoGridView.RaiseMenuPopUp = True
        Me.IndigoGridView.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmUploadBankStatements
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmUploadBankStatements"
        Me.Opacity = 1.0R
        Me.Tag = "2235"
        Me.Text = "Cargue de Extractos Bancarios"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMain.ResumeLayout(False)
        CType(Me.INDpccAddMovements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccAddMovements.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDtxtTransactionDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtReference1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtReference2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtBankCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDebitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCreditValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtBankConsecutive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTransactionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtseTransactionDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtseTransactionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTransactionDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTransactionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBankCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDebitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCreditValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReference1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReference2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBankConsecutive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTransactionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDpceAddDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcBankStatements, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBankStatements, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityAccount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvEntityAccountXpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtInitialBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtEndingBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntityAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInitialBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEndingBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBankStatements, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtMaximumAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDGcBankStatements As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvBankStatements As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgBankStatements As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDEsbBankStatementsDetail As Presentation.Controls.ExportStructureButton
    Friend WithEvents INDBtnImportFileBankStatementsDetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDCdnPeriod As CtrDateNavigator
    Friend WithEvents INDLciPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView As IndigoGridView
    Friend WithEvents INDsleEntityAccount As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvEntityAccountXpo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDliEntityAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFileBankStatementsDetailAPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDpccAddMovements As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsbAddProductionCenter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpceAddDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtTransactionDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciTransactionDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtMaximumAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciTransactionDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtReference1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciReference1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtReference2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciReference2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtBankCheck As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciBankCheck As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDebitValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciDebitValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtCreditValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciCreditValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtBankConsecutive As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciBankConsecutive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtTransactionCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciTransactionCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtseTransactionDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDColSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDTxtInitialBalance As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciInitialBalance As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtEndingBalance As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciEndingBalance As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleDocument As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepDocument As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
End Class
