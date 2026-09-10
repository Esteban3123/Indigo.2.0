Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupProductConsignmentTransfer
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccMoreInfo = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTeAvailableQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeRepositionQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeConsumedQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeIncrementQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeMaxLimit = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeOpenQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciOpenQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMaxLimit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIncrementQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciConsumedQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRepositionQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAvailableQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDPpLoading = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDPcePhysicalInventory = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrPhysicalInventory1 = New Presentation.Controls.CtrPhysicalInventory()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.WpfMessageConversation1 = New Presentation.Base.WPFMessageConversation()
        Me.INDMeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSpnProductCost = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnDeliveryQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPceBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceProducts = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDSleTargetWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTargetWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBatchCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgAditionalInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProductCost = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDPccMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccMoreInfo.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDTeAvailableQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeRepositionQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeConsumedQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeIncrementQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeMaxLimit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeOpenQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciOpenQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMaxLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIncrementQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciConsumedQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRepositionQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAvailableQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcePhysicalInventory.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnProductCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnDeliveryQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTargetWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTargetWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBatchCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAditionalInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProductCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Controls.Add(Me.INDSbAdd)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(962, 513)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(962, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(962, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDSbAdd.Location = New System.Drawing.Point(2, 474)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(958, 37)
        Me.INDSbAdd.TabIndex = 0
        Me.INDSbAdd.Text = "Agregar"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 467)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDPccMoreInfo)
        Me.INDLcRoot.Controls.Add(Me.INDPcePhysicalInventory)
        Me.INDLcRoot.Controls.Add(Me.INDPupCProducto)
        Me.INDLcRoot.Controls.Add(Me.INDMeDescription)
        Me.INDLcRoot.Controls.Add(Me.INDSpnProductCost)
        Me.INDLcRoot.Controls.Add(Me.INDSpnDeliveryQuantity)
        Me.INDLcRoot.Controls.Add(Me.INDPceBatchSerial)
        Me.INDLcRoot.Controls.Add(Me.INDPceProducts)
        Me.INDLcRoot.Controls.Add(Me.INDSleTargetWarehouse)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(758, 467)
        Me.INDLcRoot.TabIndex = 2
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDPccMoreInfo
        '
        Me.INDPccMoreInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPccMoreInfo.Controls.Add(Me.LayoutControl2)
        Me.INDPccMoreInfo.Controls.Add(Me.INDPpLoading)
        Me.INDPccMoreInfo.Controls.Add(Me.LabelControl2)
        Me.INDPccMoreInfo.Location = New System.Drawing.Point(120, 114)
        Me.INDPccMoreInfo.Manager = Me.BarManager1
        Me.INDPccMoreInfo.Name = "INDPccMoreInfo"
        Me.INDPccMoreInfo.Size = New System.Drawing.Size(236, 305)
        Me.INDPccMoreInfo.TabIndex = 15
        Me.INDPccMoreInfo.Visible = False
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDTeAvailableQuantity)
        Me.LayoutControl2.Controls.Add(Me.INDTeRepositionQuantity)
        Me.LayoutControl2.Controls.Add(Me.INDTeConsumedQuantity)
        Me.LayoutControl2.Controls.Add(Me.INDTeIncrementQuantity)
        Me.LayoutControl2.Controls.Add(Me.INDTeMaxLimit)
        Me.LayoutControl2.Controls.Add(Me.INDTeOpenQuantity)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 35)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(236, 270)
        Me.LayoutControl2.TabIndex = 16
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDTeAvailableQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeAvailableQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeAvailableQuantity, False)
        Me.INDTeAvailableQuantity.EditValue = "0"
        Me.INDTeAvailableQuantity.Location = New System.Drawing.Point(151, 203)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeAvailableQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeAvailableQuantity.Name = "INDTeAvailableQuantity"
        Me.INDTeAvailableQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeAvailableQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTeAvailableQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeAvailableQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeAvailableQuantity.Properties.ReadOnly = True
        Me.INDTeAvailableQuantity.Size = New System.Drawing.Size(71, 28)
        Me.INDTeAvailableQuantity.StyleController = Me.LayoutControl2
        Me.INDTeAvailableQuantity.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeAvailableQuantity, 0)
        '
        'INDTeRepositionQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeRepositionQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeRepositionQuantity, False)
        Me.INDTeRepositionQuantity.EditValue = "0"
        Me.INDTeRepositionQuantity.Location = New System.Drawing.Point(151, 171)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeRepositionQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeRepositionQuantity.Name = "INDTeRepositionQuantity"
        Me.INDTeRepositionQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeRepositionQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTeRepositionQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeRepositionQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeRepositionQuantity.Properties.ReadOnly = True
        Me.INDTeRepositionQuantity.Size = New System.Drawing.Size(71, 28)
        Me.INDTeRepositionQuantity.StyleController = Me.LayoutControl2
        Me.INDTeRepositionQuantity.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeRepositionQuantity, 0)
        '
        'INDTeConsumedQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeConsumedQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeConsumedQuantity, False)
        Me.INDTeConsumedQuantity.EditValue = "0"
        Me.INDTeConsumedQuantity.Location = New System.Drawing.Point(151, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeConsumedQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeConsumedQuantity.Name = "INDTeConsumedQuantity"
        Me.INDTeConsumedQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeConsumedQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTeConsumedQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeConsumedQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeConsumedQuantity.Properties.ReadOnly = True
        Me.INDTeConsumedQuantity.Size = New System.Drawing.Size(71, 28)
        Me.INDTeConsumedQuantity.StyleController = Me.LayoutControl2
        Me.INDTeConsumedQuantity.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeConsumedQuantity, 0)
        '
        'INDTeIncrementQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeIncrementQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeIncrementQuantity, False)
        Me.INDTeIncrementQuantity.EditValue = "0"
        Me.INDTeIncrementQuantity.Location = New System.Drawing.Point(151, 107)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeIncrementQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeIncrementQuantity.Name = "INDTeIncrementQuantity"
        Me.INDTeIncrementQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeIncrementQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTeIncrementQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeIncrementQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeIncrementQuantity.Properties.ReadOnly = True
        Me.INDTeIncrementQuantity.Size = New System.Drawing.Size(71, 28)
        Me.INDTeIncrementQuantity.StyleController = Me.LayoutControl2
        Me.INDTeIncrementQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeIncrementQuantity, 0)
        '
        'INDTeMaxLimit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeMaxLimit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeMaxLimit, False)
        Me.INDTeMaxLimit.EditValue = "0"
        Me.INDTeMaxLimit.Location = New System.Drawing.Point(151, 75)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeMaxLimit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeMaxLimit.Name = "INDTeMaxLimit"
        Me.INDTeMaxLimit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeMaxLimit.Properties.Appearance.Options.UseFont = True
        Me.INDTeMaxLimit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeMaxLimit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeMaxLimit.Properties.ReadOnly = True
        Me.INDTeMaxLimit.Size = New System.Drawing.Size(71, 28)
        Me.INDTeMaxLimit.StyleController = Me.LayoutControl2
        Me.INDTeMaxLimit.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeMaxLimit, 0)
        '
        'INDTeOpenQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeOpenQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeOpenQuantity, False)
        Me.INDTeOpenQuantity.EditValue = "0"
        Me.INDTeOpenQuantity.Location = New System.Drawing.Point(151, 43)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeOpenQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeOpenQuantity.Name = "INDTeOpenQuantity"
        Me.INDTeOpenQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeOpenQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTeOpenQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeOpenQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeOpenQuantity.Properties.ReadOnly = True
        Me.INDTeOpenQuantity.Size = New System.Drawing.Size(71, 28)
        Me.INDTeOpenQuantity.StyleController = Me.LayoutControl2
        Me.INDTeOpenQuantity.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeOpenQuantity, 0)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(236, 270)
        Me.LayoutControlGroup2.TextVisible = False
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciOpenQuantity, Me.INDLciMaxLimit, Me.INDLciIncrementQuantity, Me.INDLciConsumedQuantity, Me.INDLciRepositionQuantity, Me.INDLciAvailableQuantity, Me.EmptySpaceItem2})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "INDLciData"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(236, 270)
        Me.LayoutControlGroup3.Text = "Datos"
        '
        'INDLciOpenQuantity
        '
        Me.INDLciOpenQuantity.Control = Me.INDTeOpenQuantity
        Me.INDLciOpenQuantity.Location = New System.Drawing.Point(0, 0)
        Me.INDLciOpenQuantity.Name = "INDLciOpenQuantity"
        Me.INDLciOpenQuantity.Size = New System.Drawing.Size(212, 32)
        Me.INDLciOpenQuantity.Text = "Cantidad apertura"
        Me.INDLciOpenQuantity.TextSize = New System.Drawing.Size(134, 17)
        Me.INDLciOpenQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciMaxLimit
        '
        Me.INDLciMaxLimit.Control = Me.INDTeMaxLimit
        Me.INDLciMaxLimit.Location = New System.Drawing.Point(0, 32)
        Me.INDLciMaxLimit.Name = "INDLciMaxLimit"
        Me.INDLciMaxLimit.Size = New System.Drawing.Size(212, 32)
        Me.INDLciMaxLimit.Text = "Límite máximo"
        Me.INDLciMaxLimit.TextSize = New System.Drawing.Size(134, 17)
        Me.INDLciMaxLimit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciIncrementQuantity
        '
        Me.INDLciIncrementQuantity.Control = Me.INDTeIncrementQuantity
        Me.INDLciIncrementQuantity.Location = New System.Drawing.Point(0, 64)
        Me.INDLciIncrementQuantity.Name = "INDLciIncrementQuantity"
        Me.INDLciIncrementQuantity.Size = New System.Drawing.Size(212, 32)
        Me.INDLciIncrementQuantity.Text = "Cantidad incremento"
        Me.INDLciIncrementQuantity.TextSize = New System.Drawing.Size(134, 17)
        Me.INDLciIncrementQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciConsumedQuantity
        '
        Me.INDLciConsumedQuantity.Control = Me.INDTeConsumedQuantity
        Me.INDLciConsumedQuantity.Location = New System.Drawing.Point(0, 96)
        Me.INDLciConsumedQuantity.Name = "INDLciConsumedQuantity"
        Me.INDLciConsumedQuantity.Size = New System.Drawing.Size(212, 32)
        Me.INDLciConsumedQuantity.Text = "Cantidad consumida"
        Me.INDLciConsumedQuantity.TextSize = New System.Drawing.Size(134, 17)
        Me.INDLciConsumedQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciRepositionQuantity
        '
        Me.INDLciRepositionQuantity.Control = Me.INDTeRepositionQuantity
        Me.INDLciRepositionQuantity.Location = New System.Drawing.Point(0, 128)
        Me.INDLciRepositionQuantity.Name = "INDLciRepositionQuantity"
        Me.INDLciRepositionQuantity.Size = New System.Drawing.Size(212, 32)
        Me.INDLciRepositionQuantity.Text = "Cantidad reposición"
        Me.INDLciRepositionQuantity.TextSize = New System.Drawing.Size(134, 17)
        Me.INDLciRepositionQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciAvailableQuantity
        '
        Me.INDLciAvailableQuantity.Control = Me.INDTeAvailableQuantity
        Me.INDLciAvailableQuantity.Location = New System.Drawing.Point(0, 160)
        Me.INDLciAvailableQuantity.Name = "INDLciAvailableQuantity"
        Me.INDLciAvailableQuantity.Size = New System.Drawing.Size(212, 32)
        Me.INDLciAvailableQuantity.Text = "Cantidad disponible"
        Me.INDLciAvailableQuantity.TextSize = New System.Drawing.Size(134, 17)
        Me.INDLciAvailableQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 192)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(212, 25)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDPpLoading
        '
        Me.INDPpLoading.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPpLoading.Appearance.Options.UseBackColor = True
        Me.INDPpLoading.AppearanceCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDPpLoading.AppearanceCaption.Options.UseFont = True
        Me.INDPpLoading.Caption = "Cargando"
        Me.INDPpLoading.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.INDPpLoading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPpLoading.Location = New System.Drawing.Point(0, 35)
        Me.INDPpLoading.Name = "INDPpLoading"
        Me.INDPpLoading.ShowDescription = False
        Me.INDPpLoading.Size = New System.Drawing.Size(236, 270)
        Me.INDPpLoading.TabIndex = 18
        Me.INDPpLoading.Text = "Cargando"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 15.0!)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl2.Location = New System.Drawing.Point(0, 0)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(236, 35)
        Me.LabelControl2.TabIndex = 17
        Me.LabelControl2.Text = "Más Información"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(962, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 648)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(962, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 643)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(962, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 643)
        '
        'INDPcePhysicalInventory
        '
        Me.INDPcePhysicalInventory.Controls.Add(Me.CtrPhysicalInventory1)
        Me.INDPcePhysicalInventory.Location = New System.Drawing.Point(673, 80)
        Me.INDPcePhysicalInventory.Name = "INDPcePhysicalInventory"
        Me.INDPcePhysicalInventory.Size = New System.Drawing.Size(560, 352)
        Me.INDPcePhysicalInventory.TabIndex = 13
        '
        'CtrPhysicalInventory1
        '
        Me.CtrPhysicalInventory1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrPhysicalInventory1.Location = New System.Drawing.Point(0, 0)
        Me.CtrPhysicalInventory1.Name = "CtrPhysicalInventory1"
        Me.CtrPhysicalInventory1.Size = New System.Drawing.Size(560, 352)
        Me.CtrPhysicalInventory1.TabIndex = 0
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Controls.Add(Me.ElementHost1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(517, 397)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(746, 431)
        Me.INDPupCProducto.TabIndex = 7
        '
        'CtrProducts1
        '
        Me.CtrProducts1.DataSource = Nothing
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(746, 431)
        Me.CtrProducts1.TabIndex = 0
        '
        'ElementHost1
        '
        Me.ElementHost1.Location = New System.Drawing.Point(447, 219)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(8, 8)
        Me.ElementHost1.TabIndex = 1
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Me.WpfMessageConversation1
        '
        'INDMeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDescription, False)
        Me.INDMeDescription.EnterMoveNextControl = True
        Me.INDMeDescription.Location = New System.Drawing.Point(438, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDescription.Name = "INDMeDescription"
        Me.INDMeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDescription.Properties.MaxLength = 500
        Me.INDMeDescription.Size = New System.Drawing.Size(386, 154)
        Me.INDMeDescription.StyleController = Me.INDLcRoot
        Me.INDMeDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDescription, 0)
        '
        'INDSpnProductCost
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnProductCost, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnProductCost, True)
        Me.INDSpnProductCost.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnProductCost.EnterMoveNextControl = True
        Me.INDSpnProductCost.Location = New System.Drawing.Point(438, 75)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnProductCost, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDSpnProductCost.Name = "INDSpnProductCost"
        Me.INDSpnProductCost.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnProductCost.Properties.Appearance.Options.UseFont = True
        Me.INDSpnProductCost.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSpnProductCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDSpnProductCost.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnProductCost.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnProductCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnProductCost.Properties.Mask.EditMask = "c0"
        Me.INDSpnProductCost.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnProductCost.Properties.ReadOnly = True
        Me.INDSpnProductCost.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnProductCost.StyleController = Me.INDLcRoot
        Me.INDSpnProductCost.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnProductCost, 0)
        '
        'INDSpnDeliveryQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnDeliveryQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnDeliveryQuantity, True)
        Me.INDSpnDeliveryQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnDeliveryQuantity.EnterMoveNextControl = True
        Me.INDSpnDeliveryQuantity.Location = New System.Drawing.Point(24, 267)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnDeliveryQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpnDeliveryQuantity.Name = "INDSpnDeliveryQuantity"
        Me.INDSpnDeliveryQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnDeliveryQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSpnDeliveryQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnDeliveryQuantity.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.MoreInfo_16x16_blue
        Me.INDSpnDeliveryQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSpnDeliveryQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnDeliveryQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnDeliveryQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnDeliveryQuantity.StyleController = Me.INDLcRoot
        Me.INDSpnDeliveryQuantity.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnDeliveryQuantity, 0)
        '
        'INDPceBatchSerial
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceBatchSerial, Nothing)
        Me.INDPceBatchSerial.Location = New System.Drawing.Point(24, 203)
        Me.INDPceBatchSerial.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.Name = "INDPceBatchSerial"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDPceBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceBatchSerial.Properties.PopupControl = Me.INDPcePhysicalInventory
        Me.INDPceBatchSerial.Properties.PopupSizeable = False
        Me.INDPceBatchSerial.Properties.ShowPopupCloseButton = False
        Me.INDPceBatchSerial.Size = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.StyleController = Me.INDLcRoot
        Me.INDPceBatchSerial.TabIndex = 2
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceBatchSerial, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceBatchSerial, Nothing)
        '
        'INDPceProducts
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProducts, False)
        Me.INDPceProducts.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProducts, Nothing)
        Me.INDPceProducts.Location = New System.Drawing.Point(24, 139)
        Me.INDPceProducts.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.Name = "INDPceProducts"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProducts, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProducts, False)
        Me.INDPceProducts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProducts.Properties.Appearance.Options.UseFont = True
        Me.INDPceProducts.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProducts.Properties.PopupControl = Me.INDPupCProducto
        Me.INDPceProducts.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPceProducts.Size = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.StyleController = Me.INDLcRoot
        Me.INDPceProducts.TabIndex = 1
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceProducts, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceProducts, Nothing)
        '
        'INDSleTargetWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleTargetWarehouse, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleTargetWarehouse, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleTargetWarehouse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleTargetWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.INDSleTargetWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.INDSleTargetWarehouse.Location = New System.Drawing.Point(24, 75)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleTargetWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleTargetWarehouse.Name = "INDSleTargetWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleTargetWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.INDSleTargetWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleTargetWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleTargetWarehouse.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleTargetWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTargetWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleTargetWarehouse.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleTargetWarehouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTargetWarehouse.Properties.DisplayMember = "CodeName"
        Me.INDSleTargetWarehouse.Properties.NullText = ""
        Me.INDSleTargetWarehouse.Properties.PopupSizeable = False
        Me.INDSleTargetWarehouse.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleTargetWarehouse.Properties.ShowFooter = False
        Me.INDSleTargetWarehouse.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleTargetWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleTargetWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleTargetWarehouse, True)
        Me.INDSleTargetWarehouse.Size = New System.Drawing.Size(386, 28)
        Me.INDSleTargetWarehouse.StyleController = Me.INDLcRoot
        Me.INDSleTargetWarehouse.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleTargetWarehouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleTargetWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleTargetWarehouse, "{0} - {1}")
        Me.INDSleTargetWarehouse.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleTargetWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleTargetWarehouse, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
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
        Me.GridColumn1.Width = 264
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 682
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMain, Me.INDLcgAditionalInfo})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(848, 450)
        Me.Root.TextVisible = False
        '
        'INDLcgMain
        '
        Me.INDLcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMain, False)
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTargetWarehouse, Me.INDLciProduct, Me.INDLciBatchCode, Me.INDLciQuantity})
        Me.INDLcgMain.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMain.Name = "INDLcgMain"
        Me.INDLcgMain.Size = New System.Drawing.Size(414, 430)
        Me.INDLcgMain.Text = "Datos Principales"
        '
        'INDLciTargetWarehouse
        '
        Me.INDLciTargetWarehouse.Control = Me.INDSleTargetWarehouse
        Me.INDLciTargetWarehouse.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTargetWarehouse.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciTargetWarehouse.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciTargetWarehouse.Name = "INDLciTargetWarehouse"
        Me.INDLciTargetWarehouse.Size = New System.Drawing.Size(390, 64)
        Me.INDLciTargetWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTargetWarehouse.Text = "Almacén destino"
        Me.INDLciTargetWarehouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTargetWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTargetWarehouse.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLciTargetWarehouse.TextToControlDistance = 5
        '
        'INDLciProduct
        '
        Me.INDLciProduct.Control = Me.INDPceProducts
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 64)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.Size = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Producto"
        Me.INDLciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLciProduct.TextToControlDistance = 5
        '
        'INDLciBatchCode
        '
        Me.INDLciBatchCode.Control = Me.INDPceBatchSerial
        Me.INDLciBatchCode.Location = New System.Drawing.Point(0, 128)
        Me.INDLciBatchCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciBatchCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciBatchCode.Name = "INDLciBatchCode"
        Me.INDLciBatchCode.Size = New System.Drawing.Size(390, 64)
        Me.INDLciBatchCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBatchCode.Text = "Lote/Serial"
        Me.INDLciBatchCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBatchCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBatchCode.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLciBatchCode.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.Control = Me.INDSpnDeliveryQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 192)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.Size = New System.Drawing.Size(390, 185)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad a entregar"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLcgAditionalInfo
        '
        Me.INDLcgAditionalInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAditionalInfo.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAditionalInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAditionalInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAditionalInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAditionalInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAditionalInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAditionalInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAditionalInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAditionalInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAditionalInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAditionalInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAditionalInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAditionalInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgAditionalInfo, False)
        Me.INDLcgAditionalInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProductCost, Me.INDLciDescription})
        Me.INDLcgAditionalInfo.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgAditionalInfo.Name = "INDLcgAditionalInfo"
        Me.INDLcgAditionalInfo.Size = New System.Drawing.Size(414, 430)
        Me.INDLcgAditionalInfo.Text = "Información Adicional"
        '
        'INDLciProductCost
        '
        Me.INDLciProductCost.Control = Me.INDSpnProductCost
        Me.INDLciProductCost.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProductCost.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciProductCost.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciProductCost.Name = "INDLciProductCost"
        Me.INDLciProductCost.Size = New System.Drawing.Size(390, 64)
        Me.INDLciProductCost.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProductCost.Text = "Costo del producto"
        Me.INDLciProductCost.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProductCost.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProductCost.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciProductCost.TextToControlDistance = 5
        '
        'INDLciDescription
        '
        Me.INDLciDescription.Control = Me.INDMeDescription
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 64)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 180)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 180)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.Size = New System.Drawing.Size(390, 313)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmPopupProductConsignmentTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 648)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Name = "FrmPopupProductConsignmentTransfer"
        Me.Opacity = 1.0R
        Me.Text = "Producto"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDPccMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccMoreInfo.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDTeAvailableQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeRepositionQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeConsumedQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeIncrementQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeMaxLimit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeOpenQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciOpenQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMaxLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIncrementQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciConsumedQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRepositionQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAvailableQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcePhysicalInventory.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnProductCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnDeliveryQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTargetWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTargetWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBatchCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAditionalInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProductCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleTargetWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTargetWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents ElementHost1 As Windows.Forms.Integration.ElementHost
    Friend WpfMessageConversation1 As Base.WPFMessageConversation
    Friend WithEvents CtrProducts1 As CtrProducts
    Friend WithEvents INDPceProducts As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDPceBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciBatchCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSpnDeliveryQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDMeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSpnProductCost As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLcgAditionalInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciProductCost As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcePhysicalInventory As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrPhysicalInventory1 As CtrPhysicalInventory
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccMoreInfo As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTeAvailableQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeRepositionQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeConsumedQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeIncrementQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeMaxLimit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeOpenQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciOpenQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciMaxLimit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciIncrementQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciConsumedQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciRepositionQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAvailableQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Private WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents INDPpLoading As DevExpress.XtraWaitForm.ProgressPanel
End Class
