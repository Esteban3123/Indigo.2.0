Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupPurchaseOrderEquipment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPopupPurchaseOrderEquipment))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtDiscountValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDseDiscountPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseIVAPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtSubTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleIVA = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlViewIVA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtIVAValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtModel = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleTrademark = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleItem = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColEquipmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEquipmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseOutstandingQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseCancelledQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtUnitValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsleBranchOffice = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTrademark = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemModel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemOutstandingQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCancelledQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIVA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBranchOffice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemUnitValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIVAValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIVAPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDiscountPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDiscountValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseDiscountPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseIVAPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlViewIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtIVAValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTrademark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseOutstandingQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseCancelledQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBranchOffice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTrademark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOutstandingQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCancelledQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUnitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIVAValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIVAPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDiscountPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDiscountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1074, 676)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1074, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1074, 130)
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
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
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 667)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDtxtDiscountValue)
        Me.LayoutControl1.Controls.Add(Me.INDseDiscountPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDseIVAPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDtxtSubTotalValue)
        Me.LayoutControl1.Controls.Add(Me.INDsleIVA)
        Me.LayoutControl1.Controls.Add(Me.INDtxtIVAValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtTotalValue)
        Me.LayoutControl1.Controls.Add(Me.INDtxtModel)
        Me.LayoutControl1.Controls.Add(Me.INDsleTrademark)
        Me.LayoutControl1.Controls.Add(Me.INDsleItem)
        Me.LayoutControl1.Controls.Add(Me.INDseQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDseOutstandingQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDseCancelledQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDtxtUnitValue)
        Me.LayoutControl1.Controls.Add(Me.INDsleBranchOffice)
        Me.LayoutControl1.Controls.Add(Me.INDsleFunctionalUnit)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(870, 631)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDtxtDiscountValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDiscountValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDiscountValue, False)
        Me.INDtxtDiscountValue.EnterMoveNextControl = True
        Me.INDtxtDiscountValue.Location = New System.Drawing.Point(438, 379)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDiscountValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtDiscountValue.Name = "INDtxtDiscountValue"
        Me.INDtxtDiscountValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtDiscountValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDiscountValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDiscountValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDiscountValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtDiscountValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtDiscountValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDiscountValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDiscountValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDiscountValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDiscountValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDiscountValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDiscountValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtDiscountValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtDiscountValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtDiscountValue.Properties.MaxLength = 18
        Me.INDtxtDiscountValue.Properties.ReadOnly = True
        Me.INDtxtDiscountValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDiscountValue.StyleController = Me.LayoutControl1
        Me.INDtxtDiscountValue.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDiscountValue, 0)
        '
        'INDseDiscountPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseDiscountPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseDiscountPercentage, False)
        Me.INDseDiscountPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseDiscountPercentage.EnterMoveNextControl = True
        Me.INDseDiscountPercentage.Location = New System.Drawing.Point(438, 319)
        Me.IndigoTextEdit1.SetMascara(Me.INDseDiscountPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseDiscountPercentage.Name = "INDseDiscountPercentage"
        Me.INDseDiscountPercentage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseDiscountPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDiscountPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDseDiscountPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDseDiscountPercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseDiscountPercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseDiscountPercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDiscountPercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseDiscountPercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseDiscountPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseDiscountPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseDiscountPercentage.Properties.Mask.EditMask = "P"
        Me.INDseDiscountPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseDiscountPercentage.Properties.MaxLength = 6
        Me.INDseDiscountPercentage.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDseDiscountPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDseDiscountPercentage.StyleController = Me.LayoutControl1
        Me.INDseDiscountPercentage.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseDiscountPercentage, 0)
        '
        'INDseIVAPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseIVAPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseIVAPercentage, False)
        Me.INDseIVAPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseIVAPercentage.EnterMoveNextControl = True
        Me.INDseIVAPercentage.Location = New System.Drawing.Point(438, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDseIVAPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseIVAPercentage.Name = "INDseIVAPercentage"
        Me.INDseIVAPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseIVAPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseIVAPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDseIVAPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDseIVAPercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseIVAPercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseIVAPercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseIVAPercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseIVAPercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseIVAPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseIVAPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseIVAPercentage.Properties.Mask.EditMask = "P"
        Me.INDseIVAPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseIVAPercentage.Properties.MaxLength = 5
        Me.INDseIVAPercentage.Properties.ReadOnly = True
        Me.INDseIVAPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDseIVAPercentage.StyleController = Me.LayoutControl1
        Me.INDseIVAPercentage.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseIVAPercentage, 0)
        '
        'INDtxtSubTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubTotalValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubTotalValue, False)
        Me.INDtxtSubTotalValue.EnterMoveNextControl = True
        Me.INDtxtSubTotalValue.Location = New System.Drawing.Point(438, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubTotalValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtSubTotalValue.Name = "INDtxtSubTotalValue"
        Me.INDtxtSubTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSubTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubTotalValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtSubTotalValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtSubTotalValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotalValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSubTotalValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSubTotalValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubTotalValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtSubTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubTotalValue.Properties.MaxLength = 18
        Me.INDtxtSubTotalValue.Properties.ReadOnly = True
        Me.INDtxtSubTotalValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSubTotalValue.StyleController = Me.LayoutControl1
        Me.INDtxtSubTotalValue.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubTotalValue, 0)
        '
        'INDsleIVA
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleIVA, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleIVA, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleIVA, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleIVA, False)
        Me.INDsleIVA.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleIVA, False)
        Me.INDsleIVA.Location = New System.Drawing.Point(24, 373)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleIVA, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleIVA.Name = "INDsleIVA"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleIVA, False)
        Me.INDsleIVA.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleIVA.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleIVA.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleIVA.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleIVA.Properties.Appearance.Options.UseFont = True
        Me.INDsleIVA.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleIVA.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleIVA.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleIVA.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleIVA.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleIVA.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleIVA.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleIVA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleIVA.Properties.DisplayMember = "Name"
        Me.INDsleIVA.Properties.NullText = ""
        Me.INDsleIVA.Properties.PopupSizeable = False
        Me.INDsleIVA.Properties.PopupView = Me.INDSlViewIVA
        Me.INDsleIVA.Properties.ShowFooter = False
        Me.INDsleIVA.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleIVA, True)
        Me.INDsleIVA.Size = New System.Drawing.Size(386, 28)
        Me.INDsleIVA.StyleController = Me.LayoutControl1
        Me.INDsleIVA.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleIVA, "1509")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleIVA, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleIVA, "{0} - {1}")
        Me.INDsleIVA.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleIVA, False)
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
        'INDtxtIVAValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtIVAValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtIVAValue, False)
        Me.INDtxtIVAValue.Enabled = False
        Me.INDtxtIVAValue.EnterMoveNextControl = True
        Me.INDtxtIVAValue.Location = New System.Drawing.Point(438, 253)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtIVAValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtIVAValue.Name = "INDtxtIVAValue"
        Me.INDtxtIVAValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtIVAValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIVAValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtIVAValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtIVAValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtIVAValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtIVAValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtIVAValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtIVAValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIVAValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtIVAValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtIVAValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtIVAValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtIVAValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtIVAValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtIVAValue.Properties.MaxLength = 18
        Me.INDtxtIVAValue.Properties.ReadOnly = True
        Me.INDtxtIVAValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtIVAValue.StyleController = Me.LayoutControl1
        Me.INDtxtIVAValue.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtIVAValue, 0)
        '
        'INDTxtTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTotalValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTotalValue, False)
        Me.INDTxtTotalValue.Enabled = False
        Me.INDTxtTotalValue.EnterMoveNextControl = True
        Me.INDTxtTotalValue.Location = New System.Drawing.Point(438, 433)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTotalValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtTotalValue.Name = "INDTxtTotalValue"
        Me.INDTxtTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtTotalValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtTotalValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtTotalValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtTotalValue.Properties.MaxLength = 18
        Me.INDTxtTotalValue.Properties.ReadOnly = True
        Me.INDTxtTotalValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtTotalValue.StyleController = Me.LayoutControl1
        Me.INDTxtTotalValue.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTotalValue, 0)
        '
        'INDtxtModel
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtModel, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtModel, True)
        Me.INDtxtModel.EnterMoveNextControl = True
        Me.INDtxtModel.Location = New System.Drawing.Point(24, 313)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtModel, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtModel.Name = "INDtxtModel"
        Me.INDtxtModel.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtModel.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtModel.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtModel.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtModel.Properties.Appearance.Options.UseFont = True
        Me.INDtxtModel.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtModel.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtModel.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtModel.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtModel.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtModel.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtModel.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtModel.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtModel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtModel.Properties.MaxLength = 100
        Me.INDtxtModel.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtModel.StyleController = Me.LayoutControl1
        Me.INDtxtModel.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtModel, 0)
        Me.INDtxtModel.ToolTip = "Este Campo es Necesario"
        '
        'INDsleTrademark
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTrademark, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTrademark, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTrademark, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTrademark, False)
        Me.INDsleTrademark.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTrademark, False)
        Me.INDsleTrademark.Location = New System.Drawing.Point(24, 253)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTrademark, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTrademark.Name = "INDsleTrademark"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTrademark, False)
        Me.INDsleTrademark.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleTrademark.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleTrademark.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTrademark.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTrademark.Properties.Appearance.Options.UseFont = True
        Me.INDsleTrademark.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTrademark.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleTrademark.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleTrademark.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleTrademark.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleTrademark.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleTrademark.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleTrademark.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTrademark.Properties.DisplayMember = "Descripcion"
        Me.INDsleTrademark.Properties.NullText = ""
        Me.INDsleTrademark.Properties.PopupSizeable = False
        Me.INDsleTrademark.Properties.PopupView = Me.SearchLookUpEdit4View
        Me.INDsleTrademark.Properties.ShowFooter = False
        Me.INDsleTrademark.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTrademark, True)
        Me.INDsleTrademark.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTrademark.StyleController = Me.LayoutControl1
        Me.INDsleTrademark.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTrademark, "1700")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTrademark, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTrademark, "{0} - {1}")
        Me.INDsleTrademark.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTrademark, False)
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
        'INDsleItem
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleItem, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleItem, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleItem, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleItem, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleItem, False)
        Me.INDsleItem.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleItem, False)
        Me.INDsleItem.Location = New System.Drawing.Point(24, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleItem, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleItem.Name = "INDsleItem"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleItem, False)
        Me.INDsleItem.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleItem.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleItem.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleItem.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleItem.Properties.Appearance.Options.UseFont = True
        Me.INDsleItem.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleItem.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleItem.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleItem.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleItem.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleItem.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleItem.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleItem.Properties.DisplayMember = "CodeDescription"
        Me.INDsleItem.Properties.NullText = ""
        Me.INDsleItem.Properties.PopupSizeable = False
        Me.INDsleItem.Properties.PopupView = Me.SearchLookUpEdit3View
        Me.INDsleItem.Properties.ShowFooter = False
        Me.INDsleItem.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleItem, True)
        Me.INDsleItem.Size = New System.Drawing.Size(386, 28)
        Me.INDsleItem.StyleController = Me.LayoutControl1
        Me.INDsleItem.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleItem, "572")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleItem, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleItem, "{0} - {1}")
        Me.INDsleItem.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleItem, False)
        '
        'SearchLookUpEdit3View
        '
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColEquipmentCode, Me.INDColEquipmentName})
        Me.SearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit3View.Name = "SearchLookUpEdit3View"
        Me.SearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit3View, False)
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
        'INDseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantity, True)
        Me.INDseQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseQuantity.EnterMoveNextControl = True
        Me.INDseQuantity.Location = New System.Drawing.Point(24, 439)
        Me.IndigoTextEdit1.SetMascara(Me.INDseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseQuantity.Name = "INDseQuantity"
        Me.INDseQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseQuantity.Properties.MaxLength = 9
        Me.INDseQuantity.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDseQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseQuantity.StyleController = Me.LayoutControl1
        Me.INDseQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantity, 0)
        '
        'INDseOutstandingQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseOutstandingQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseOutstandingQuantity, True)
        Me.INDseOutstandingQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseOutstandingQuantity.EnterMoveNextControl = True
        Me.INDseOutstandingQuantity.Location = New System.Drawing.Point(24, 499)
        Me.IndigoTextEdit1.SetMascara(Me.INDseOutstandingQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseOutstandingQuantity.Name = "INDseOutstandingQuantity"
        Me.INDseOutstandingQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseOutstandingQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseOutstandingQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseOutstandingQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseOutstandingQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseOutstandingQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseOutstandingQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseOutstandingQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseOutstandingQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseOutstandingQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseOutstandingQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseOutstandingQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseOutstandingQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseOutstandingQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseOutstandingQuantity.Properties.MaxLength = 9
        Me.INDseOutstandingQuantity.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDseOutstandingQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseOutstandingQuantity.StyleController = Me.LayoutControl1
        Me.INDseOutstandingQuantity.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseOutstandingQuantity, 0)
        '
        'INDseCancelledQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseCancelledQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseCancelledQuantity, True)
        Me.INDseCancelledQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseCancelledQuantity.EnterMoveNextControl = True
        Me.INDseCancelledQuantity.Location = New System.Drawing.Point(24, 559)
        Me.IndigoTextEdit1.SetMascara(Me.INDseCancelledQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseCancelledQuantity.Name = "INDseCancelledQuantity"
        Me.INDseCancelledQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseCancelledQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseCancelledQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseCancelledQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseCancelledQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseCancelledQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseCancelledQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseCancelledQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseCancelledQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseCancelledQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseCancelledQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseCancelledQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseCancelledQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseCancelledQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseCancelledQuantity.Properties.MaxLength = 9
        Me.INDseCancelledQuantity.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDseCancelledQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseCancelledQuantity.StyleController = Me.LayoutControl1
        Me.INDseCancelledQuantity.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseCancelledQuantity, 0)
        '
        'INDtxtUnitValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtUnitValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtUnitValue, True)
        Me.INDtxtUnitValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtUnitValue.EnterMoveNextControl = True
        Me.INDtxtUnitValue.Location = New System.Drawing.Point(438, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtUnitValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtUnitValue.Name = "INDtxtUnitValue"
        Me.INDtxtUnitValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtUnitValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtUnitValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtUnitValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtUnitValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtUnitValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtUnitValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtUnitValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtUnitValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtUnitValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtUnitValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtUnitValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtUnitValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtUnitValue.Properties.Mask.EditMask = "c2"
        Me.INDtxtUnitValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtUnitValue.Properties.MaxLength = 18
        Me.INDtxtUnitValue.Properties.MaxValue = New Decimal(New Integer() {1874919423, 2328306, 0, 0})
        Me.INDtxtUnitValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtUnitValue.StyleController = Me.LayoutControl1
        Me.INDtxtUnitValue.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtUnitValue, 0)
        '
        'INDsleBranchOffice
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBranchOffice, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBranchOffice, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleBranchOffice, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBranchOffice, False)
        Me.INDsleBranchOffice.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBranchOffice, False)
        Me.INDsleBranchOffice.Location = New System.Drawing.Point(24, 133)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBranchOffice, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBranchOffice.Name = "INDsleBranchOffice"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBranchOffice, False)
        Me.INDsleBranchOffice.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleBranchOffice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleBranchOffice.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleBranchOffice.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBranchOffice.Properties.Appearance.Options.UseFont = True
        Me.INDsleBranchOffice.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleBranchOffice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleBranchOffice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleBranchOffice.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleBranchOffice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleBranchOffice.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleBranchOffice.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleBranchOffice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleBranchOffice.Properties.DisplayMember = "Descripcion"
        Me.INDsleBranchOffice.Properties.NullText = ""
        Me.INDsleBranchOffice.Properties.PopupSizeable = False
        Me.INDsleBranchOffice.Properties.PopupView = Me.GridView1
        Me.INDsleBranchOffice.Properties.ShowClearButton = False
        Me.INDsleBranchOffice.Properties.ShowFooter = False
        Me.INDsleBranchOffice.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBranchOffice, True)
        Me.INDsleBranchOffice.Size = New System.Drawing.Size(386, 28)
        Me.INDsleBranchOffice.StyleController = Me.LayoutControl1
        Me.INDsleBranchOffice.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBranchOffice, "563")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBranchOffice, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBranchOffice, "{0} - {1}")
        Me.INDsleBranchOffice.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBranchOffice, False)
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Codigo"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 60
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "Descripcion"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 324
        '
        'INDsleFunctionalUnit
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFunctionalUnit, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFunctionalUnit, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.Location = New System.Drawing.Point(24, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFunctionalUnit.Name = "INDsleFunctionalUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFunctionalUnit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFunctionalUnit.Properties.DisplayMember = "Descripcion"
        Me.INDsleFunctionalUnit.Properties.NullText = ""
        Me.INDsleFunctionalUnit.Properties.PopupSizeable = False
        Me.INDsleFunctionalUnit.Properties.PopupView = Me.GridView2
        Me.INDsleFunctionalUnit.Properties.ShowClearButton = False
        Me.INDsleFunctionalUnit.Properties.ShowFooter = False
        Me.INDsleFunctionalUnit.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFunctionalUnit, True)
        Me.INDsleFunctionalUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFunctionalUnit.StyleController = Me.LayoutControl1
        Me.INDsleFunctionalUnit.TabIndex = 2
        Me.INDsleFunctionalUnit.Tag = ""
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFunctionalUnit, "523")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFunctionalUnit, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFunctionalUnit, "{0} - {1}")
        Me.INDsleFunctionalUnit.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFunctionalUnit, False)
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
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Codigo"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 60
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción"
        Me.GridColumn4.FieldName = "Descripcion"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 324
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3, Me.LayoutControlGroup4})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(870, 631)
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTrademark, Me.INDlyItemModel, Me.INDlyItemQuantity, Me.INDlyItemOutstandingQuantity, Me.INDlyItemCancelledQuantity, Me.INDlyItemIVA, Me.INDlyItemItem, Me.INDlyItemBranchOffice, Me.INDlyItemFunctionalUnit})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(414, 611)
        Me.LayoutControlGroup3.Text = "Datos Principales"
        '
        'INDlyItemTrademark
        '
        Me.INDlyItemTrademark.Control = Me.INDsleTrademark
        Me.INDlyItemTrademark.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemTrademark.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTrademark.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTrademark.Name = "INDlyItemTrademark"
        Me.INDlyItemTrademark.ShowInCustomizationForm = False
        Me.INDlyItemTrademark.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemTrademark.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTrademark.Text = "Marca"
        Me.INDlyItemTrademark.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTrademark.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemModel
        '
        Me.INDlyItemModel.Control = Me.INDtxtModel
        Me.INDlyItemModel.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemModel.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemModel.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemModel.Name = "INDlyItemModel"
        Me.INDlyItemModel.ShowInCustomizationForm = False
        Me.INDlyItemModel.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemModel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemModel.Text = "Modelo"
        Me.INDlyItemModel.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemModel.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemQuantity
        '
        Me.INDlyItemQuantity.Control = Me.INDseQuantity
        Me.INDlyItemQuantity.Location = New System.Drawing.Point(0, 360)
        Me.INDlyItemQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemQuantity.Name = "INDlyItemQuantity"
        Me.INDlyItemQuantity.ShowInCustomizationForm = False
        Me.INDlyItemQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemQuantity.Text = "Cantidad"
        Me.INDlyItemQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemQuantity.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemQuantity.TextToControlDistance = 5
        '
        'INDlyItemOutstandingQuantity
        '
        Me.INDlyItemOutstandingQuantity.Control = Me.INDseOutstandingQuantity
        Me.INDlyItemOutstandingQuantity.Location = New System.Drawing.Point(0, 420)
        Me.INDlyItemOutstandingQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemOutstandingQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemOutstandingQuantity.Name = "INDlyItemOutstandingQuantity"
        Me.INDlyItemOutstandingQuantity.ShowInCustomizationForm = False
        Me.INDlyItemOutstandingQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemOutstandingQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemOutstandingQuantity.Text = "Cantidad Pendiente"
        Me.INDlyItemOutstandingQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemOutstandingQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemOutstandingQuantity.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemOutstandingQuantity.TextToControlDistance = 5
        Me.INDlyItemOutstandingQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemCancelledQuantity
        '
        Me.INDlyItemCancelledQuantity.Control = Me.INDseCancelledQuantity
        Me.INDlyItemCancelledQuantity.Location = New System.Drawing.Point(0, 480)
        Me.INDlyItemCancelledQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCancelledQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCancelledQuantity.Name = "INDlyItemCancelledQuantity"
        Me.INDlyItemCancelledQuantity.ShowInCustomizationForm = False
        Me.INDlyItemCancelledQuantity.Size = New System.Drawing.Size(390, 78)
        Me.INDlyItemCancelledQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCancelledQuantity.Text = "Cantidad Cancelada"
        Me.INDlyItemCancelledQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCancelledQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCancelledQuantity.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemCancelledQuantity.TextToControlDistance = 5
        Me.INDlyItemCancelledQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemIVA
        '
        Me.INDlyItemIVA.Control = Me.INDsleIVA
        Me.INDlyItemIVA.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemIVA.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVA.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVA.Name = "INDlyItemIVA"
        Me.INDlyItemIVA.ShowInCustomizationForm = False
        Me.INDlyItemIVA.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVA.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIVA.Text = "IVA"
        Me.INDlyItemIVA.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIVA.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemItem
        '
        Me.INDlyItemItem.Control = Me.INDsleItem
        Me.INDlyItemItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemItem.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemItem.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemItem.Name = "INDlyItemItem"
        Me.INDlyItemItem.ShowInCustomizationForm = False
        Me.INDlyItemItem.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemItem.Text = "Articulo"
        Me.INDlyItemItem.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemItem.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemBranchOffice
        '
        Me.INDlyItemBranchOffice.AllowHide = False
        Me.INDlyItemBranchOffice.Control = Me.INDsleBranchOffice
        Me.INDlyItemBranchOffice.CustomizationFormText = "Sucursal"
        Me.INDlyItemBranchOffice.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemBranchOffice.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemBranchOffice.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemBranchOffice.Name = "INDlyItemBranchOffice"
        Me.INDlyItemBranchOffice.ShowInCustomizationForm = False
        Me.INDlyItemBranchOffice.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemBranchOffice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBranchOffice.Text = "Sucursal"
        Me.INDlyItemBranchOffice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBranchOffice.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemFunctionalUnit
        '
        Me.INDlyItemFunctionalUnit.Control = Me.INDsleFunctionalUnit
        Me.INDlyItemFunctionalUnit.CustomizationFormText = "Unidad Funcional"
        Me.INDlyItemFunctionalUnit.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemFunctionalUnit.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFunctionalUnit.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFunctionalUnit.Name = "INDlyItemFunctionalUnit"
        Me.INDlyItemFunctionalUnit.ShowInCustomizationForm = False
        Me.INDlyItemFunctionalUnit.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFunctionalUnit.Text = "Unidad Funcional"
        Me.INDlyItemFunctionalUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFunctionalUnit.TextSize = New System.Drawing.Size(110, 17)
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
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemUnitValue, Me.INDlyItemIVAValue, Me.INDLciTotalValue, Me.INDlyItemSubTotalValue, Me.INDlyItemIVAPercentage, Me.INDlyItemDiscountPercentage, Me.INDlyItemDiscountValue})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(436, 611)
        Me.LayoutControlGroup4.Text = "Costo"
        '
        'INDlyItemUnitValue
        '
        Me.INDlyItemUnitValue.Control = Me.INDtxtUnitValue
        Me.INDlyItemUnitValue.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemUnitValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemUnitValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemUnitValue.Name = "INDlyItemUnitValue"
        Me.INDlyItemUnitValue.ShowInCustomizationForm = False
        Me.INDlyItemUnitValue.Size = New System.Drawing.Size(412, 60)
        Me.INDlyItemUnitValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUnitValue.Text = "Valor Articulo"
        Me.INDlyItemUnitValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemUnitValue.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemIVAValue
        '
        Me.INDlyItemIVAValue.Control = Me.INDtxtIVAValue
        Me.INDlyItemIVAValue.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemIVAValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVAValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVAValue.Name = "INDlyItemIVAValue"
        Me.INDlyItemIVAValue.ShowInCustomizationForm = False
        Me.INDlyItemIVAValue.Size = New System.Drawing.Size(412, 60)
        Me.INDlyItemIVAValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIVAValue.Text = "Valor IVA"
        Me.INDlyItemIVAValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIVAValue.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDLciTotalValue
        '
        Me.INDLciTotalValue.Control = Me.INDTxtTotalValue
        Me.INDLciTotalValue.Location = New System.Drawing.Point(0, 360)
        Me.INDLciTotalValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciTotalValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciTotalValue.Name = "INDLciTotalValue"
        Me.INDLciTotalValue.ShowInCustomizationForm = False
        Me.INDLciTotalValue.Size = New System.Drawing.Size(412, 198)
        Me.INDLciTotalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTotalValue.Text = "Valor Total"
        Me.INDLciTotalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTotalValue.TextSize = New System.Drawing.Size(110, 17)
        '
        'INDlyItemSubTotalValue
        '
        Me.INDlyItemSubTotalValue.Control = Me.INDtxtSubTotalValue
        Me.INDlyItemSubTotalValue.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemSubTotalValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSubTotalValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSubTotalValue.Name = "INDlyItemSubTotalValue"
        Me.INDlyItemSubTotalValue.ShowInCustomizationForm = False
        Me.INDlyItemSubTotalValue.Size = New System.Drawing.Size(412, 60)
        Me.INDlyItemSubTotalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubTotalValue.Text = "Sub Total"
        Me.INDlyItemSubTotalValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubTotalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubTotalValue.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemSubTotalValue.TextToControlDistance = 5
        '
        'INDlyItemIVAPercentage
        '
        Me.INDlyItemIVAPercentage.Control = Me.INDseIVAPercentage
        Me.INDlyItemIVAPercentage.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemIVAPercentage.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVAPercentage.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemIVAPercentage.Name = "INDlyItemIVAPercentage"
        Me.INDlyItemIVAPercentage.ShowInCustomizationForm = False
        Me.INDlyItemIVAPercentage.Size = New System.Drawing.Size(412, 60)
        Me.INDlyItemIVAPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIVAPercentage.Text = "% IVA"
        Me.INDlyItemIVAPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIVAPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIVAPercentage.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemIVAPercentage.TextToControlDistance = 5
        '
        'INDlyItemDiscountPercentage
        '
        Me.INDlyItemDiscountPercentage.Control = Me.INDseDiscountPercentage
        Me.INDlyItemDiscountPercentage.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemDiscountPercentage.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDiscountPercentage.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDiscountPercentage.Name = "INDlyItemDiscountPercentage"
        Me.INDlyItemDiscountPercentage.ShowInCustomizationForm = False
        Me.INDlyItemDiscountPercentage.Size = New System.Drawing.Size(412, 60)
        Me.INDlyItemDiscountPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDiscountPercentage.Text = "% Descuento"
        Me.INDlyItemDiscountPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDiscountPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDiscountPercentage.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemDiscountPercentage.TextToControlDistance = 5
        '
        'INDlyItemDiscountValue
        '
        Me.INDlyItemDiscountValue.Control = Me.INDtxtDiscountValue
        Me.INDlyItemDiscountValue.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemDiscountValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDiscountValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDiscountValue.Name = "INDlyItemDiscountValue"
        Me.INDlyItemDiscountValue.Size = New System.Drawing.Size(412, 60)
        Me.INDlyItemDiscountValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDiscountValue.Text = "Valor Descuento"
        Me.INDlyItemDiscountValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDiscountValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDiscountValue.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlyItemDiscountValue.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnAdd.Appearance.Options.UseFont = True
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAdd, True)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(866, 32)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 638)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(870, 36)
        Me.PanelControl1.TabIndex = 2
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'FrmPopupPurchaseOrderEquipment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 811)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupPurchaseOrderEquipment"
        Me.Opacity = 1.0R
        Me.Text = "Artículo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseDiscountPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseIVAPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlViewIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtIVAValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTrademark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseOutstandingQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseCancelledQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBranchOffice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTrademark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOutstandingQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCancelledQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUnitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIVAValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIVAPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDiscountPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDiscountValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDsleTrademark As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleItem As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTrademark As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemModel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemUnitValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDColCodeTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEquipmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEquipmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDtxtIVAValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemIVAValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleIVA As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlViewIVA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemIVA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColCodeIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDlyItemOutstandingQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCancelledQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtSubTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSubTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseIVAPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemIVAPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseDiscountPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemDiscountPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDiscountValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemDiscountValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseOutstandingQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseCancelledQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDtxtUnitValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDsleBranchOffice As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemBranchOffice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
