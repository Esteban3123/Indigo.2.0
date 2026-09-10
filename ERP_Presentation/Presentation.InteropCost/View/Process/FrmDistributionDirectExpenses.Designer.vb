Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDistributionDirectExpenses
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDistributionDirectExpenses))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmemoObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDebtDistribution = New Presentation.Controls.ExportStructureButton()
        Me.INDTxtValue = New DevExpress.XtraEditors.TextEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.MBtnAddCenter = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnRemoveCenter = New DevExpress.XtraBars.BarButtonItem()
        Me.INDpccAddProductionCenter = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtCostValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleMeasurementUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSearchMeasurementUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAddProductionCenter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleProductionCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtMaximumAmount = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSleRound = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliProductionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAmount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLCIRound = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpceAddDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcDirectCostDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDirectCostDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColProductionCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColMeasurementUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCostValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrpspnValue = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.ColPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepPercentage = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDrpsleProductionCenter = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleGeneralExpense = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvGeneralExpense = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliGeneralExpenses = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgOtherInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PopupMenuProductionCenter = New DevExpress.XtraBars.PopupMenu()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDmemoObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccAddProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccAddProductionCenter.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtCostValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMeasurementUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSearchMeasurementUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleProductionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtMaximumAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleRound.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLCIRound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAddDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDirectCostDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDirectCostDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrpspnValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrpsleProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleGeneralExpense.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvGeneralExpense, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliGeneralExpenses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgOtherInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1133, 577)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1133, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1133, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 568)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDmemoObservation)
        Me.INDlcRoot.Controls.Add(Me.INDebtDistribution)
        Me.INDlcRoot.Controls.Add(Me.INDTxtValue)
        Me.INDlcRoot.Controls.Add(Me.INDpccAddProductionCenter)
        Me.INDlcRoot.Controls.Add(Me.INDpceAddDetail)
        Me.INDlcRoot.Controls.Add(Me.INDgcDirectCostDetail)
        Me.INDlcRoot.Controls.Add(Me.INDsleGeneralExpense)
        Me.INDlcRoot.Controls.Add(Me.INDtxtDescription)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(929, 568)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDmemoObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoObservation, True)
        Me.INDmemoObservation.EnterMoveNextControl = True
        Me.INDmemoObservation.Location = New System.Drawing.Point(-285, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoObservation.Name = "INDmemoObservation"
        Me.INDmemoObservation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmemoObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoObservation.Properties.Appearance.Options.UseFont = True
        Me.INDmemoObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoObservation.Size = New System.Drawing.Size(386, 90)
        Me.INDmemoObservation.StyleController = Me.INDlcRoot
        Me.INDmemoObservation.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoObservation, 0)
        Me.INDmemoObservation.ToolTip = "Este Campo es Necesario"
        '
        'INDebtDistribution
        '
        Me.INDebtDistribution.Image = CType(resources.GetObject("INDebtDistribution.Image"), System.Drawing.Image)
        Me.INDebtDistribution.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDebtDistribution.Location = New System.Drawing.Point(859, 59)
        Me.INDebtDistribution.Name = "INDebtDistribution"
        Me.INDebtDistribution.Size = New System.Drawing.Size(42, 38)
        Me.INDebtDistribution.StyleController = Me.INDlcRoot
        Me.INDebtDistribution.TabIndex = 18
        Me.INDebtDistribution.ToolTip = "Exportar estructura a Excel"
        '
        'INDTxtValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValue, True)
        Me.INDTxtValue.EditValue = ""
        Me.INDTxtValue.EnterMoveNextControl = True
        Me.INDTxtValue.Location = New System.Drawing.Point(-285, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtValue.MenuManager = Me.BarManager1
        Me.INDTxtValue.Name = "INDTxtValue"
        Me.INDTxtValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtValue.StyleController = Me.INDlcRoot
        Me.INDTxtValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValue, 0)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MBtnAddCenter, Me.MBtnRemoveCenter})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Size = New System.Drawing.Size(1133, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 695)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1133, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 690)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1133, 5)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 690)
        '
        'MBtnAddCenter
        '
        Me.MBtnAddCenter.Caption = "Agregar Centros"
        Me.MBtnAddCenter.Glyph = Global.Presentation.InteropCost.My.Resources.Resources.Iconos_Crystal_24x24_Usuarios___12_
        Me.MBtnAddCenter.Id = 0
        Me.MBtnAddCenter.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnAddCenter.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnAddCenter.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnAddCenter.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnAddCenter.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnAddCenter.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnAddCenter.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnAddCenter.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddCenter.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnAddCenter.Name = "MBtnAddCenter"
        '
        'MBtnRemoveCenter
        '
        Me.MBtnRemoveCenter.Caption = "Remover Centros"
        Me.MBtnRemoveCenter.Glyph = Global.Presentation.InteropCost.My.Resources.Resources.Iconos_Crystal_24x24_Usuarios___10_
        Me.MBtnRemoveCenter.Id = 1
        Me.MBtnRemoveCenter.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveCenter.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnRemoveCenter.Name = "MBtnRemoveCenter"
        '
        'INDpccAddProductionCenter
        '
        Me.INDpccAddProductionCenter.Controls.Add(Me.LayoutControl1)
        Me.INDpccAddProductionCenter.Location = New System.Drawing.Point(413, 120)
        Me.INDpccAddProductionCenter.Name = "INDpccAddProductionCenter"
        Me.INDpccAddProductionCenter.Size = New System.Drawing.Size(433, 292)
        Me.INDpccAddProductionCenter.TabIndex = 10
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDtxtCostValue)
        Me.LayoutControl1.Controls.Add(Me.INDsleMeasurementUnit)
        Me.LayoutControl1.Controls.Add(Me.INDsbAddProductionCenter)
        Me.LayoutControl1.Controls.Add(Me.INDsleProductionCenter)
        Me.LayoutControl1.Controls.Add(Me.INDtxtMaximumAmount)
        Me.LayoutControl1.Controls.Add(Me.INDSleRound)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(433, 292)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDtxtCostValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCostValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCostValue, False)
        Me.INDtxtCostValue.EditValue = ""
        Me.INDtxtCostValue.EnterMoveNextControl = True
        Me.INDtxtCostValue.Location = New System.Drawing.Point(217, 97)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCostValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCostValue.MenuManager = Me.BarManager1
        Me.INDtxtCostValue.Name = "INDtxtCostValue"
        Me.INDtxtCostValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtCostValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCostValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCostValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCostValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtCostValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtCostValue.Properties.Mask.EditMask = "c2"
        Me.INDtxtCostValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtCostValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtCostValue.Properties.ReadOnly = True
        Me.INDtxtCostValue.Size = New System.Drawing.Size(201, 28)
        Me.INDtxtCostValue.StyleController = Me.LayoutControl1
        Me.INDtxtCostValue.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCostValue, 0)
        '
        'INDsleMeasurementUnit
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleMeasurementUnit, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleMeasurementUnit, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleMeasurementUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.INDsleMeasurementUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.INDsleMeasurementUnit.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleMeasurementUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleMeasurementUnit.MenuManager = Me.BarManager1
        Me.INDsleMeasurementUnit.Name = "INDsleMeasurementUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.INDsleMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleMeasurementUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleMeasurementUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleMeasurementUnit.Properties.Appearance.Options.UseFont = True
        Me.INDsleMeasurementUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleMeasurementUnit.Properties.DisplayMember = "CodeName"
        Me.INDsleMeasurementUnit.Properties.NullText = ""
        Me.INDsleMeasurementUnit.Properties.PopupSizeable = False
        Me.INDsleMeasurementUnit.Properties.ShowFooter = False
        Me.INDsleMeasurementUnit.Properties.ValueMember = "Id"
        Me.INDsleMeasurementUnit.Properties.View = Me.viewSearchMeasurementUnit
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleMeasurementUnit, True)
        Me.INDsleMeasurementUnit.Size = New System.Drawing.Size(201, 28)
        Me.INDsleMeasurementUnit.StyleController = Me.LayoutControl1
        Me.INDsleMeasurementUnit.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleMeasurementUnit, "300")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleMeasurementUnit, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleMeasurementUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleMeasurementUnit, False)
        '
        'viewSearchMeasurementUnit
        '
        Me.viewSearchMeasurementUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSearchMeasurementUnit.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSearchMeasurementUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSearchMeasurementUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSearchMeasurementUnit.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSearchMeasurementUnit.Appearance.GroupRow.Options.UseFont = True
        Me.viewSearchMeasurementUnit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSearchMeasurementUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSearchMeasurementUnit.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSearchMeasurementUnit.Appearance.Row.Options.UseFont = True
        Me.viewSearchMeasurementUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13})
        Me.viewSearchMeasurementUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSearchMeasurementUnit.Name = "viewSearchMeasurementUnit"
        Me.viewSearchMeasurementUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSearchMeasurementUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchMeasurementUnit.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchMeasurementUnit.OptionsView.ShowAutoFilterRow = True
        Me.viewSearchMeasurementUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSearchMeasurementUnit, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Código"
        Me.GridColumn9.FieldName = "Code"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.OptionsColumn.FixedWidth = True
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Nombre"
        Me.GridColumn11.FieldName = "Name"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Abreviación"
        Me.GridColumn12.FieldName = "Abbreviation"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.OptionsColumn.FixedWidth = True
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 2
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Punto de Valor"
        Me.GridColumn13.FieldName = "CostValue"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.OptionsColumn.FixedWidth = True
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 3
        '
        'INDsbAddProductionCenter
        '
        Me.INDsbAddProductionCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddProductionCenter.Appearance.Options.UseFont = True
        Me.INDsbAddProductionCenter.Location = New System.Drawing.Point(12, 248)
        Me.INDsbAddProductionCenter.Name = "INDsbAddProductionCenter"
        Me.INDsbAddProductionCenter.Size = New System.Drawing.Size(406, 32)
        Me.INDsbAddProductionCenter.StyleController = Me.LayoutControl1
        Me.INDsbAddProductionCenter.TabIndex = 5
        Me.INDsbAddProductionCenter.Text = "Agregar"
        '
        'INDsleProductionCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleProductionCenter, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleProductionCenter, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleProductionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleProductionCenter.Name = "INDsleProductionCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleProductionCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleProductionCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleProductionCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleProductionCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleProductionCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleProductionCenter.Properties.NullText = ""
        Me.INDsleProductionCenter.Properties.PopupSizeable = False
        Me.INDsleProductionCenter.Properties.ShowFooter = False
        Me.INDsleProductionCenter.Properties.ValueMember = "Id"
        Me.INDsleProductionCenter.Properties.View = Me.GridView2
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleProductionCenter, True)
        Me.INDsleProductionCenter.Size = New System.Drawing.Size(406, 28)
        Me.INDsleProductionCenter.StyleController = Me.LayoutControl1
        Me.INDsleProductionCenter.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleProductionCenter, "1201")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleProductionCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleProductionCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleProductionCenter, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 335
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 1057
        '
        'INDtxtMaximumAmount
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtMaximumAmount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtMaximumAmount, False)
        Me.INDtxtMaximumAmount.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtMaximumAmount.EnterMoveNextControl = True
        Me.INDtxtMaximumAmount.Location = New System.Drawing.Point(12, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtMaximumAmount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtMaximumAmount.Name = "INDtxtMaximumAmount"
        Me.INDtxtMaximumAmount.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.INDtxtMaximumAmount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtxtMaximumAmount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtMaximumAmount.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtMaximumAmount.Size = New System.Drawing.Size(409, 28)
        Me.INDtxtMaximumAmount.StyleController = Me.LayoutControl1
        Me.INDtxtMaximumAmount.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtMaximumAmount, 0)
        '
        'INDSleRound
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleRound, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleRound, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleRound, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleRound, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleRound, False)
        Me.INDSleRound.EditValue = CType(0, Short)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleRound, False)
        Me.INDSleRound.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleRound, False)
        Me.INDSleRound.Location = New System.Drawing.Point(12, 216)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleRound, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleRound.MenuManager = Me.BarManager1
        Me.INDSleRound.Name = "INDSleRound"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleRound, False)
        Me.INDSleRound.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleRound.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleRound.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleRound.Properties.Appearance.Options.UseFont = True
        Me.INDSleRound.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleRound.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDSleRound.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleRound.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleRound.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleRound.Properties.DisplayMember = "Item2"
        Me.INDSleRound.Properties.NullText = ""
        Me.INDSleRound.Properties.PopupSizeable = False
        Me.INDSleRound.Properties.ShowFooter = False
        Me.INDSleRound.Properties.ValueMember = "Item1"
        Me.INDSleRound.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleRound, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleRound, True)
        Me.INDSleRound.Size = New System.Drawing.Size(409, 28)
        Me.INDSleRound.StyleController = Me.LayoutControl1
        Me.INDSleRound.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleRound, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleRound, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleRound, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleRound, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleRound, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
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
        Me.GridColumn1.Caption = "Redondeo"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliProductionCenter, Me.LayoutControlItem2, Me.INDliAmount, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.INDLCIRound})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(433, 292)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliProductionCenter
        '
        Me.INDliProductionCenter.Control = Me.INDsleProductionCenter
        Me.INDliProductionCenter.CustomizationFormText = "LayoutControlItem1"
        Me.INDliProductionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDliProductionCenter.MaxSize = New System.Drawing.Size(410, 60)
        Me.INDliProductionCenter.MinSize = New System.Drawing.Size(410, 60)
        Me.INDliProductionCenter.Name = "INDliProductionCenter"
        Me.INDliProductionCenter.Size = New System.Drawing.Size(413, 60)
        Me.INDliProductionCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliProductionCenter.Text = "Centro de Producción"
        Me.INDliProductionCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliProductionCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliProductionCenter.TextSize = New System.Drawing.Size(155, 21)
        Me.INDliProductionCenter.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsbAddProductionCenter
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 236)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(413, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDliAmount
        '
        Me.INDliAmount.Control = Me.INDtxtMaximumAmount
        Me.INDliAmount.CustomizationFormText = "Cantidad"
        Me.INDliAmount.Location = New System.Drawing.Point(0, 120)
        Me.INDliAmount.MinSize = New System.Drawing.Size(50, 25)
        Me.INDliAmount.Name = "INDliAmount"
        Me.INDliAmount.Size = New System.Drawing.Size(413, 60)
        Me.INDliAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAmount.Text = "Cantidad"
        Me.INDliAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAmount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliAmount.TextSize = New System.Drawing.Size(155, 20)
        Me.INDliAmount.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDsleMeasurementUnit
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(205, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(205, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(205, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Unidad de Medida"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(155, 21)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDtxtCostValue
        Me.LayoutControlItem5.Location = New System.Drawing.Point(205, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(205, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(205, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(208, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Punto de Valor"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(155, 20)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'INDLCIRound
        '
        Me.INDLCIRound.Control = Me.INDSleRound
        Me.INDLCIRound.Location = New System.Drawing.Point(0, 180)
        Me.INDLCIRound.Name = "INDLCIRound"
        Me.INDLCIRound.Size = New System.Drawing.Size(413, 56)
        Me.INDLCIRound.Text = "Redondeo"
        Me.INDLCIRound.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLCIRound.TextSize = New System.Drawing.Size(70, 21)
        '
        'INDpceAddDetail
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceAddDetail, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceAddDetail, Nothing)
        Me.INDpceAddDetail.Location = New System.Drawing.Point(129, 59)
        Me.INDpceAddDetail.MinimumSize = New System.Drawing.Size(726, 32)
        Me.INDpceAddDetail.Name = "INDpceAddDetail"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceAddDetail, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceAddDetail, False)
        Me.INDpceAddDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceAddDetail.Properties.Appearance.Options.UseFont = True
        Me.INDpceAddDetail.Properties.AutoHeight = False
        Me.INDpceAddDetail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceAddDetail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("INDpceAddDetail.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDpceAddDetail.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceAddDetail.Properties.PopupControl = Me.INDpccAddProductionCenter
        Me.INDpceAddDetail.Properties.PopupSizeable = False
        Me.INDpceAddDetail.Properties.ShowPopupCloseButton = False
        Me.INDpceAddDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAddDetail.Size = New System.Drawing.Size(726, 38)
        Me.INDpceAddDetail.StyleController = Me.INDlcRoot
        Me.INDpceAddDetail.TabIndex = 5
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceAddDetail, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceAddDetail, Nothing)
        '
        'INDgcDirectCostDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDirectCostDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDirectCostDetail, Nothing)
        Me.INDgcDirectCostDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDirectCostDetail, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDirectCostDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDirectCostDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDirectCostDetail, False)
        Me.INDgcDirectCostDetail.Location = New System.Drawing.Point(129, 101)
        Me.INDgcDirectCostDetail.MainView = Me.INDgvDirectCostDetail
        Me.INDgcDirectCostDetail.Name = "INDgcDirectCostDetail"
        Me.INDgcDirectCostDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrpsleProductionCenter, Me.INDrpspnValue, Me.INDrepPercentage})
        Me.INDgcDirectCostDetail.Size = New System.Drawing.Size(776, 426)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDirectCostDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDirectCostDetail.TabIndex = 6
        Me.INDgcDirectCostDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDirectCostDetail})
        '
        'INDgvDirectCostDetail
        '
        Me.INDgvDirectCostDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDirectCostDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDirectCostDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDirectCostDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDirectCostDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDirectCostDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDirectCostDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDirectCostDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDirectCostDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDirectCostDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDirectCostDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDirectCostDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvDirectCostDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDirectCostDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDirectCostDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColProductionCenter, Me.ColMeasurementUnit, Me.ColCount, Me.ColCostValue, Me.ColTotal, Me.ColPercentage})
        Me.INDgvDirectCostDetail.GridControl = Me.INDgcDirectCostDetail
        Me.INDgvDirectCostDetail.Name = "INDgvDirectCostDetail"
        Me.INDgvDirectCostDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDirectCostDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDirectCostDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDirectCostDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvDirectCostDetail.OptionsView.ShowFooter = True
        Me.INDgvDirectCostDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDirectCostDetail, False)
        '
        'ColProductionCenter
        '
        Me.ColProductionCenter.Caption = "Centro de Producción"
        Me.ColProductionCenter.FieldName = "ProductionCenterCodeName"
        Me.ColProductionCenter.Name = "ColProductionCenter"
        Me.ColProductionCenter.OptionsColumn.AllowEdit = False
        Me.ColProductionCenter.OptionsColumn.AllowFocus = False
        Me.ColProductionCenter.Visible = True
        Me.ColProductionCenter.VisibleIndex = 0
        Me.ColProductionCenter.Width = 180
        '
        'ColMeasurementUnit
        '
        Me.ColMeasurementUnit.Caption = "Unidad de Medida"
        Me.ColMeasurementUnit.FieldName = "MeasurementUnitCodeName"
        Me.ColMeasurementUnit.Name = "ColMeasurementUnit"
        Me.ColMeasurementUnit.OptionsColumn.AllowEdit = False
        Me.ColMeasurementUnit.OptionsColumn.AllowFocus = False
        Me.ColMeasurementUnit.Width = 180
        '
        'ColCount
        '
        Me.ColCount.Caption = "Cantidad"
        Me.ColCount.DisplayFormat.FormatString = "0.##"
        Me.ColCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColCount.FieldName = "Count"
        Me.ColCount.Name = "ColCount"
        Me.ColCount.OptionsColumn.AllowEdit = False
        Me.ColCount.OptionsColumn.AllowFocus = False
        Me.ColCount.OptionsColumn.FixedWidth = True
        '
        'ColCostValue
        '
        Me.ColCostValue.Caption = "Puntos de Valor"
        Me.ColCostValue.DisplayFormat.FormatString = "c2"
        Me.ColCostValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColCostValue.FieldName = "CostValue"
        Me.ColCostValue.Name = "ColCostValue"
        Me.ColCostValue.OptionsColumn.AllowEdit = False
        Me.ColCostValue.OptionsColumn.AllowFocus = False
        Me.ColCostValue.OptionsColumn.FixedWidth = True
        Me.ColCostValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CostValue", "{0:c2}")})
        '
        'ColTotal
        '
        Me.ColTotal.Caption = "Total"
        Me.ColTotal.ColumnEdit = Me.INDrpspnValue
        Me.ColTotal.DisplayFormat.FormatString = "c2"
        Me.ColTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColTotal.FieldName = "Value"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.OptionsColumn.AllowEdit = False
        Me.ColTotal.OptionsColumn.AllowFocus = False
        Me.ColTotal.OptionsColumn.FixedWidth = True
        Me.ColTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "{0:c2}")})
        Me.ColTotal.Visible = True
        Me.ColTotal.VisibleIndex = 1
        '
        'INDrpspnValue
        '
        Me.INDrpspnValue.AutoHeight = False
        Me.INDrpspnValue.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrpspnValue.Mask.EditMask = "C0"
        Me.INDrpspnValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrpspnValue.Name = "INDrpspnValue"
        Me.INDrpspnValue.ReadOnly = True
        Me.INDrpspnValue.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'ColPercentage
        '
        Me.ColPercentage.Caption = "% Distribuido"
        Me.ColPercentage.ColumnEdit = Me.INDrepPercentage
        Me.ColPercentage.FieldName = "Percentage"
        Me.ColPercentage.Name = "ColPercentage"
        Me.ColPercentage.OptionsColumn.AllowEdit = False
        Me.ColPercentage.OptionsColumn.AllowFocus = False
        Me.ColPercentage.OptionsColumn.FixedWidth = True
        Me.ColPercentage.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Percentage", "{0:0.####}%")})
        Me.ColPercentage.Visible = True
        Me.ColPercentage.VisibleIndex = 2
        '
        'INDrepPercentage
        '
        Me.INDrepPercentage.AutoHeight = False
        Me.INDrepPercentage.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepPercentage.Mask.EditMask = "P4"
        Me.INDrepPercentage.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepPercentage.Name = "INDrepPercentage"
        Me.INDrepPercentage.ReadOnly = True
        Me.INDrepPercentage.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDrpsleProductionCenter
        '
        Me.INDrpsleProductionCenter.AutoHeight = False
        Me.INDrpsleProductionCenter.DisplayMember = "CodeName"
        Me.INDrpsleProductionCenter.Name = "INDrpsleProductionCenter"
        Me.INDrpsleProductionCenter.NullText = ""
        Me.INDrpsleProductionCenter.ReadOnly = True
        Me.INDrpsleProductionCenter.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        Me.INDrpsleProductionCenter.ValueMember = "Id"
        Me.INDrpsleProductionCenter.View = Me.RepositoryItemSearchLookUpEdit1View
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Code"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 293
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Name"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 1099
        '
        'INDsleGeneralExpense
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleGeneralExpense, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleGeneralExpense, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleGeneralExpense, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleGeneralExpense, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.INDsleGeneralExpense.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.INDsleGeneralExpense.Location = New System.Drawing.Point(-285, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleGeneralExpense, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleGeneralExpense.Name = "INDsleGeneralExpense"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleGeneralExpense, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.INDsleGeneralExpense.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleGeneralExpense.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleGeneralExpense.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleGeneralExpense.Properties.Appearance.Options.UseFont = True
        Me.INDsleGeneralExpense.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleGeneralExpense.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleGeneralExpense.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleGeneralExpense.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleGeneralExpense.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleGeneralExpense.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleGeneralExpense.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleGeneralExpense.Properties.DisplayMember = "CodeName"
        Me.INDsleGeneralExpense.Properties.NullText = ""
        Me.INDsleGeneralExpense.Properties.PopupSizeable = False
        Me.INDsleGeneralExpense.Properties.ShowFooter = False
        Me.INDsleGeneralExpense.Properties.ValueMember = "Id"
        Me.INDsleGeneralExpense.Properties.View = Me.INDgvGeneralExpense
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleGeneralExpense, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleGeneralExpense, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleGeneralExpense, True)
        Me.INDsleGeneralExpense.Size = New System.Drawing.Size(386, 28)
        Me.INDsleGeneralExpense.StyleController = Me.INDlcRoot
        Me.INDsleGeneralExpense.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleGeneralExpense, "1210")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleGeneralExpense, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleGeneralExpense, "{0} - {1}")
        Me.INDsleGeneralExpense.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleGeneralExpense, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleGeneralExpense, False)
        '
        'INDgvGeneralExpense
        '
        Me.INDgvGeneralExpense.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvGeneralExpense.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvGeneralExpense.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvGeneralExpense.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvGeneralExpense.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvGeneralExpense.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvGeneralExpense.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvGeneralExpense.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvGeneralExpense.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvGeneralExpense.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvGeneralExpense.Appearance.Row.Options.UseFont = True
        Me.INDgvGeneralExpense.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDgvGeneralExpense.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvGeneralExpense.Name = "INDgvGeneralExpense"
        Me.INDgvGeneralExpense.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvGeneralExpense.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvGeneralExpense.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvGeneralExpense.OptionsView.ShowAutoFilterRow = True
        Me.INDgvGeneralExpense.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvGeneralExpense, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 309
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 1083
        '
        'INDtxtDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDescription, True)
        Me.INDtxtDescription.EnterMoveNextControl = True
        Me.INDtxtDescription.Location = New System.Drawing.Point(-285, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDescription.StyleController = Me.INDlcRoot
        Me.INDtxtDescription.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDescription, 0)
        Me.INDtxtDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(-285, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.InteropCost.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgOtherInfo})
        Me.INDlcgRoot.Location = New System.Drawing.Point(-309, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1238, 551)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliDescription, Me.LayoutControlItem3, Me.INDliGeneralExpenses, Me.INDlyItemObservation})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 531)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDliCode
        '
        Me.INDliCode.AllowHide = False
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "Código"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.ShowInCustomizationForm = False
        Me.INDliCode.Size = New System.Drawing.Size(390, 60)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCode.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDliDescription
        '
        Me.INDliDescription.AllowHide = False
        Me.INDliDescription.Control = Me.INDtxtDescription
        Me.INDliDescription.CustomizationFormText = "Descripción"
        Me.INDliDescription.Location = New System.Drawing.Point(0, 120)
        Me.INDliDescription.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliDescription.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliDescription.Name = "INDliDescription"
        Me.INDliDescription.ShowInCustomizationForm = False
        Me.INDliDescription.Size = New System.Drawing.Size(390, 60)
        Me.INDliDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDescription.Text = "Descripción"
        Me.INDliDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDescription.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliDescription.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDTxtValue
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Valor"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(96, 21)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'INDliGeneralExpenses
        '
        Me.INDliGeneralExpenses.AllowHide = False
        Me.INDliGeneralExpenses.Control = Me.INDsleGeneralExpense
        Me.INDliGeneralExpenses.CustomizationFormText = "Elemento del Costo"
        Me.INDliGeneralExpenses.Location = New System.Drawing.Point(0, 60)
        Me.INDliGeneralExpenses.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliGeneralExpenses.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliGeneralExpenses.Name = "INDliGeneralExpenses"
        Me.INDliGeneralExpenses.ShowInCustomizationForm = False
        Me.INDliGeneralExpenses.Size = New System.Drawing.Size(390, 60)
        Me.INDliGeneralExpenses.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliGeneralExpenses.Text = "Elemento del Costo"
        Me.INDliGeneralExpenses.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliGeneralExpenses.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliGeneralExpenses.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliGeneralExpenses.TextToControlDistance = 5
        '
        'INDlyItemObservation
        '
        Me.INDlyItemObservation.Control = Me.INDmemoObservation
        Me.INDlyItemObservation.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemObservation.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDlyItemObservation.MinSize = New System.Drawing.Size(390, 120)
        Me.INDlyItemObservation.Name = "INDlyItemObservation"
        Me.INDlyItemObservation.ShowInCustomizationForm = False
        Me.INDlyItemObservation.Size = New System.Drawing.Size(390, 232)
        Me.INDlyItemObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservation.Text = "Observación"
        Me.INDlyItemObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservation.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlyItemObservation.TextToControlDistance = 5
        '
        'INDlcgOtherInfo
        '
        Me.INDlcgOtherInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOtherInfo.AppearanceGroup.Options.UseFont = True
        Me.INDlcgOtherInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOtherInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgOtherInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOtherInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgOtherInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgOtherInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgOtherInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOtherInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgOtherInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOtherInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgOtherInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOtherInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgOtherInfo, False)
        Me.INDlcgOtherInfo.CustomizationFormText = "Información Detallada"
        Me.INDlcgOtherInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliDetail, Me.INDliAddDetail, Me.LayoutControlItem1})
        Me.INDlcgOtherInfo.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgOtherInfo.Name = "INDlcgOtherInfo"
        Me.INDlcgOtherInfo.Size = New System.Drawing.Size(804, 531)
        Me.INDlcgOtherInfo.Text = "Información Detallada"
        '
        'INDliDetail
        '
        Me.INDliDetail.Control = Me.INDgcDirectCostDetail
        Me.INDliDetail.CustomizationFormText = "LayoutControlItem5"
        Me.INDliDetail.Location = New System.Drawing.Point(0, 42)
        Me.INDliDetail.MaxSize = New System.Drawing.Size(780, 0)
        Me.INDliDetail.MinSize = New System.Drawing.Size(780, 24)
        Me.INDliDetail.Name = "INDliDetail"
        Me.INDliDetail.Size = New System.Drawing.Size(780, 430)
        Me.INDliDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliDetail.TextToControlDistance = 0
        Me.INDliDetail.TextVisible = False
        '
        'INDliAddDetail
        '
        Me.INDliAddDetail.Control = Me.INDpceAddDetail
        Me.INDliAddDetail.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.INDliAddDetail.CustomizationFormText = "Agregar Detalle"
        Me.INDliAddDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDliAddDetail.MaxSize = New System.Drawing.Size(730, 42)
        Me.INDliAddDetail.MinSize = New System.Drawing.Size(725, 42)
        Me.INDliAddDetail.Name = "INDliAddDetail"
        Me.INDliAddDetail.Size = New System.Drawing.Size(730, 42)
        Me.INDliAddDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddDetail.TextToControlDistance = 0
        Me.INDliAddDetail.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDebtDistribution
        Me.LayoutControlItem1.Location = New System.Drawing.Point(730, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(46, 42)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(46, 42)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(50, 42)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'PopupMenuProductionCenter
        '
        Me.PopupMenuProductionCenter.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnAddCenter), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnRemoveCenter)})
        Me.PopupMenuProductionCenter.Manager = Me.BarManager1
        Me.PopupMenuProductionCenter.Name = "PopupMenuProductionCenter"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'IndigoGridControl1
        '
        '
        'FrmDistributionDirectExpenses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1133, 695)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDistributionDirectExpenses"
        Me.Opacity = 1.0R
        Me.Tag = "1202"
        Me.Text = "Distribución Gastos Directos"
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
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDmemoObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccAddProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccAddProductionCenter.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtCostValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMeasurementUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSearchMeasurementUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleProductionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtMaximumAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleRound.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLCIRound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAddDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDirectCostDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDirectCostDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrpspnValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrpsleProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleGeneralExpense.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvGeneralExpense, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliGeneralExpenses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgOtherInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDsleGeneralExpense As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvGeneralExpense As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliGeneralExpenses As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDirectCostDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDirectCostDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgOtherInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColProductionCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceAddDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDliAddDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDpccAddProductionCenter As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleProductionCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliProductionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAddProductionCenter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliAmount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrpsleProductionCenter As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrpspnValue As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents PopupMenuProductionCenter As DevExpress.XtraBars.PopupMenu
    Friend WithEvents MBtnAddCenter As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnRemoveCenter As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents ColPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepPercentage As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDTxtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleMeasurementUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSearchMeasurementUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtCostValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColMeasurementUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCostValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtMaximumAmount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDebtDistribution As Presentation.Controls.ExportStructureButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLCIRound As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents INDSleRound As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDmemoObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObservation As DevExpress.XtraLayout.LayoutControlItem
End Class
