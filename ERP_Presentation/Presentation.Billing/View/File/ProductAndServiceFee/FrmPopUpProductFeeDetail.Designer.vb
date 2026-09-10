Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopUpProductFeeDetail
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
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTextObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtSalesValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleFinalDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsePercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsleRateType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDslePercentageType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRateType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPercentageType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygContract = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciSalesValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDTextObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSalesValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFinalDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFinalDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsePercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleRateType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePercentageType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRateType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPercentageType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSalesValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1108, 495)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1108, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1108, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDTextObservations)
        Me.INDlcRoot.Controls.Add(Me.INDtxtSalesValue)
        Me.INDlcRoot.Controls.Add(Me.INDsleFinalDate)
        Me.INDlcRoot.Controls.Add(Me.INDsleInitialDate)
        Me.INDlcRoot.Controls.Add(Me.INDsleProduct)
        Me.INDlcRoot.Controls.Add(Me.INDsePercentage)
        Me.INDlcRoot.Controls.Add(Me.INDsleRateType)
        Me.INDlcRoot.Controls.Add(Me.INDslePercentageType)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(904, 446)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDTextObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTextObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTextObservations, False)
        Me.INDTextObservations.EnterMoveNextControl = True
        Me.INDTextObservations.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDTextObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTextObservations.Name = "INDTextObservations"
        Me.INDTextObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTextObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDTextObservations.Properties.Appearance.Options.UseFont = True
        Me.INDTextObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTextObservations.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTextObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTextObservations.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTextObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTextObservations.Properties.MaxLength = 100
        Me.INDTextObservations.Size = New System.Drawing.Size(386, 90)
        Me.INDTextObservations.StyleController = Me.INDlcRoot
        Me.INDTextObservations.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTextObservations, 0)
        '
        'INDtxtSalesValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSalesValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSalesValue, False)
        Me.INDtxtSalesValue.EnterMoveNextControl = True
        Me.INDtxtSalesValue.Location = New System.Drawing.Point(438, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSalesValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtSalesValue.Name = "INDtxtSalesValue"
        Me.INDtxtSalesValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSalesValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSalesValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSalesValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSalesValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSalesValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSalesValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtSalesValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtSalesValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSalesValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSalesValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSalesValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSalesValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtSalesValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSalesValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSalesValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSalesValue.StyleController = Me.INDlcRoot
        Me.INDtxtSalesValue.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSalesValue, 0)
        '
        'INDsleFinalDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFinalDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDsleFinalDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFinalDate, False)
        Me.INDsleFinalDate.EditValue = Nothing
        Me.INDsleFinalDate.EnterMoveNextControl = True
        Me.INDsleFinalDate.Location = New System.Drawing.Point(438, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFinalDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDsleFinalDate, Presentation.Controls.IndigoDate.EMask.FechaHora)
        Me.INDsleFinalDate.Name = "INDsleFinalDate"
        Me.INDsleFinalDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleFinalDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFinalDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFinalDate.Properties.Appearance.Options.UseFont = True
        Me.INDsleFinalDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleFinalDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleFinalDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFinalDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleFinalDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleFinalDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFinalDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFinalDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFinalDate.Properties.Mask.EditMask = "dd/MM/yyyy hh:mm tt"
        Me.INDsleFinalDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDsleFinalDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDsleFinalDate.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFinalDate.StyleController = Me.INDlcRoot
        Me.INDsleFinalDate.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFinalDate, 0)
        '
        'INDsleInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleInitialDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDsleInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleInitialDate, False)
        Me.INDsleInitialDate.EditValue = Nothing
        Me.INDsleInitialDate.EnterMoveNextControl = True
        Me.INDsleInitialDate.Location = New System.Drawing.Point(438, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDsleInitialDate, Presentation.Controls.IndigoDate.EMask.FechaHora)
        Me.INDsleInitialDate.Name = "INDsleInitialDate"
        Me.INDsleInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDsleInitialDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleInitialDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleInitialDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleInitialDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleInitialDate.Properties.Mask.EditMask = "dd/MM/yyyy hh:mm tt"
        Me.INDsleInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDsleInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDsleInitialDate.Size = New System.Drawing.Size(386, 28)
        Me.INDsleInitialDate.StyleController = Me.INDlcRoot
        Me.INDsleInitialDate.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleInitialDate, 0)
        '
        'INDsleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleProduct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleProduct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleProduct, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleProduct, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleProduct, False)
        Me.INDsleProduct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleProduct, False)
        Me.INDsleProduct.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleProduct.Name = "INDsleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleProduct, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleProduct, False)
        Me.INDsleProduct.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleProduct.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDsleProduct.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleProduct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleProduct.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleProduct.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleProduct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleProduct.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleProduct.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleProduct.Properties.DisplayMember = "CodeName"
        Me.INDsleProduct.Properties.NullText = ""
        Me.INDsleProduct.Properties.PopupSizeable = False
        Me.INDsleProduct.Properties.PopupView = Me.GridView2
        Me.INDsleProduct.Properties.ShowFooter = False
        Me.INDsleProduct.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleProduct, True)
        Me.INDsleProduct.Size = New System.Drawing.Size(386, 28)
        Me.INDsleProduct.StyleController = Me.INDlcRoot
        Me.INDsleProduct.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleProduct, "304")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleProduct, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleProduct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleProduct, False)
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
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 238
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 374
        '
        'INDsePercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsePercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsePercentage, False)
        Me.INDsePercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDsePercentage.EnterMoveNextControl = True
        Me.INDsePercentage.Location = New System.Drawing.Point(438, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDsePercentage, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsePercentage.Name = "INDsePercentage"
        Me.INDsePercentage.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDsePercentage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsePercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsePercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDsePercentage.Properties.Appearance.Options.UseFont = True
        Me.INDsePercentage.Properties.Appearance.Options.UseTextOptions = True
        Me.INDsePercentage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDsePercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsePercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsePercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsePercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsePercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsePercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsePercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsePercentage.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDsePercentage.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDsePercentage.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.INDsePercentage.Properties.Mask.EditMask = "####.##%%"
        Me.INDsePercentage.Properties.Mask.IgnoreMaskBlank = False
        Me.INDsePercentage.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(48)
        Me.INDsePercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDsePercentage.Properties.MaxValue = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.INDsePercentage.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.INDsePercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDsePercentage.StyleController = Me.INDlcRoot
        Me.INDsePercentage.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsePercentage, 0)
        '
        'INDsleRateType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleRateType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleRateType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleRateType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleRateType, False)
        Me.INDsleRateType.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleRateType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleRateType.MaximumSize = New System.Drawing.Size(386, 28)
        Me.INDsleRateType.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDsleRateType.Name = "INDsleRateType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleRateType, False)
        Me.INDsleRateType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleRateType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleRateType.Properties.Appearance.Options.UseFont = True
        Me.INDsleRateType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleRateType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleRateType.Properties.DisplayMember = "Item2"
        Me.INDsleRateType.Properties.NullText = ""
        Me.INDsleRateType.Properties.PopupSizeable = False
        Me.INDsleRateType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleRateType.Properties.ShowFooter = False
        Me.INDsleRateType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleRateType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleRateType, True)
        Me.INDsleRateType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleRateType.StyleController = Me.INDlcRoot
        Me.INDsleRateType.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleRateType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleRateType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleRateType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleRateType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleRateType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Tipo"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDslePercentageType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePercentageType, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePercentageType, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDslePercentageType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePercentageType, False)
        Me.INDslePercentageType.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePercentageType, False)
        Me.INDslePercentageType.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePercentageType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePercentageType.MaximumSize = New System.Drawing.Size(386, 28)
        Me.INDslePercentageType.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDslePercentageType.Name = "INDslePercentageType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePercentageType, False)
        Me.INDslePercentageType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDslePercentageType.Properties.Appearance.Options.UseFont = True
        Me.INDslePercentageType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslePercentageType.Properties.DisplayMember = "Item2"
        Me.INDslePercentageType.Properties.NullText = ""
        Me.INDslePercentageType.Properties.PopupSizeable = False
        Me.INDslePercentageType.Properties.PopupView = Me.GridView1
        Me.INDslePercentageType.Properties.ShowFooter = False
        Me.INDslePercentageType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePercentageType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePercentageType, True)
        Me.INDslePercentageType.Size = New System.Drawing.Size(386, 28)
        Me.INDslePercentageType.StyleController = Me.INDlcRoot
        Me.INDslePercentageType.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePercentageType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePercentageType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePercentageType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePercentageType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePercentageType, False)
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tipo"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDlcgRoot.CustomizationFormText = "Root"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygContract})
        Me.INDlcgRoot.Name = "Root"
        Me.INDlcgRoot.Size = New System.Drawing.Size(904, 446)
        Me.INDlcgRoot.TextVisible = False
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
        Me.INDlygPrincipalData.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlygPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciProduct, Me.INDLciRateType, Me.INDLciPercentageType, Me.INDlyItemObservations})
        Me.INDlygPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalData.Name = "INDlygPrincipalData"
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 426)
        Me.INDlygPrincipalData.Text = "Datos Principales"
        '
        'INDlciProduct
        '
        Me.INDlciProduct.AllowHide = False
        Me.INDlciProduct.Control = Me.INDsleProduct
        Me.INDlciProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDlciProduct.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciProduct.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciProduct.Name = "INDlciProduct"
        Me.INDlciProduct.ShowInCustomizationForm = False
        Me.INDlciProduct.Size = New System.Drawing.Size(390, 60)
        Me.INDlciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciProduct.Text = "Producto"
        Me.INDlciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciProduct.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciProduct.TextToControlDistance = 5
        '
        'INDLciRateType
        '
        Me.INDLciRateType.Control = Me.INDsleRateType
        Me.INDLciRateType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciRateType.CustomizationFormText = "Tipo de Tarifa"
        Me.INDLciRateType.Location = New System.Drawing.Point(0, 60)
        Me.INDLciRateType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciRateType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciRateType.Name = "INDLciRateType"
        Me.INDLciRateType.Size = New System.Drawing.Size(390, 60)
        Me.INDLciRateType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRateType.Text = "Tipo de Tarifa"
        Me.INDLciRateType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRateType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRateType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRateType.TextToControlDistance = 5
        '
        'INDLciPercentageType
        '
        Me.INDLciPercentageType.Control = Me.INDslePercentageType
        Me.INDLciPercentageType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPercentageType.CustomizationFormText = "Tipo Porcentaje"
        Me.INDLciPercentageType.Location = New System.Drawing.Point(0, 120)
        Me.INDLciPercentageType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPercentageType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPercentageType.Name = "INDLciPercentageType"
        Me.INDLciPercentageType.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPercentageType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPercentageType.Text = "Tipo Porcentaje"
        Me.INDLciPercentageType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPercentageType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPercentageType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciPercentageType.TextToControlDistance = 5
        Me.INDLciPercentageType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemObservations
        '
        Me.INDlyItemObservations.AllowHide = False
        Me.INDlyItemObservations.Control = Me.INDTextObservations
        Me.INDlyItemObservations.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemObservations.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDlyItemObservations.MinSize = New System.Drawing.Size(390, 120)
        Me.INDlyItemObservations.Name = "INDlyItemObservations"
        Me.INDlyItemObservations.ShowInCustomizationForm = False
        Me.INDlyItemObservations.Size = New System.Drawing.Size(390, 193)
        Me.INDlyItemObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservations.Text = "Observaciones"
        Me.INDlyItemObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemObservations.TextToControlDistance = 5
        '
        'INDlygContract
        '
        Me.INDlygContract.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygContract.AppearanceGroup.Options.UseFont = True
        Me.INDlygContract.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygContract.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygContract.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygContract.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygContract.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygContract.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygContract.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygContract.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygContract.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygContract.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygContract.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygContract.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygContract, False)
        Me.INDlygContract.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciInitialDate, Me.INDlciEndDate, Me.INDlciSalesValue, Me.INDlciPercentage})
        Me.INDlygContract.Location = New System.Drawing.Point(414, 0)
        Me.INDlygContract.Name = "INDlygContract"
        Me.INDlygContract.Size = New System.Drawing.Size(470, 426)
        Me.INDlygContract.Text = "Contractual"
        '
        'INDlciInitialDate
        '
        Me.INDlciInitialDate.AllowHide = False
        Me.INDlciInitialDate.Control = Me.INDsleInitialDate
        Me.INDlciInitialDate.Location = New System.Drawing.Point(0, 0)
        Me.INDlciInitialDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciInitialDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciInitialDate.Name = "INDlciInitialDate"
        Me.INDlciInitialDate.ShowInCustomizationForm = False
        Me.INDlciInitialDate.Size = New System.Drawing.Size(446, 60)
        Me.INDlciInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciInitialDate.Text = "Fecha Inicial"
        Me.INDlciInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciInitialDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciInitialDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciInitialDate.TextToControlDistance = 5
        '
        'INDlciEndDate
        '
        Me.INDlciEndDate.AllowHide = False
        Me.INDlciEndDate.Control = Me.INDsleFinalDate
        Me.INDlciEndDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlciEndDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciEndDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciEndDate.Name = "INDlciEndDate"
        Me.INDlciEndDate.ShowInCustomizationForm = False
        Me.INDlciEndDate.Size = New System.Drawing.Size(446, 60)
        Me.INDlciEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEndDate.Text = "Fecha Final"
        Me.INDlciEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciEndDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEndDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciEndDate.TextToControlDistance = 5
        '
        'INDlciSalesValue
        '
        Me.INDlciSalesValue.Control = Me.INDtxtSalesValue
        Me.INDlciSalesValue.Location = New System.Drawing.Point(0, 180)
        Me.INDlciSalesValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciSalesValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciSalesValue.Name = "INDlciSalesValue"
        Me.INDlciSalesValue.Size = New System.Drawing.Size(446, 193)
        Me.INDlciSalesValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciSalesValue.Text = "Precio de Venta"
        Me.INDlciSalesValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciSalesValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciSalesValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciSalesValue.TextToControlDistance = 5
        Me.INDlciSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciPercentage
        '
        Me.INDlciPercentage.Control = Me.INDsePercentage
        Me.INDlciPercentage.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciPercentage.CustomizationFormText = "Porcentaje"
        Me.INDlciPercentage.Location = New System.Drawing.Point(0, 120)
        Me.INDlciPercentage.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciPercentage.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciPercentage.Name = "INDlciPercentage"
        Me.INDlciPercentage.Size = New System.Drawing.Size(446, 60)
        Me.INDlciPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPercentage.Text = "Porcentaje"
        Me.INDlciPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciPercentage.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciPercentage.TextToControlDistance = 5
        Me.INDlciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbtnAdd.Appearance.Options.UseFont = True
        Me.INDbtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(900, 36)
        Me.INDbtnAdd.TabIndex = 0
        Me.INDbtnAdd.Text = "Agregar"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 486)
        Me.CtrNavigationControl1.TabIndex = 1
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAdd)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(202, 453)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(904, 40)
        Me.INDpanelButtons.TabIndex = 2
        '
        'FrmPopUpProductFeeDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 630)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpProductFeeDetail"
        Me.Opacity = 1.0R
        Me.Tag = "306"
        Me.Text = "Tarifa de Productos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDTextObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSalesValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFinalDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFinalDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsePercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleRateType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePercentageType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRateType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPercentageType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSalesValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDtxtSalesValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleFinalDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDsleInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDsleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciSalesValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTextObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlygContract As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciRateType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPercentageType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsePercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleRateType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDslePercentageType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
