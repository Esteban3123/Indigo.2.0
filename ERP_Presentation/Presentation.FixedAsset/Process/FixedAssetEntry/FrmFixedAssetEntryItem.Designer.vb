Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetEntryItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetEntryItem))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyItem = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtDiscountValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDseDiscountPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseIvaPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtSubTotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleIVA = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlViewIVA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIVAPercentage = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDtxtIvaValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnAddItemDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPlaca = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSerie = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResponsible = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDslePolicy = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit5View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtModel = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleTrademark = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleItem = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.ViewItemSearch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColEquipmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEquipmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtUnitValue = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygItem = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTrademark = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemModel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIVA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPolicy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygCostItem = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemUnitValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIvaValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIvaPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDiscountPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDiscountValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDBtnAddItem = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpanelButtonsAdd = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyItem.SuspendLayout()
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseDiscountPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseIvaPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlViewIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIVAPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtIvaValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePolicy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit5View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTrademark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewItemSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTrademark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygCostItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUnitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIvaValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIvaPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDiscountPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDiscountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtonsAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtonsAdd.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyItem)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtonsAdd)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1589, 726)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.ToolBars.Size = New System.Drawing.Size(1589, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1589, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControlPanel1.Appearance.Image = CType(resources.GetObject("CtrNavigationControlPanel1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControlPanel1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControlPanel1.Appearance.Options.UseImage = True
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyItem
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 9)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 715)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyItem
        '
        Me.INDlyItem.Controls.Add(Me.INDMeObservation)
        Me.INDlyItem.Controls.Add(Me.INDtxtDiscountValue)
        Me.INDlyItem.Controls.Add(Me.INDseDiscountPercentage)
        Me.INDlyItem.Controls.Add(Me.INDseIvaPercentage)
        Me.INDlyItem.Controls.Add(Me.INDtxtSubTotal)
        Me.INDlyItem.Controls.Add(Me.INDsleIVA)
        Me.INDlyItem.Controls.Add(Me.INDtxtIvaValue)
        Me.INDlyItem.Controls.Add(Me.INDbtnAddItemDetail)
        Me.INDlyItem.Controls.Add(Me.INDgcDetail)
        Me.INDlyItem.Controls.Add(Me.INDslePolicy)
        Me.INDlyItem.Controls.Add(Me.INDtxtTotalValue)
        Me.INDlyItem.Controls.Add(Me.INDtxtModel)
        Me.INDlyItem.Controls.Add(Me.INDsleTrademark)
        Me.INDlyItem.Controls.Add(Me.INDsleItem)
        Me.INDlyItem.Controls.Add(Me.INDseQuantity)
        Me.INDlyItem.Controls.Add(Me.INDtxtUnitValue)
        Me.INDlyItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyItem.Location = New System.Drawing.Point(202, 9)
        Me.INDlyItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlyItem.Name = "INDlyItem"
        Me.INDlyItem.Root = Me.LayoutControlGroup1
        Me.INDlyItem.Size = New System.Drawing.Size(1385, 666)
        Me.INDlyItem.TabIndex = 3
        Me.INDlyItem.Text = "LayoutControl1"
        '
        'INDMeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeObservation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeObservation, False)
        Me.INDMeObservation.EnterMoveNextControl = True
        Me.INDMeObservation.Location = New System.Drawing.Point(24, 535)
        Me.INDMeObservation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeObservation.Name = "INDMeObservation"
        Me.INDMeObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservation.Properties.MaxLength = 1000
        Me.INDMeObservation.Size = New System.Drawing.Size(451, 39)
        Me.INDMeObservation.StyleController = Me.INDlyItem
        Me.INDMeObservation.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeObservation, 0)
        '
        'INDtxtDiscountValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDiscountValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDiscountValue, False)
        Me.INDtxtDiscountValue.EnterMoveNextControl = True
        Me.INDtxtDiscountValue.Location = New System.Drawing.Point(503, 313)
        Me.INDtxtDiscountValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDiscountValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
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
        Me.INDtxtDiscountValue.Size = New System.Drawing.Size(451, 36)
        Me.INDtxtDiscountValue.StyleController = Me.INDlyItem
        Me.INDtxtDiscountValue.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDiscountValue, 0)
        '
        'INDseDiscountPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseDiscountPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseDiscountPercentage, False)
        Me.INDseDiscountPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseDiscountPercentage.EnterMoveNextControl = True
        Me.INDseDiscountPercentage.Location = New System.Drawing.Point(503, 239)
        Me.INDseDiscountPercentage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDseDiscountPercentage.Size = New System.Drawing.Size(451, 36)
        Me.INDseDiscountPercentage.StyleController = Me.INDlyItem
        Me.INDseDiscountPercentage.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseDiscountPercentage, 0)
        '
        'INDseIvaPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseIvaPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseIvaPercentage, False)
        Me.INDseIvaPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseIvaPercentage.EnterMoveNextControl = True
        Me.INDseIvaPercentage.Location = New System.Drawing.Point(503, 387)
        Me.INDseIvaPercentage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDseIvaPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseIvaPercentage.Name = "INDseIvaPercentage"
        Me.INDseIvaPercentage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseIvaPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseIvaPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDseIvaPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDseIvaPercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseIvaPercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseIvaPercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseIvaPercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseIvaPercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseIvaPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseIvaPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseIvaPercentage.Properties.Mask.EditMask = "P"
        Me.INDseIvaPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseIvaPercentage.Properties.MaxLength = 6
        Me.INDseIvaPercentage.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDseIvaPercentage.Size = New System.Drawing.Size(451, 36)
        Me.INDseIvaPercentage.StyleController = Me.INDlyItem
        Me.INDseIvaPercentage.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseIvaPercentage, 0)
        '
        'INDtxtSubTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubTotal, False)
        Me.INDtxtSubTotal.EnterMoveNextControl = True
        Me.INDtxtSubTotal.Location = New System.Drawing.Point(503, 165)
        Me.INDtxtSubTotal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubTotal, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtSubTotal.Name = "INDtxtSubTotal"
        Me.INDtxtSubTotal.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSubTotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotal.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubTotal.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtSubTotal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtSubTotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSubTotal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSubTotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubTotal.Properties.Mask.EditMask = "c0"
        Me.INDtxtSubTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubTotal.Properties.MaxLength = 18
        Me.INDtxtSubTotal.Properties.ReadOnly = True
        Me.INDtxtSubTotal.Size = New System.Drawing.Size(451, 36)
        Me.INDtxtSubTotal.StyleController = Me.INDlyItem
        Me.INDtxtSubTotal.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubTotal, 0)
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
        Me.INDsleIVA.Location = New System.Drawing.Point(24, 387)
        Me.INDsleIVA.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDsleIVA.Properties.DisplayMember = "CodeName"
        Me.INDsleIVA.Properties.NullText = ""
        Me.INDsleIVA.Properties.PopupSizeable = False
        Me.INDsleIVA.Properties.PopupView = Me.INDSlViewIVA
        Me.INDsleIVA.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepIVAPercentage})
        Me.INDsleIVA.Properties.ShowFooter = False
        Me.INDsleIVA.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleIVA, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleIVA, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleIVA, True)
        Me.INDsleIVA.Size = New System.Drawing.Size(451, 36)
        Me.INDsleIVA.StyleController = Me.INDlyItem
        Me.INDsleIVA.TabIndex = 4
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
        Me.INDSlViewIVA.DetailHeight = 431
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
        Me.INDColCodeIVA.MinWidth = 23
        Me.INDColCodeIVA.Name = "INDColCodeIVA"
        Me.INDColCodeIVA.Visible = True
        Me.INDColCodeIVA.VisibleIndex = 0
        Me.INDColCodeIVA.Width = 321
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Descripción"
        Me.INDColDescription.FieldName = "Name"
        Me.INDColDescription.MinWidth = 23
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 1
        Me.INDColDescription.Width = 651
        '
        'INDColPercentage
        '
        Me.INDColPercentage.Caption = "%"
        Me.INDColPercentage.ColumnEdit = Me.INDrepIVAPercentage
        Me.INDColPercentage.FieldName = "Percentage"
        Me.INDColPercentage.MinWidth = 23
        Me.INDColPercentage.Name = "INDColPercentage"
        Me.INDColPercentage.Visible = True
        Me.INDColPercentage.VisibleIndex = 2
        Me.INDColPercentage.Width = 652
        '
        'INDrepIVAPercentage
        '
        Me.INDrepIVAPercentage.AutoHeight = False
        Me.INDrepIVAPercentage.Mask.EditMask = "P2"
        Me.INDrepIVAPercentage.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepIVAPercentage.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepIVAPercentage.Name = "INDrepIVAPercentage"
        '
        'INDtxtIvaValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtIvaValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtIvaValue, False)
        Me.INDtxtIvaValue.Enabled = False
        Me.INDtxtIvaValue.EnterMoveNextControl = True
        Me.INDtxtIvaValue.Location = New System.Drawing.Point(503, 461)
        Me.INDtxtIvaValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtIvaValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtIvaValue.Name = "INDtxtIvaValue"
        Me.INDtxtIvaValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtIvaValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIvaValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtIvaValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtIvaValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtIvaValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtIvaValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtIvaValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtIvaValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtIvaValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtIvaValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtIvaValue.Properties.MaxLength = 18
        Me.INDtxtIvaValue.Properties.ReadOnly = True
        Me.INDtxtIvaValue.Size = New System.Drawing.Size(451, 36)
        Me.INDtxtIvaValue.StyleController = Me.INDlyItem
        Me.INDtxtIvaValue.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtIvaValue, 0)
        '
        'INDbtnAddItemDetail
        '
        Me.INDbtnAddItemDetail.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddItemDetail.Appearance.Options.UseFont = True
        Me.INDbtnAddItemDetail.Location = New System.Drawing.Point(982, 60)
        Me.INDbtnAddItemDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddItemDetail, True)
        Me.INDbtnAddItemDetail.Name = "INDbtnAddItemDetail"
        Me.INDbtnAddItemDetail.Size = New System.Drawing.Size(962, 40)
        Me.INDbtnAddItemDetail.StyleController = Me.INDlyItem
        Me.INDbtnAddItemDetail.TabIndex = 13
        Me.INDbtnAddItemDetail.Text = "Agregar"
        '
        'INDgcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetail, Nothing)
        Me.INDgcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetail, False)
        Me.INDgcDetail.Location = New System.Drawing.Point(982, 104)
        Me.INDgcDetail.MainView = Me.INDviewDetail
        Me.INDgcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgcDetail.Name = "INDgcDetail"
        Me.INDgcDetail.Size = New System.Drawing.Size(962, 516)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetail.TabIndex = 14
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDetail})
        '
        'INDviewDetail
        '
        Me.INDviewDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDetail.Appearance.Row.Options.UseFont = True
        Me.INDviewDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.INDcolPlaca, Me.INDColSerie, Me.INDColResponsible})
        Me.INDviewDetail.DetailHeight = 431
        Me.INDviewDetail.GridControl = Me.INDgcDetail
        Me.INDviewDetail.Name = "INDviewDetail"
        Me.INDviewDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDetail.OptionsView.ShowDetailButtons = False
        Me.INDviewDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDetail, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Orden"
        Me.GridColumn2.FieldName = "OrderItem"
        Me.GridColumn2.MinWidth = 23
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 143
        '
        'INDcolPlaca
        '
        Me.INDcolPlaca.Caption = "Placa"
        Me.INDcolPlaca.FieldName = "Plate"
        Me.INDcolPlaca.MinWidth = 23
        Me.INDcolPlaca.Name = "INDcolPlaca"
        Me.INDcolPlaca.OptionsColumn.AllowEdit = False
        Me.INDcolPlaca.OptionsColumn.AllowFocus = False
        Me.INDcolPlaca.Visible = True
        Me.INDcolPlaca.VisibleIndex = 1
        Me.INDcolPlaca.Width = 400
        '
        'INDColSerie
        '
        Me.INDColSerie.Caption = "Serie"
        Me.INDColSerie.FieldName = "Serie"
        Me.INDColSerie.MinWidth = 23
        Me.INDColSerie.Name = "INDColSerie"
        Me.INDColSerie.OptionsColumn.AllowEdit = False
        Me.INDColSerie.OptionsColumn.AllowFocus = False
        Me.INDColSerie.Visible = True
        Me.INDColSerie.VisibleIndex = 2
        Me.INDColSerie.Width = 400
        '
        'INDColResponsible
        '
        Me.INDColResponsible.Caption = "Responsable"
        Me.INDColResponsible.FieldName = "ResponsibleCodeName"
        Me.INDColResponsible.MinWidth = 23
        Me.INDColResponsible.Name = "INDColResponsible"
        Me.INDColResponsible.OptionsColumn.AllowEdit = False
        Me.INDColResponsible.OptionsColumn.AllowFocus = False
        Me.INDColResponsible.Visible = True
        Me.INDColResponsible.VisibleIndex = 3
        Me.INDColResponsible.Width = 400
        '
        'INDslePolicy
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePolicy, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePolicy, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePolicy, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDslePolicy, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePolicy, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePolicy, False)
        Me.INDslePolicy.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePolicy, False)
        Me.INDslePolicy.Location = New System.Drawing.Point(24, 313)
        Me.INDslePolicy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePolicy, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePolicy.Name = "INDslePolicy"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePolicy, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePolicy, False)
        Me.INDslePolicy.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDslePolicy.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDslePolicy.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDslePolicy.Properties.Appearance.Options.UseBackColor = True
        Me.INDslePolicy.Properties.Appearance.Options.UseFont = True
        Me.INDslePolicy.Properties.Appearance.Options.UseForeColor = True
        Me.INDslePolicy.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDslePolicy.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDslePolicy.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDslePolicy.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDslePolicy.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDslePolicy.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDslePolicy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslePolicy.Properties.DisplayMember = "CodeName"
        Me.INDslePolicy.Properties.NullText = ""
        Me.INDslePolicy.Properties.PopupSizeable = False
        Me.INDslePolicy.Properties.PopupView = Me.SearchLookUpEdit5View
        Me.INDslePolicy.Properties.ShowFooter = False
        Me.INDslePolicy.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePolicy, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDslePolicy, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePolicy, True)
        Me.INDslePolicy.Size = New System.Drawing.Size(451, 36)
        Me.INDslePolicy.StyleController = Me.INDlyItem
        Me.INDslePolicy.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePolicy, "1702")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePolicy, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePolicy, "{0} - {1}")
        Me.INDslePolicy.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePolicy, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePolicy, False)
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
        Me.SearchLookUpEdit5View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn25, Me.GridColumn26})
        Me.SearchLookUpEdit5View.DetailHeight = 431
        Me.SearchLookUpEdit5View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit5View.Name = "SearchLookUpEdit5View"
        Me.SearchLookUpEdit5View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit5View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit5View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit5View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit5View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit5View, False)
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Código"
        Me.GridColumn25.FieldName = "Code"
        Me.GridColumn25.MinWidth = 23
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 0
        Me.GridColumn25.Width = 269
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Nombre"
        Me.GridColumn26.FieldName = "Name"
        Me.GridColumn26.MinWidth = 23
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 1
        Me.GridColumn26.Width = 1354
        '
        'INDtxtTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalValue, False)
        Me.INDtxtTotalValue.Enabled = False
        Me.INDtxtTotalValue.EnterMoveNextControl = True
        Me.INDtxtTotalValue.Location = New System.Drawing.Point(503, 535)
        Me.INDtxtTotalValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtTotalValue.Name = "INDtxtTotalValue"
        Me.INDtxtTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtTotalValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalValue.Properties.MaxLength = 18
        Me.INDtxtTotalValue.Properties.ReadOnly = True
        Me.INDtxtTotalValue.Size = New System.Drawing.Size(451, 36)
        Me.INDtxtTotalValue.StyleController = Me.INDlyItem
        Me.INDtxtTotalValue.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalValue, 0)
        '
        'INDtxtModel
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtModel, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtModel, True)
        Me.INDtxtModel.EnterMoveNextControl = True
        Me.INDtxtModel.Location = New System.Drawing.Point(24, 239)
        Me.INDtxtModel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDtxtModel.Size = New System.Drawing.Size(451, 36)
        Me.INDtxtModel.StyleController = Me.INDlyItem
        Me.INDtxtModel.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtModel, 0)
        Me.INDtxtModel.ToolTip = "Este Campo es Necesario"
        '
        'INDsleTrademark
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTrademark, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTrademark, AppearanceObject6)
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
        Me.INDsleTrademark.Location = New System.Drawing.Point(24, 165)
        Me.INDsleTrademark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDsleTrademark.Properties.DisplayMember = "CodeDescription"
        Me.INDsleTrademark.Properties.NullText = ""
        Me.INDsleTrademark.Properties.PopupSizeable = False
        Me.INDsleTrademark.Properties.PopupView = Me.SearchLookUpEdit4View
        Me.INDsleTrademark.Properties.ShowFooter = False
        Me.INDsleTrademark.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTrademark, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTrademark, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTrademark, True)
        Me.INDsleTrademark.Size = New System.Drawing.Size(451, 36)
        Me.INDsleTrademark.StyleController = Me.INDlyItem
        Me.INDsleTrademark.TabIndex = 1
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
        Me.SearchLookUpEdit4View.DetailHeight = 431
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
        Me.INDColCodeTrademark.MinWidth = 23
        Me.INDColCodeTrademark.Name = "INDColCodeTrademark"
        Me.INDColCodeTrademark.OptionsColumn.AllowEdit = False
        Me.INDColCodeTrademark.Visible = True
        Me.INDColCodeTrademark.VisibleIndex = 0
        Me.INDColCodeTrademark.Width = 70
        '
        'INDColNameTrademark
        '
        Me.INDColNameTrademark.Caption = "Descripción"
        Me.INDColNameTrademark.FieldName = "Descripcion"
        Me.INDColNameTrademark.MinWidth = 23
        Me.INDColNameTrademark.Name = "INDColNameTrademark"
        Me.INDColNameTrademark.OptionsColumn.AllowEdit = False
        Me.INDColNameTrademark.Visible = True
        Me.INDColNameTrademark.VisibleIndex = 1
        Me.INDColNameTrademark.Width = 378
        '
        'INDsleItem
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleItem, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleItem, AppearanceObject8)
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
        Me.INDsleItem.Location = New System.Drawing.Point(24, 91)
        Me.INDsleItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDsleItem.Properties.PopupView = Me.ViewItemSearch
        Me.INDsleItem.Properties.ShowFooter = False
        Me.INDsleItem.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleItem, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleItem, True)
        Me.INDsleItem.Size = New System.Drawing.Size(451, 36)
        Me.INDsleItem.StyleController = Me.INDlyItem
        Me.INDsleItem.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleItem, "572")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleItem, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleItem, "{0} - {1}")
        Me.INDsleItem.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleItem, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleItem, False)
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
        Me.ViewItemSearch.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColEquipmentCode, Me.INDColEquipmentName, Me.GridColumn1})
        Me.ViewItemSearch.DetailHeight = 431
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
        Me.INDColEquipmentCode.MinWidth = 23
        Me.INDColEquipmentCode.Name = "INDColEquipmentCode"
        Me.INDColEquipmentCode.OptionsColumn.AllowEdit = False
        Me.INDColEquipmentCode.Visible = True
        Me.INDColEquipmentCode.VisibleIndex = 0
        Me.INDColEquipmentCode.Width = 259
        '
        'INDColEquipmentName
        '
        Me.INDColEquipmentName.Caption = "Descripción"
        Me.INDColEquipmentName.FieldName = "Description"
        Me.INDColEquipmentName.MinWidth = 23
        Me.INDColEquipmentName.Name = "INDColEquipmentName"
        Me.INDColEquipmentName.OptionsColumn.AllowEdit = False
        Me.INDColEquipmentName.Visible = True
        Me.INDColEquipmentName.VisibleIndex = 1
        Me.INDColEquipmentName.Width = 887
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Último Costo"
        Me.GridColumn1.DisplayFormat.FormatString = "C0"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn1.FieldName = "LastCostItem"
        Me.GridColumn1.MinWidth = 23
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 478
        '
        'INDseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantity, True)
        Me.INDseQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseQuantity.EnterMoveNextControl = True
        Me.INDseQuantity.Location = New System.Drawing.Point(24, 461)
        Me.INDseQuantity.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDseQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseQuantity.Size = New System.Drawing.Size(451, 36)
        Me.INDseQuantity.StyleController = Me.INDlyItem
        Me.INDseQuantity.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantity, 0)
        '
        'INDtxtUnitValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtUnitValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtUnitValue, True)
        Me.INDtxtUnitValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtUnitValue.EnterMoveNextControl = True
        Me.INDtxtUnitValue.Location = New System.Drawing.Point(503, 91)
        Me.INDtxtUnitValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.INDtxtUnitValue.Properties.MaxLength = 16
        Me.INDtxtUnitValue.Properties.MaxValue = New Decimal(New Integer() {1874919423, 2328306, 0, 0})
        Me.INDtxtUnitValue.Size = New System.Drawing.Size(451, 36)
        Me.INDtxtUnitValue.StyleController = Me.INDlyItem
        Me.INDtxtUnitValue.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtUnitValue, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygItem, Me.INDlygCostItem, Me.INDlygDetails})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1968, 644)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygItem
        '
        Me.INDlygItem.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygItem.AppearanceGroup.Options.UseFont = True
        Me.INDlygItem.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygItem.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygItem, False)
        Me.INDlygItem.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemItem, Me.INDlyItemQuantity, Me.INDlyItemTrademark, Me.INDlyItemModel, Me.INDlyItemIVA, Me.INDlyItemPolicy, Me.LayoutControlItem1})
        Me.INDlygItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlygItem.Name = "INDlygItem"
        Me.INDlygItem.Size = New System.Drawing.Size(479, 624)
        Me.INDlygItem.Text = "Datos Principales"
        '
        'INDlyItemItem
        '
        Me.INDlyItemItem.Control = Me.INDsleItem
        Me.INDlyItemItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemItem.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemItem.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemItem.Name = "INDlyItemItem"
        Me.INDlyItemItem.ShowInCustomizationForm = False
        Me.INDlyItemItem.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemItem.Text = "Artículo"
        Me.INDlyItemItem.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemItem.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemItem.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemItem.TextToControlDistance = 5
        '
        'INDlyItemQuantity
        '
        Me.INDlyItemQuantity.Control = Me.INDseQuantity
        Me.INDlyItemQuantity.Location = New System.Drawing.Point(0, 370)
        Me.INDlyItemQuantity.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemQuantity.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemQuantity.Name = "INDlyItemQuantity"
        Me.INDlyItemQuantity.ShowInCustomizationForm = False
        Me.INDlyItemQuantity.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemQuantity.Text = "Cantidad"
        Me.INDlyItemQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemQuantity.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemQuantity.TextToControlDistance = 5
        '
        'INDlyItemTrademark
        '
        Me.INDlyItemTrademark.Control = Me.INDsleTrademark
        Me.INDlyItemTrademark.Location = New System.Drawing.Point(0, 74)
        Me.INDlyItemTrademark.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemTrademark.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemTrademark.Name = "INDlyItemTrademark"
        Me.INDlyItemTrademark.ShowInCustomizationForm = False
        Me.INDlyItemTrademark.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemTrademark.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTrademark.Text = "Marca"
        Me.INDlyItemTrademark.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTrademark.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTrademark.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemTrademark.TextToControlDistance = 5
        '
        'INDlyItemModel
        '
        Me.INDlyItemModel.Control = Me.INDtxtModel
        Me.INDlyItemModel.Location = New System.Drawing.Point(0, 148)
        Me.INDlyItemModel.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemModel.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemModel.Name = "INDlyItemModel"
        Me.INDlyItemModel.ShowInCustomizationForm = False
        Me.INDlyItemModel.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemModel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemModel.Text = "Modelo"
        Me.INDlyItemModel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemModel.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemModel.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemModel.TextToControlDistance = 5
        '
        'INDlyItemIVA
        '
        Me.INDlyItemIVA.Control = Me.INDsleIVA
        Me.INDlyItemIVA.Location = New System.Drawing.Point(0, 296)
        Me.INDlyItemIVA.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemIVA.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemIVA.Name = "INDlyItemIVA"
        Me.INDlyItemIVA.ShowInCustomizationForm = False
        Me.INDlyItemIVA.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemIVA.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIVA.Text = "IVA"
        Me.INDlyItemIVA.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIVA.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIVA.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemIVA.TextToControlDistance = 5
        '
        'INDlyItemPolicy
        '
        Me.INDlyItemPolicy.Control = Me.INDslePolicy
        Me.INDlyItemPolicy.Location = New System.Drawing.Point(0, 222)
        Me.INDlyItemPolicy.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemPolicy.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemPolicy.Name = "INDlyItemPolicy"
        Me.INDlyItemPolicy.ShowInCustomizationForm = False
        Me.INDlyItemPolicy.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemPolicy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPolicy.Text = "Póliza"
        Me.INDlyItemPolicy.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPolicy.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPolicy.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemPolicy.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDMeObservation
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 444)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(455, 74)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(455, 74)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(455, 120)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Observación"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(157, 26)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDlygCostItem
        '
        Me.INDlygCostItem.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCostItem.AppearanceGroup.Options.UseFont = True
        Me.INDlygCostItem.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCostItem.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygCostItem.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCostItem.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygCostItem.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygCostItem.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygCostItem.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCostItem.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygCostItem.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCostItem.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygCostItem.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCostItem.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygCostItem, False)
        Me.INDlygCostItem.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemUnitValue, Me.INDlyItemIvaValue, Me.INDlyItemTotalValue, Me.INDlyItemIvaPercentage, Me.INDlyItemDiscountPercentage, Me.INDlyItemDiscountValue, Me.INDlyItemSubTotal})
        Me.INDlygCostItem.Location = New System.Drawing.Point(479, 0)
        Me.INDlygCostItem.Name = "INDlygCostItem"
        Me.INDlygCostItem.Size = New System.Drawing.Size(479, 624)
        Me.INDlygCostItem.Text = "Costo"
        '
        'INDlyItemUnitValue
        '
        Me.INDlyItemUnitValue.Control = Me.INDtxtUnitValue
        Me.INDlyItemUnitValue.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemUnitValue.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemUnitValue.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemUnitValue.Name = "INDlyItemUnitValue"
        Me.INDlyItemUnitValue.ShowInCustomizationForm = False
        Me.INDlyItemUnitValue.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemUnitValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUnitValue.Text = "Valor Unitario"
        Me.INDlyItemUnitValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemUnitValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemUnitValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemUnitValue.TextToControlDistance = 5
        '
        'INDlyItemIvaValue
        '
        Me.INDlyItemIvaValue.Control = Me.INDtxtIvaValue
        Me.INDlyItemIvaValue.Location = New System.Drawing.Point(0, 370)
        Me.INDlyItemIvaValue.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemIvaValue.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemIvaValue.Name = "INDlyItemIvaValue"
        Me.INDlyItemIvaValue.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemIvaValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIvaValue.Text = "Valor IVA"
        Me.INDlyItemIvaValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIvaValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIvaValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemIvaValue.TextToControlDistance = 5
        '
        'INDlyItemTotalValue
        '
        Me.INDlyItemTotalValue.Control = Me.INDtxtTotalValue
        Me.INDlyItemTotalValue.Location = New System.Drawing.Point(0, 444)
        Me.INDlyItemTotalValue.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemTotalValue.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemTotalValue.Name = "INDlyItemTotalValue"
        Me.INDlyItemTotalValue.Size = New System.Drawing.Size(455, 120)
        Me.INDlyItemTotalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalValue.Text = "Valor Total"
        Me.INDlyItemTotalValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTotalValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemTotalValue.TextToControlDistance = 5
        '
        'INDlyItemIvaPercentage
        '
        Me.INDlyItemIvaPercentage.Control = Me.INDseIvaPercentage
        Me.INDlyItemIvaPercentage.Location = New System.Drawing.Point(0, 296)
        Me.INDlyItemIvaPercentage.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemIvaPercentage.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemIvaPercentage.Name = "INDlyItemIvaPercentage"
        Me.INDlyItemIvaPercentage.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemIvaPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIvaPercentage.Text = "% IVA"
        Me.INDlyItemIvaPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIvaPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemIvaPercentage.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemIvaPercentage.TextToControlDistance = 5
        '
        'INDlyItemDiscountPercentage
        '
        Me.INDlyItemDiscountPercentage.Control = Me.INDseDiscountPercentage
        Me.INDlyItemDiscountPercentage.Location = New System.Drawing.Point(0, 148)
        Me.INDlyItemDiscountPercentage.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemDiscountPercentage.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemDiscountPercentage.Name = "INDlyItemDiscountPercentage"
        Me.INDlyItemDiscountPercentage.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemDiscountPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDiscountPercentage.Text = "% Descuento"
        Me.INDlyItemDiscountPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDiscountPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDiscountPercentage.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemDiscountPercentage.TextToControlDistance = 5
        '
        'INDlyItemDiscountValue
        '
        Me.INDlyItemDiscountValue.Control = Me.INDtxtDiscountValue
        Me.INDlyItemDiscountValue.Location = New System.Drawing.Point(0, 222)
        Me.INDlyItemDiscountValue.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemDiscountValue.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemDiscountValue.Name = "INDlyItemDiscountValue"
        Me.INDlyItemDiscountValue.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemDiscountValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDiscountValue.Text = "Valor Descuento"
        Me.INDlyItemDiscountValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDiscountValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDiscountValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemDiscountValue.TextToControlDistance = 5
        '
        'INDlyItemSubTotal
        '
        Me.INDlyItemSubTotal.Control = Me.INDtxtSubTotal
        Me.INDlyItemSubTotal.Location = New System.Drawing.Point(0, 74)
        Me.INDlyItemSubTotal.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemSubTotal.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyItemSubTotal.Name = "INDlyItemSubTotal"
        Me.INDlyItemSubTotal.Size = New System.Drawing.Size(455, 74)
        Me.INDlyItemSubTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubTotal.Text = "Sub Total"
        Me.INDlyItemSubTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubTotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubTotal.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemSubTotal.TextToControlDistance = 5
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
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem13})
        Me.INDlygDetails.Location = New System.Drawing.Point(958, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(990, 624)
        Me.INDlygDetails.Text = "Detalles"
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDgcDetail
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 44)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(966, 0)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(966, 30)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(966, 520)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.INDbtnAddItemDetail
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(966, 44)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(966, 44)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(966, 44)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDBtnAddItem
        '
        Me.INDBtnAddItem.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnAddItem.Appearance.Options.UseFont = True
        Me.INDBtnAddItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAddItem.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAddItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAddItem, True)
        Me.INDBtnAddItem.Name = "INDBtnAddItem"
        Me.INDBtnAddItem.Size = New System.Drawing.Size(1381, 45)
        Me.INDBtnAddItem.TabIndex = 0
        Me.INDBtnAddItem.Text = "Agregar"
        '
        'INDpanelButtonsAdd
        '
        Me.INDpanelButtonsAdd.Controls.Add(Me.INDBtnAddItem)
        Me.INDpanelButtonsAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtonsAdd.Location = New System.Drawing.Point(202, 675)
        Me.INDpanelButtonsAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpanelButtonsAdd.MaximumSize = New System.Drawing.Size(0, 49)
        Me.INDpanelButtonsAdd.MinimumSize = New System.Drawing.Size(0, 49)
        Me.INDpanelButtonsAdd.Name = "INDpanelButtonsAdd"
        Me.INDpanelButtonsAdd.Size = New System.Drawing.Size(1385, 49)
        Me.INDpanelButtonsAdd.TabIndex = 2
        '
        'FrmFixedAssetEntryItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1589, 863)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetEntryItem"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Text = "Articulo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyItem.ResumeLayout(False)
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseDiscountPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseIvaPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlViewIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIVAPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtIvaValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePolicy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit5View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTrademark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewItemSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTrademark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygCostItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUnitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIvaValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIvaPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDiscountPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDiscountValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtonsAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtonsAdd.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDpanelButtonsAdd As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAddItem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItem As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtDiscountValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDseDiscountPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseIvaPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDtxtSubTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleIVA As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlViewIVA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCodeIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtIvaValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbtnAddItemDetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolPlaca As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSerie As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResponsible As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDslePolicy As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit5View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtxtTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleTrademark As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCodeTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleItem As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents ViewItemSearch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColEquipmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEquipmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygItem As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTrademark As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemModel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemIVA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPolicy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygCostItem As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemUnitValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemIvaValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSubTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemIvaPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDiscountPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDiscountValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtUnitValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDMeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepIVAPercentage As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
