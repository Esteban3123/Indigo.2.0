Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetRemissionEntranceItem
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetRemissionEntranceItem))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSpDiscountPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpeIvaPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpeOutstandingQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSlIVA = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlViewIVA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAddDetailEquipment = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcEquipmentDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvEquipmentDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolPlaca = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSerie = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResponsible = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlePolize = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit5View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodePolize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNamePolize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtModel = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleTrademark = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleEquipment = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.ViewItemSearch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColEquipmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEquipmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLastCostEquipment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtEquipmentValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtTaxesValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtDiscountValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtSubTotal = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciEquipment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTrademark = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciModel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IVA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPoliza = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciOutstandingQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciEquipmentValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTaxes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDSpDiscountPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpeIvaPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpeOutstandingQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlViewIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcEquipmentDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvEquipmentDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlePolize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit5View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTrademark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleEquipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewItemSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtEquipmentValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtTaxesValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEquipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTrademark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciOutstandingQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEquipmentValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTaxes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1362, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1362, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1362, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControlPanel1.Appearance.Image = CType(resources.GetObject("CtrNavigationControlPanel1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControlPanel1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControlPanel1.Appearance.Options.UseImage = True
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSpDiscountPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDSpeIvaPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDSpeOutstandingQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDSlIVA)
        Me.LayoutControl1.Controls.Add(Me.INDBtnAddDetailEquipment)
        Me.LayoutControl1.Controls.Add(Me.INDGcEquipmentDetail)
        Me.LayoutControl1.Controls.Add(Me.INDSlePolize)
        Me.LayoutControl1.Controls.Add(Me.INDTxtTotalValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtModel)
        Me.LayoutControl1.Controls.Add(Me.INDSleTrademark)
        Me.LayoutControl1.Controls.Add(Me.INDSleEquipment)
        Me.LayoutControl1.Controls.Add(Me.INDseQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDTxtEquipmentValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtTaxesValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtDiscountValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtSubTotal)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1158, 517)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSpDiscountPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpDiscountPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpDiscountPercentage, False)
        Me.INDSpDiscountPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpDiscountPercentage.EnterMoveNextControl = True
        Me.INDSpDiscountPercentage.Location = New System.Drawing.Point(438, 313)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpDiscountPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpDiscountPercentage.Name = "INDSpDiscountPercentage"
        Me.INDSpDiscountPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpDiscountPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpDiscountPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpDiscountPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpDiscountPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpDiscountPercentage.Properties.MaxLength = 6
        Me.INDSpDiscountPercentage.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDSpDiscountPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpDiscountPercentage.StyleController = Me.LayoutControl1
        Me.INDSpDiscountPercentage.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpDiscountPercentage, 0)
        '
        'INDSpeIvaPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpeIvaPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpeIvaPercentage, False)
        Me.INDSpeIvaPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpeIvaPercentage.EnterMoveNextControl = True
        Me.INDSpeIvaPercentage.Location = New System.Drawing.Point(438, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpeIvaPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpeIvaPercentage.Name = "INDSpeIvaPercentage"
        Me.INDSpeIvaPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpeIvaPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpeIvaPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpeIvaPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpeIvaPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpeIvaPercentage.Properties.MaxLength = 6
        Me.INDSpeIvaPercentage.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDSpeIvaPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpeIvaPercentage.StyleController = Me.LayoutControl1
        Me.INDSpeIvaPercentage.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpeIvaPercentage, 0)
        '
        'INDSpeOutstandingQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpeOutstandingQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpeOutstandingQuantity, False)
        Me.INDSpeOutstandingQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpeOutstandingQuantity.EnterMoveNextControl = True
        Me.INDSpeOutstandingQuantity.Location = New System.Drawing.Point(24, 433)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpeOutstandingQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpeOutstandingQuantity.Name = "INDSpeOutstandingQuantity"
        Me.INDSpeOutstandingQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpeOutstandingQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSpeOutstandingQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpeOutstandingQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpeOutstandingQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpeOutstandingQuantity.Properties.MaxLength = 9
        Me.INDSpeOutstandingQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDSpeOutstandingQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSpeOutstandingQuantity.StyleController = Me.LayoutControl1
        Me.INDSpeOutstandingQuantity.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpeOutstandingQuantity, 0)
        '
        'INDSlIVA
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlIVA, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlIVA, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSlIVA, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlIVA, False)
        Me.INDSlIVA.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlIVA, False)
        Me.INDSlIVA.Location = New System.Drawing.Point(24, 313)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlIVA, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlIVA.Name = "INDSlIVA"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlIVA, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlIVA, False)
        Me.INDSlIVA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlIVA.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlIVA.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSlIVA.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlIVA.Properties.Appearance.Options.UseFont = True
        Me.INDSlIVA.Properties.Appearance.Options.UseForeColor = True
        Me.INDSlIVA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlIVA.Properties.DisplayMember = "Name"
        Me.INDSlIVA.Properties.NullText = ""
        Me.INDSlIVA.Properties.PopupSizeable = False
        Me.INDSlIVA.Properties.PopupView = Me.INDSlViewIVA
        Me.INDSlIVA.Properties.ShowFooter = False
        Me.INDSlIVA.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlIVA, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlIVA, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlIVA, True)
        Me.INDSlIVA.Size = New System.Drawing.Size(386, 28)
        Me.INDSlIVA.StyleController = Me.LayoutControl1
        Me.INDSlIVA.TabIndex = 18
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlIVA, "1509")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlIVA, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlIVA, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlIVA, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlIVA, False)
        '
        'INDSlViewIVA
        '
        Me.INDSlViewIVA.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDSlViewIVA.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDSlViewIVA.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDSlViewIVA.Appearance.FocusedRow.Options.UseFont = True
        Me.INDSlViewIVA.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDSlViewIVA.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlViewIVA.Appearance.GroupRow.Options.UseFont = True
        Me.INDSlViewIVA.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlViewIVA.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDSlViewIVA.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSlViewIVA.Appearance.Row.Options.UseFont = True
        Me.INDSlViewIVA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeIVA, Me.INDColDescription, Me.INDColPercentage})
        Me.INDSlViewIVA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSlViewIVA.Name = "INDSlViewIVA"
        Me.INDSlViewIVA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSlViewIVA.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSlViewIVA.OptionsView.EnableAppearanceOddRow = True
        Me.INDSlViewIVA.OptionsView.ShowAutoFilterRow = True
        Me.INDSlViewIVA.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDSlViewIVA, False)
        '
        'INDColCodeIVA
        '
        Me.INDColCodeIVA.Caption = "Código"
        Me.INDColCodeIVA.FieldName = "Code"
        Me.INDColCodeIVA.Name = "INDColCodeIVA"
        Me.INDColCodeIVA.Visible = True
        Me.INDColCodeIVA.VisibleIndex = 0
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Descripción"
        Me.INDColDescription.FieldName = "Name"
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 1
        '
        'INDColPercentage
        '
        Me.INDColPercentage.Caption = "%"
        Me.INDColPercentage.FieldName = "Percentage"
        Me.INDColPercentage.Name = "INDColPercentage"
        Me.INDColPercentage.Visible = True
        Me.INDColPercentage.VisibleIndex = 2
        '
        'INDBtnAddDetailEquipment
        '
        Me.INDBtnAddDetailEquipment.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnAddDetailEquipment.Appearance.Options.UseFont = True
        Me.INDBtnAddDetailEquipment.Location = New System.Drawing.Point(852, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAddDetailEquipment, True)
        Me.INDBtnAddDetailEquipment.Name = "INDBtnAddDetailEquipment"
        Me.INDBtnAddDetailEquipment.Size = New System.Drawing.Size(824, 32)
        Me.INDBtnAddDetailEquipment.StyleController = Me.LayoutControl1
        Me.INDBtnAddDetailEquipment.TabIndex = 16
        Me.INDBtnAddDetailEquipment.Text = "Agregar"
        '
        'INDGcEquipmentDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcEquipmentDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcEquipmentDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcEquipmentDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcEquipmentDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcEquipmentDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcEquipmentDetail, False)
        Me.INDGcEquipmentDetail.Location = New System.Drawing.Point(852, 89)
        Me.INDGcEquipmentDetail.MainView = Me.INDGvEquipmentDetail
        Me.INDGcEquipmentDetail.Name = "INDGcEquipmentDetail"
        Me.INDGcEquipmentDetail.Size = New System.Drawing.Size(824, 387)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcEquipmentDetail, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcEquipmentDetail.TabIndex = 15
        Me.INDGcEquipmentDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvEquipmentDetail})
        '
        'INDGvEquipmentDetail
        '
        Me.INDGvEquipmentDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvEquipmentDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvEquipmentDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvEquipmentDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvEquipmentDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvEquipmentDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvEquipmentDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvEquipmentDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvEquipmentDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvEquipmentDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvEquipmentDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvEquipmentDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvEquipmentDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvEquipmentDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvEquipmentDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolPlaca, Me.INDColSerie, Me.INDColResponsible, Me.INDColUnit})
        Me.INDGvEquipmentDetail.GridControl = Me.INDGcEquipmentDetail
        Me.INDGvEquipmentDetail.Name = "INDGvEquipmentDetail"
        Me.INDGvEquipmentDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvEquipmentDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvEquipmentDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvEquipmentDetail.OptionsView.ShowDetailButtons = False
        Me.INDGvEquipmentDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvEquipmentDetail, False)
        '
        'INDcolPlaca
        '
        Me.INDcolPlaca.Caption = "Placa"
        Me.INDcolPlaca.FieldName = "Plate"
        Me.INDcolPlaca.Name = "INDcolPlaca"
        Me.INDcolPlaca.OptionsColumn.AllowFocus = False
        Me.INDcolPlaca.Visible = True
        Me.INDcolPlaca.VisibleIndex = 0
        '
        'INDColSerie
        '
        Me.INDColSerie.Caption = "Serie"
        Me.INDColSerie.FieldName = "Serie"
        Me.INDColSerie.Name = "INDColSerie"
        Me.INDColSerie.OptionsColumn.AllowEdit = False
        Me.INDColSerie.OptionsColumn.AllowFocus = False
        Me.INDColSerie.Visible = True
        Me.INDColSerie.VisibleIndex = 1
        '
        'INDColResponsible
        '
        Me.INDColResponsible.Caption = "Responsable"
        Me.INDColResponsible.FieldName = "NameResponsible"
        Me.INDColResponsible.Name = "INDColResponsible"
        Me.INDColResponsible.OptionsColumn.AllowEdit = False
        Me.INDColResponsible.OptionsColumn.AllowFocus = False
        Me.INDColResponsible.Visible = True
        Me.INDColResponsible.VisibleIndex = 2
        '
        'INDColUnit
        '
        Me.INDColUnit.Caption = "Localización"
        Me.INDColUnit.FieldName = "NameLocation"
        Me.INDColUnit.Name = "INDColUnit"
        Me.INDColUnit.OptionsColumn.AllowEdit = False
        Me.INDColUnit.OptionsColumn.AllowFocus = False
        Me.INDColUnit.Visible = True
        Me.INDColUnit.VisibleIndex = 3
        '
        'INDSlePolize
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlePolize, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlePolize, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSlePolize, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlePolize, False)
        Me.INDSlePolize.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlePolize, False)
        Me.INDSlePolize.Location = New System.Drawing.Point(24, 253)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlePolize, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlePolize.Name = "INDSlePolize"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlePolize, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlePolize, False)
        Me.INDSlePolize.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlePolize.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlePolize.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSlePolize.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlePolize.Properties.Appearance.Options.UseFont = True
        Me.INDSlePolize.Properties.Appearance.Options.UseForeColor = True
        Me.INDSlePolize.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSlePolize.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSlePolize.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlePolize.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSlePolize.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSlePolize.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSlePolize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlePolize.Properties.DisplayMember = "Name"
        Me.INDSlePolize.Properties.NullText = ""
        Me.INDSlePolize.Properties.PopupSizeable = False
        Me.INDSlePolize.Properties.PopupView = Me.SearchLookUpEdit5View
        Me.INDSlePolize.Properties.ShowFooter = False
        Me.INDSlePolize.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlePolize, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlePolize, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlePolize, True)
        Me.INDSlePolize.Size = New System.Drawing.Size(386, 28)
        Me.INDSlePolize.StyleController = Me.LayoutControl1
        Me.INDSlePolize.TabIndex = 13
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlePolize, "1702")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlePolize, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlePolize, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlePolize, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlePolize, False)
        '
        'SearchLookUpEdit5View
        '
        Me.SearchLookUpEdit5View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit5View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit5View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit5View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit5View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit5View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit5View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit5View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit5View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit5View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit5View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit5View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodePolize, Me.INDColNamePolize})
        Me.SearchLookUpEdit5View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit5View.Name = "SearchLookUpEdit5View"
        Me.SearchLookUpEdit5View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit5View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit5View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit5View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit5View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit5View, False)
        '
        'INDColCodePolize
        '
        Me.INDColCodePolize.Caption = "Código"
        Me.INDColCodePolize.FieldName = "Code"
        Me.INDColCodePolize.Name = "INDColCodePolize"
        Me.INDColCodePolize.OptionsColumn.AllowEdit = False
        Me.INDColCodePolize.Visible = True
        Me.INDColCodePolize.VisibleIndex = 0
        Me.INDColCodePolize.Width = 60
        '
        'INDColNamePolize
        '
        Me.INDColNamePolize.Caption = "Descripción"
        Me.INDColNamePolize.FieldName = "Name"
        Me.INDColNamePolize.Name = "INDColNamePolize"
        Me.INDColNamePolize.OptionsColumn.AllowEdit = False
        Me.INDColNamePolize.Visible = True
        Me.INDColNamePolize.VisibleIndex = 1
        Me.INDColNamePolize.Width = 324
        '
        'INDTxtTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTotalValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTotalValue, False)
        Me.INDTxtTotalValue.Enabled = False
        Me.INDTxtTotalValue.EnterMoveNextControl = True
        Me.INDTxtTotalValue.Location = New System.Drawing.Point(438, 433)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTotalValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtTotalValue.Name = "INDTxtTotalValue"
        Me.INDTxtTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtTotalValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtTotalValue.Properties.MaxLength = 18
        Me.INDTxtTotalValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtTotalValue.StyleController = Me.LayoutControl1
        Me.INDTxtTotalValue.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTotalValue, 0)
        '
        'INDTxtModel
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtModel, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtModel, False)
        Me.INDTxtModel.EnterMoveNextControl = True
        Me.INDTxtModel.Location = New System.Drawing.Point(24, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtModel, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtModel.Name = "INDTxtModel"
        Me.INDTxtModel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtModel.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtModel.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtModel.Properties.Appearance.Options.UseFont = True
        Me.INDTxtModel.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxtModel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtModel.Properties.MaxLength = 100
        Me.INDTxtModel.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtModel.StyleController = Me.LayoutControl1
        Me.INDTxtModel.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtModel, 0)
        '
        'INDSleTrademark
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleTrademark, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleTrademark, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleTrademark, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleTrademark, False)
        Me.INDSleTrademark.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleTrademark, False)
        Me.INDSleTrademark.Location = New System.Drawing.Point(24, 133)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleTrademark, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleTrademark.Name = "INDSleTrademark"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleTrademark, False)
        Me.INDSleTrademark.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleTrademark.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleTrademark.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleTrademark.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTrademark.Properties.Appearance.Options.UseFont = True
        Me.INDSleTrademark.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleTrademark.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTrademark.Properties.DisplayMember = "Descripcion"
        Me.INDSleTrademark.Properties.NullText = ""
        Me.INDSleTrademark.Properties.PopupSizeable = False
        Me.INDSleTrademark.Properties.PopupView = Me.SearchLookUpEdit4View
        Me.INDSleTrademark.Properties.ShowFooter = False
        Me.INDSleTrademark.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleTrademark, True)
        Me.INDSleTrademark.Size = New System.Drawing.Size(386, 28)
        Me.INDSleTrademark.StyleController = Me.LayoutControl1
        Me.INDSleTrademark.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleTrademark, "1700")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleTrademark, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleTrademark, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleTrademark, False)
        '
        'SearchLookUpEdit4View
        '
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit4View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit4View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit4View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit4View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit4View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeTrademark, Me.INDColNameTrademark})
        Me.SearchLookUpEdit4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit4View.Name = "SearchLookUpEdit4View"
        Me.SearchLookUpEdit4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit4View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit4View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit4View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit4View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit4View, False)
        '
        'INDColCodeTrademark
        '
        Me.INDColCodeTrademark.Caption = "Código"
        Me.INDColCodeTrademark.FieldName = "Codigo"
        Me.INDColCodeTrademark.Name = "INDColCodeTrademark"
        Me.INDColCodeTrademark.OptionsColumn.AllowEdit = False
        Me.INDColCodeTrademark.Visible = True
        Me.INDColCodeTrademark.VisibleIndex = 0
        Me.INDColCodeTrademark.Width = 60
        '
        'INDColNameTrademark
        '
        Me.INDColNameTrademark.Caption = "Descripción"
        Me.INDColNameTrademark.FieldName = "Descripcion"
        Me.INDColNameTrademark.Name = "INDColNameTrademark"
        Me.INDColNameTrademark.OptionsColumn.AllowEdit = False
        Me.INDColNameTrademark.Visible = True
        Me.INDColNameTrademark.VisibleIndex = 1
        Me.INDColNameTrademark.Width = 324
        '
        'INDSleEquipment
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleEquipment, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleEquipment, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleEquipment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleEquipment, False)
        Me.INDSleEquipment.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleEquipment, False)
        Me.INDSleEquipment.Location = New System.Drawing.Point(24, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleEquipment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleEquipment.Name = "INDSleEquipment"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleEquipment, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleEquipment, False)
        Me.INDSleEquipment.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleEquipment.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleEquipment.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleEquipment.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleEquipment.Properties.Appearance.Options.UseFont = True
        Me.INDSleEquipment.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleEquipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleEquipment.Properties.DisplayMember = "Description"
        Me.INDSleEquipment.Properties.NullText = ""
        Me.INDSleEquipment.Properties.PopupSizeable = False
        Me.INDSleEquipment.Properties.PopupView = Me.ViewItemSearch
        Me.INDSleEquipment.Properties.ShowFooter = False
        Me.INDSleEquipment.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleEquipment, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleEquipment, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleEquipment, True)
        Me.INDSleEquipment.Size = New System.Drawing.Size(386, 28)
        Me.INDSleEquipment.StyleController = Me.LayoutControl1
        Me.INDSleEquipment.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleEquipment, "572")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleEquipment, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleEquipment, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleEquipment, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleEquipment, False)
        '
        'ViewItemSearch
        '
        Me.ViewItemSearch.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.ViewItemSearch.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ViewItemSearch.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.ViewItemSearch.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewItemSearch.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewItemSearch.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewItemSearch.Appearance.GroupRow.Options.UseFont = True
        Me.ViewItemSearch.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewItemSearch.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewItemSearch.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ViewItemSearch.Appearance.Row.Options.UseFont = True
        Me.ViewItemSearch.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColEquipmentCode, Me.INDColEquipmentName, Me.INDColLastCostEquipment})
        Me.ViewItemSearch.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.ViewItemSearch.Name = "ViewItemSearch"
        Me.ViewItemSearch.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewItemSearch.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewItemSearch.OptionsView.EnableAppearanceOddRow = True
        Me.ViewItemSearch.OptionsView.ShowAutoFilterRow = True
        Me.ViewItemSearch.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.ViewItemSearch, False)
        '
        'INDColEquipmentCode
        '
        Me.INDColEquipmentCode.Caption = "Código"
        Me.INDColEquipmentCode.FieldName = "Code"
        Me.INDColEquipmentCode.Name = "INDColEquipmentCode"
        Me.INDColEquipmentCode.OptionsColumn.AllowEdit = False
        Me.INDColEquipmentCode.Visible = True
        Me.INDColEquipmentCode.VisibleIndex = 0
        Me.INDColEquipmentCode.Width = 60
        '
        'INDColEquipmentName
        '
        Me.INDColEquipmentName.Caption = "Descripción"
        Me.INDColEquipmentName.FieldName = "Description"
        Me.INDColEquipmentName.Name = "INDColEquipmentName"
        Me.INDColEquipmentName.OptionsColumn.AllowEdit = False
        Me.INDColEquipmentName.Visible = True
        Me.INDColEquipmentName.VisibleIndex = 1
        Me.INDColEquipmentName.Width = 324
        '
        'INDColLastCostEquipment
        '
        Me.INDColLastCostEquipment.Caption = "LastCostEquipment"
        Me.INDColLastCostEquipment.FieldName = "LastCostEquipment"
        Me.INDColLastCostEquipment.Name = "INDColLastCostEquipment"
        '
        'INDseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantity, False)
        Me.INDseQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseQuantity.EnterMoveNextControl = True
        Me.INDseQuantity.Location = New System.Drawing.Point(24, 373)
        Me.IndigoTextEdit1.SetMascara(Me.INDseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseQuantity.Name = "INDseQuantity"
        Me.INDseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseQuantity.Properties.MaxLength = 9
        Me.INDseQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseQuantity.StyleController = Me.LayoutControl1
        Me.INDseQuantity.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantity, 0)
        '
        'INDTxtEquipmentValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtEquipmentValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtEquipmentValue, False)
        Me.INDTxtEquipmentValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtEquipmentValue.EnterMoveNextControl = True
        Me.INDTxtEquipmentValue.Location = New System.Drawing.Point(438, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtEquipmentValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtEquipmentValue.Name = "INDTxtEquipmentValue"
        Me.INDTxtEquipmentValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtEquipmentValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtEquipmentValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtEquipmentValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtEquipmentValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtEquipmentValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtEquipmentValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtEquipmentValue.Properties.MaxLength = 18
        Me.INDTxtEquipmentValue.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDTxtEquipmentValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtEquipmentValue.StyleController = Me.LayoutControl1
        Me.INDTxtEquipmentValue.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtEquipmentValue, 0)
        '
        'INDTxtTaxesValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTaxesValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTaxesValue, False)
        Me.INDTxtTaxesValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtTaxesValue.EnterMoveNextControl = True
        Me.INDTxtTaxesValue.Location = New System.Drawing.Point(438, 253)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTaxesValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtTaxesValue.Name = "INDTxtTaxesValue"
        Me.INDTxtTaxesValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTaxesValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTaxesValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtTaxesValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtTaxesValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtTaxesValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtTaxesValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtTaxesValue.Properties.MaxLength = 18
        Me.INDTxtTaxesValue.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDTxtTaxesValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtTaxesValue.StyleController = Me.LayoutControl1
        Me.INDTxtTaxesValue.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTaxesValue, 0)
        '
        'INDTxtDiscountValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDiscountValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDiscountValue, False)
        Me.INDTxtDiscountValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtDiscountValue.EnterMoveNextControl = True
        Me.INDTxtDiscountValue.Location = New System.Drawing.Point(438, 373)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDiscountValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtDiscountValue.Name = "INDTxtDiscountValue"
        Me.INDTxtDiscountValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDiscountValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDiscountValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtDiscountValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtDiscountValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtDiscountValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtDiscountValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtDiscountValue.Properties.MaxLength = 18
        Me.INDTxtDiscountValue.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDTxtDiscountValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtDiscountValue.StyleController = Me.LayoutControl1
        Me.INDTxtDiscountValue.TabIndex = 23
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDiscountValue, 0)
        '
        'INDTxtSubTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSubTotal, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSubTotal, False)
        Me.INDTxtSubTotal.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtSubTotal.EnterMoveNextControl = True
        Me.INDTxtSubTotal.Location = New System.Drawing.Point(438, 133)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSubTotal, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtSubTotal.Name = "INDTxtSubTotal"
        Me.INDTxtSubTotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtSubTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtSubTotal.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtSubTotal.Properties.Mask.EditMask = "c0"
        Me.INDTxtSubTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtSubTotal.Properties.MaxLength = 18
        Me.INDTxtSubTotal.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDTxtSubTotal.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtSubTotal.StyleController = Me.LayoutControl1
        Me.INDTxtSubTotal.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSubTotal, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3, Me.LayoutControlGroup4})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1700, 500)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciEquipment, Me.INDLciQuantity, Me.INDLciTrademark, Me.INDLciModel, Me.IVA, Me.INDLciPoliza, Me.INDLciOutstandingQuantity})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 480)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'INDLciEquipment
        '
        Me.INDLciEquipment.Control = Me.INDSleEquipment
        Me.INDLciEquipment.Location = New System.Drawing.Point(0, 0)
        Me.INDLciEquipment.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEquipment.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEquipment.Name = "INDLciEquipment"
        Me.INDLciEquipment.ShowInCustomizationForm = False
        Me.INDLciEquipment.Size = New System.Drawing.Size(390, 60)
        Me.INDLciEquipment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEquipment.Text = "Artículo"
        Me.INDLciEquipment.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEquipment.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.Control = Me.INDseQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 300)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.ShowInCustomizationForm = False
        Me.INDLciQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad"
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciTrademark
        '
        Me.INDLciTrademark.Control = Me.INDSleTrademark
        Me.INDLciTrademark.Location = New System.Drawing.Point(0, 60)
        Me.INDLciTrademark.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciTrademark.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciTrademark.Name = "INDLciTrademark"
        Me.INDLciTrademark.ShowInCustomizationForm = False
        Me.INDLciTrademark.Size = New System.Drawing.Size(390, 60)
        Me.INDLciTrademark.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTrademark.Text = "Marca"
        Me.INDLciTrademark.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTrademark.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciModel
        '
        Me.INDLciModel.Control = Me.INDTxtModel
        Me.INDLciModel.Location = New System.Drawing.Point(0, 120)
        Me.INDLciModel.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciModel.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciModel.Name = "INDLciModel"
        Me.INDLciModel.ShowInCustomizationForm = False
        Me.INDLciModel.Size = New System.Drawing.Size(390, 60)
        Me.INDLciModel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciModel.Text = "Modelo"
        Me.INDLciModel.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciModel.TextSize = New System.Drawing.Size(126, 17)
        '
        'IVA
        '
        Me.IVA.Control = Me.INDSlIVA
        Me.IVA.Location = New System.Drawing.Point(0, 240)
        Me.IVA.MaxSize = New System.Drawing.Size(390, 60)
        Me.IVA.MinSize = New System.Drawing.Size(390, 60)
        Me.IVA.Name = "IVA"
        Me.IVA.ShowInCustomizationForm = False
        Me.IVA.Size = New System.Drawing.Size(390, 60)
        Me.IVA.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.IVA.TextLocation = DevExpress.Utils.Locations.Top
        Me.IVA.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciPoliza
        '
        Me.INDLciPoliza.Control = Me.INDSlePolize
        Me.INDLciPoliza.Location = New System.Drawing.Point(0, 180)
        Me.INDLciPoliza.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPoliza.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPoliza.Name = "INDLciPoliza"
        Me.INDLciPoliza.ShowInCustomizationForm = False
        Me.INDLciPoliza.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPoliza.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPoliza.Text = "Póliza"
        Me.INDLciPoliza.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPoliza.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciOutstandingQuantity
        '
        Me.INDLciOutstandingQuantity.Control = Me.INDSpeOutstandingQuantity
        Me.INDLciOutstandingQuantity.Location = New System.Drawing.Point(0, 360)
        Me.INDLciOutstandingQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciOutstandingQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciOutstandingQuantity.Name = "INDLciOutstandingQuantity"
        Me.INDLciOutstandingQuantity.ShowInCustomizationForm = False
        Me.INDLciOutstandingQuantity.Size = New System.Drawing.Size(390, 67)
        Me.INDLciOutstandingQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciOutstandingQuantity.Text = "Cantidad Pendiente"
        Me.INDLciOutstandingQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciOutstandingQuantity.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciOutstandingQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciEquipmentValue, Me.INDLciTaxes, Me.INDLciTotalValue, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(414, 480)
        Me.LayoutControlGroup3.Text = "Costo"
        '
        'INDLciEquipmentValue
        '
        Me.INDLciEquipmentValue.Control = Me.INDTxtEquipmentValue
        Me.INDLciEquipmentValue.Location = New System.Drawing.Point(0, 0)
        Me.INDLciEquipmentValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEquipmentValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEquipmentValue.Name = "INDLciEquipmentValue"
        Me.INDLciEquipmentValue.ShowInCustomizationForm = False
        Me.INDLciEquipmentValue.Size = New System.Drawing.Size(390, 60)
        Me.INDLciEquipmentValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEquipmentValue.Text = "Valor Artículo"
        Me.INDLciEquipmentValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEquipmentValue.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciTaxes
        '
        Me.INDLciTaxes.Control = Me.INDTxtTaxesValue
        Me.INDLciTaxes.Location = New System.Drawing.Point(0, 180)
        Me.INDLciTaxes.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciTaxes.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciTaxes.Name = "INDLciTaxes"
        Me.INDLciTaxes.Size = New System.Drawing.Size(390, 60)
        Me.INDLciTaxes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTaxes.Text = "Valor IVA"
        Me.INDLciTaxes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTaxes.TextSize = New System.Drawing.Size(126, 17)
        '
        'INDLciTotalValue
        '
        Me.INDLciTotalValue.Control = Me.INDTxtTotalValue
        Me.INDLciTotalValue.Location = New System.Drawing.Point(0, 360)
        Me.INDLciTotalValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciTotalValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciTotalValue.Name = "INDLciTotalValue"
        Me.INDLciTotalValue.Size = New System.Drawing.Size(390, 67)
        Me.INDLciTotalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTotalValue.Text = "Valor Total"
        Me.INDLciTotalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTotalValue.TextSize = New System.Drawing.Size(126, 17)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDTxtSubTotal
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Sub Total"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(126, 17)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSpeIvaPercentage
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "% IVA"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(126, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDSpDiscountPercentage
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "% Descuento"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(126, 17)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDTxtDiscountValue
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 300)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Valor Descuento"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(126, 17)
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem13})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(828, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(852, 480)
        Me.LayoutControlGroup4.Text = "Detalles"
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDGcEquipmentDetail
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(828, 391)
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.INDBtnAddDetailEquipment
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnAdd.Appearance.Options.UseFont = True
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAdd, True)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(1154, 36)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 524)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1158, 40)
        Me.PanelControl1.TabIndex = 2
        '
        'FrmFixedAssetRemissionEntranceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 701)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetRemissionEntranceItem"
        Me.Opacity = 1.0R
        Me.Text = "Artículo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDSpDiscountPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpeIvaPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpeOutstandingQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlViewIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcEquipmentDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvEquipmentDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlePolize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit5View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTrademark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleEquipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewItemSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtEquipmentValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtTaxesValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEquipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTrademark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciOutstandingQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEquipmentValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTaxes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleTrademark As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleEquipment As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents ViewItemSearch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciEquipment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTrademark As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlePolize As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit5View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTxtTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciModel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPoliza As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciEquipmentValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDColCodePolize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNamePolize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCodeTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEquipmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEquipmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDGcEquipmentDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvEquipmentDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnAddDetailEquipment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolPlaca As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSerie As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResponsible As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciTaxes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlIVA As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlViewIVA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IVA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColCodeIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDColLastCostEquipment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpDiscountPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpeIvaPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpeOutstandingQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciOutstandingQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtEquipmentValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtTaxesValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtDiscountValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtSubTotal As DevExpress.XtraEditors.SpinEdit
End Class
