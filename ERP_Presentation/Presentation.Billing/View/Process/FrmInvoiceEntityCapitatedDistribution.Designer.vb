Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmInvoiceEntityCapitatedDistribution
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyInvoiceEntityCapitatedDistribution = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDdeInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleInvoiceEntityCapitatedId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvInvoiceEntityCapitatedId = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolInvoiceEntityCapitatedCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolInvoiceEntityCapitatedInvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolInvoiceEntityCapitatedCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolInvoiceEntityCapitatedValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbeCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDgcBill = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgvDetailSelectOption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDgvDetailInvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvDetailHealthAdministrator = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvDetailInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvDetailInvoiceValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvDetailInvoiceCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDbbSelection = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbUnselection = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbSeeLiquidation = New DevExpress.XtraBars.BarButtonItem()
        Me.INDsleCareGroupId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInvoiceEntityCapitatedId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCareGroupId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemBill = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDgvDetailInvoiceStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyInvoiceEntityCapitatedDistribution,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlyInvoiceEntityCapitatedDistribution.SuspendLayout
        CType(Me.INDdeEndDate.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDdeEndDate.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDdeInitialDate.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDdeInitialDate.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleInvoiceEntityCapitatedId.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvInvoiceEntityCapitatedId,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDmeObservation.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDdeDocumentDate.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDbeCode.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcBill,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvDetails,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrepCheckSelectOption,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BarManager1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleCareGroupId.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SearchLookUpEdit1View,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlygPrincipalInformation,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemDocumentDate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemObservation,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemInvoiceEntityCapitatedId,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemInitialDate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemEndDate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemCareGroupId,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlygDetails,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemBill,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLabelControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoDate1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoPopUpContainerEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoCheckEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupMenu1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyInvoiceEntityCapitatedDistribution)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1264, 579)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1264, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = true
        Me.BarraBotones.Size = New System.Drawing.Size(1264, 98)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'INDlyInvoiceEntityCapitatedDistribution
        '
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDdeEndDate)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDdeInitialDate)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDsleInvoiceEntityCapitatedId)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDmeObservation)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDdeDocumentDate)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDbeCode)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDgcBill)
        Me.INDlyInvoiceEntityCapitatedDistribution.Controls.Add(Me.INDsleCareGroupId)
        Me.INDlyInvoiceEntityCapitatedDistribution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyInvoiceEntityCapitatedDistribution.Location = New System.Drawing.Point(202, 7)
        Me.INDlyInvoiceEntityCapitatedDistribution.Name = "INDlyInvoiceEntityCapitatedDistribution"
        Me.INDlyInvoiceEntityCapitatedDistribution.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2086, 94, 572, 474)
        Me.INDlyInvoiceEntityCapitatedDistribution.Root = Me.LayoutControlGroup1
        Me.INDlyInvoiceEntityCapitatedDistribution.Size = New System.Drawing.Size(1060, 570)
        Me.INDlyInvoiceEntityCapitatedDistribution.TabIndex = 1
        Me.INDlyInvoiceEntityCapitatedDistribution.Text = "LayoutControl1"
        '
        'INDdeEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeEndDate, false)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeEndDate, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeEndDate, false)
        Me.INDdeEndDate.EditValue = Nothing
        Me.INDdeEndDate.EnterMoveNextControl = true
        Me.INDdeEndDate.Location = New System.Drawing.Point(24, 385)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeEndDate.Name = "INDdeEndDate"
        Me.INDdeEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdeEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDdeEndDate.Properties.Appearance.Options.UseBackColor = true
        Me.INDdeEndDate.Properties.Appearance.Options.UseFont = true
        Me.INDdeEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDdeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeEndDate.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDdeEndDate.Properties.ReadOnly = true
        Me.INDdeEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeEndDate.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDdeEndDate.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeEndDate, 0)
        '
        'INDdeInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeInitialDate, false)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeInitialDate, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeInitialDate, false)
        Me.INDdeInitialDate.EditValue = Nothing
        Me.INDdeInitialDate.EnterMoveNextControl = true
        Me.INDdeInitialDate.Location = New System.Drawing.Point(24, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeInitialDate.Name = "INDdeInitialDate"
        Me.INDdeInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdeInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDdeInitialDate.Properties.Appearance.Options.UseBackColor = true
        Me.INDdeInitialDate.Properties.Appearance.Options.UseFont = true
        Me.INDdeInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDdeInitialDate.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDdeInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeInitialDate.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDdeInitialDate.Properties.ReadOnly = true
        Me.INDdeInitialDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeInitialDate.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDdeInitialDate.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeInitialDate, 0)
        '
        'INDsleInvoiceEntityCapitatedId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleInvoiceEntityCapitatedId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleInvoiceEntityCapitatedId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleInvoiceEntityCapitatedId, true)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.INDsleInvoiceEntityCapitatedId.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.INDsleInvoiceEntityCapitatedId.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleInvoiceEntityCapitatedId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleInvoiceEntityCapitatedId.Name = "INDsleInvoiceEntityCapitatedId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleInvoiceEntityCapitatedId, true)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.INDsleInvoiceEntityCapitatedId.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleInvoiceEntityCapitatedId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleInvoiceEntityCapitatedId.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleInvoiceEntityCapitatedId.Properties.Appearance.Options.UseFont = true
        Me.INDsleInvoiceEntityCapitatedId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleInvoiceEntityCapitatedId.Properties.DisplayMember = "Code"
        Me.INDsleInvoiceEntityCapitatedId.Properties.NullText = ""
        Me.INDsleInvoiceEntityCapitatedId.Properties.PopupFormSize = New System.Drawing.Size(600, 0)
        Me.INDsleInvoiceEntityCapitatedId.Properties.PopupSizeable = false
        Me.INDsleInvoiceEntityCapitatedId.Properties.ShowFooter = false
        Me.INDsleInvoiceEntityCapitatedId.Properties.ValueMember = "Id"
        Me.INDsleInvoiceEntityCapitatedId.Properties.View = Me.INDgvInvoiceEntityCapitatedId
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleInvoiceEntityCapitatedId, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleInvoiceEntityCapitatedId, true)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleInvoiceEntityCapitatedId, true)
        Me.INDsleInvoiceEntityCapitatedId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleInvoiceEntityCapitatedId.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDsleInvoiceEntityCapitatedId.TabIndex = 18
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleInvoiceEntityCapitatedId, "758")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleInvoiceEntityCapitatedId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleInvoiceEntityCapitatedId, "{0} - {1}")
        Me.INDsleInvoiceEntityCapitatedId.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleInvoiceEntityCapitatedId, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleInvoiceEntityCapitatedId, false)
        '
        'INDgvInvoiceEntityCapitatedId
        '
        Me.INDgvInvoiceEntityCapitatedId.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvInvoiceEntityCapitatedId.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvInvoiceEntityCapitatedId.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvInvoiceEntityCapitatedId.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvInvoiceEntityCapitatedId.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvInvoiceEntityCapitatedId.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvInvoiceEntityCapitatedId.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvInvoiceEntityCapitatedId.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvInvoiceEntityCapitatedId.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvInvoiceEntityCapitatedId.Appearance.Row.Options.UseFont = true
        Me.INDgvInvoiceEntityCapitatedId.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolInvoiceEntityCapitatedCode, Me.INDcolInvoiceEntityCapitatedInvoiceNumber, Me.INDcolInvoiceEntityCapitatedCareGroup, Me.INDcolInvoiceEntityCapitatedValue})
        Me.INDgvInvoiceEntityCapitatedId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvInvoiceEntityCapitatedId.Name = "INDgvInvoiceEntityCapitatedId"
        Me.INDgvInvoiceEntityCapitatedId.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDgvInvoiceEntityCapitatedId.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvInvoiceEntityCapitatedId.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvInvoiceEntityCapitatedId.OptionsView.ShowAutoFilterRow = true
        Me.INDgvInvoiceEntityCapitatedId.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvInvoiceEntityCapitatedId, false)
        '
        'INDcolInvoiceEntityCapitatedCode
        '
        Me.INDcolInvoiceEntityCapitatedCode.Caption = "Código"
        Me.INDcolInvoiceEntityCapitatedCode.FieldName = "Code"
        Me.INDcolInvoiceEntityCapitatedCode.Name = "INDcolInvoiceEntityCapitatedCode"
        Me.INDcolInvoiceEntityCapitatedCode.Visible = true
        Me.INDcolInvoiceEntityCapitatedCode.VisibleIndex = 0
        '
        'INDcolInvoiceEntityCapitatedInvoiceNumber
        '
        Me.INDcolInvoiceEntityCapitatedInvoiceNumber.Caption = "Factura"
        Me.INDcolInvoiceEntityCapitatedInvoiceNumber.FieldName = "InvoiceId.InvoiceNumber"
        Me.INDcolInvoiceEntityCapitatedInvoiceNumber.Name = "INDcolInvoiceEntityCapitatedInvoiceNumber"
        Me.INDcolInvoiceEntityCapitatedInvoiceNumber.Visible = true
        Me.INDcolInvoiceEntityCapitatedInvoiceNumber.VisibleIndex = 1
        '
        'INDcolInvoiceEntityCapitatedCareGroup
        '
        Me.INDcolInvoiceEntityCapitatedCareGroup.Caption = "Grupo de Atención"
        Me.INDcolInvoiceEntityCapitatedCareGroup.FieldName = "CareGroupId.CodeName"
        Me.INDcolInvoiceEntityCapitatedCareGroup.Name = "INDcolInvoiceEntityCapitatedCareGroup"
        Me.INDcolInvoiceEntityCapitatedCareGroup.Visible = true
        Me.INDcolInvoiceEntityCapitatedCareGroup.VisibleIndex = 2
        '
        'INDcolInvoiceEntityCapitatedValue
        '
        Me.INDcolInvoiceEntityCapitatedValue.Caption = "Valor"
        Me.INDcolInvoiceEntityCapitatedValue.DisplayFormat.FormatString = "c0"
        Me.INDcolInvoiceEntityCapitatedValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolInvoiceEntityCapitatedValue.FieldName = "TotalValue"
        Me.INDcolInvoiceEntityCapitatedValue.Name = "INDcolInvoiceEntityCapitatedValue"
        Me.INDcolInvoiceEntityCapitatedValue.Visible = true
        Me.INDcolInvoiceEntityCapitatedValue.VisibleIndex = 3
        '
        'INDmeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeObservation, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeObservation, true)
        Me.INDmeObservation.EnterMoveNextControl = true
        Me.INDmeObservation.Location = New System.Drawing.Point(24, 445)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeObservation.Name = "INDmeObservation"
        Me.INDmeObservation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDmeObservation.Properties.Appearance.Options.UseBackColor = true
        Me.INDmeObservation.Properties.Appearance.Options.UseFont = true
        Me.INDmeObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDmeObservation.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDmeObservation.Properties.MaxLength = 500
        Me.INDmeObservation.Size = New System.Drawing.Size(386, 70)
        Me.INDmeObservation.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDmeObservation.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeObservation, 0)
        Me.INDmeObservation.ToolTip = "Este Campo es Necesario"
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, true)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, true)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = true
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = true
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = true
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDdeDocumentDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbeCode, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbeCode, true)
        Me.INDbeCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbeCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbeCode.Name = "INDbeCode"
        Me.INDbeCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDbeCode.Properties.Appearance.Options.UseBackColor = true
        Me.INDbeCode.Properties.Appearance.Options.UseFont = true
        Me.INDbeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDbeCode.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDbeCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Billing.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, true)})
        Me.INDbeCode.Properties.MaxLength = 20
        Me.INDbeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbeCode.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDbeCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbeCode, 0)
        Me.INDbeCode.ToolTip = "Este Campo es Necesario"
        '
        'INDgcBill
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcBill, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcBill, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcBill, true)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcBill, true)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcBill, false)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcBill, false)
        Me.INDgcBill.Location = New System.Drawing.Point(438, 59)
        Me.INDgcBill.MainView = Me.INDgvDetails
        Me.INDgcBill.MenuManager = Me.BarManager1
        Me.INDgcBill.Name = "INDgcBill"
        Me.INDgcBill.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectOption})
        Me.INDgcBill.Size = New System.Drawing.Size(598, 487)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcBill, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcBill.TabIndex = 7
        Me.INDgcBill.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetails})
        '
        'INDgvDetails
        '
        Me.INDgvDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetails.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvDetails.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvDetails.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvDetails.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetails.Appearance.Row.Options.UseFont = true
        Me.INDgvDetails.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13!)
        Me.INDgvDetails.Appearance.ViewCaption.Options.UseFont = true
        Me.INDgvDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgvDetailSelectOption, Me.INDgvDetailInvoiceNumber, Me.INDgvDetailHealthAdministrator, Me.INDgvDetailInvoiceDate, Me.INDgvDetailInvoiceValue, Me.INDgvDetailInvoiceCategory, Me.INDgvDetailInvoiceStatus})
        Me.INDgvDetails.GridControl = Me.INDgcBill
        Me.INDgvDetails.Name = "INDgvDetails"
        Me.INDgvDetails.OptionsSelection.MultiSelect = true
        Me.INDgvDetails.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvDetails.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvDetails.OptionsView.ShowAutoFilterRow = true
        Me.INDgvDetails.OptionsView.ShowDetailButtons = false
        Me.INDgvDetails.OptionsView.ShowFooter = true
        Me.INDgvDetails.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetails, false)
        '
        'INDgvDetailSelectOption
        '
        Me.INDgvDetailSelectOption.Caption = "Sel."
        Me.INDgvDetailSelectOption.ColumnEdit = Me.INDrepCheckSelectOption
        Me.INDgvDetailSelectOption.FieldName = "SelectOption"
        Me.INDgvDetailSelectOption.Image = Global.Presentation.Billing.My.Resources.Resources.undcheck
        Me.INDgvDetailSelectOption.ImageAlignment = System.Drawing.StringAlignment.Center
        Me.INDgvDetailSelectOption.Name = "INDgvDetailSelectOption"
        Me.INDgvDetailSelectOption.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgvDetailSelectOption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgvDetailSelectOption.OptionsColumn.AllowMove = false
        Me.INDgvDetailSelectOption.OptionsColumn.AllowShowHide = false
        Me.INDgvDetailSelectOption.OptionsColumn.AllowSize = false
        Me.INDgvDetailSelectOption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgvDetailSelectOption.OptionsColumn.FixedWidth = true
        Me.INDgvDetailSelectOption.OptionsFilter.AllowAutoFilter = false
        Me.INDgvDetailSelectOption.OptionsFilter.AllowFilter = false
        Me.INDgvDetailSelectOption.Visible = true
        Me.INDgvDetailSelectOption.VisibleIndex = 0
        Me.INDgvDetailSelectOption.Width = 32
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = false
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'INDgvDetailInvoiceNumber
        '
        Me.INDgvDetailInvoiceNumber.Caption = "Control Servicio"
        Me.INDgvDetailInvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDgvDetailInvoiceNumber.Name = "INDgvDetailInvoiceNumber"
        Me.INDgvDetailInvoiceNumber.Visible = true
        Me.INDgvDetailInvoiceNumber.VisibleIndex = 1
        Me.INDgvDetailInvoiceNumber.Width = 77
        '
        'INDgvDetailHealthAdministrator
        '
        Me.INDgvDetailHealthAdministrator.Caption = "Entidad"
        Me.INDgvDetailHealthAdministrator.FieldName = "HealthAdministratorName"
        Me.INDgvDetailHealthAdministrator.Name = "INDgvDetailHealthAdministrator"
        Me.INDgvDetailHealthAdministrator.Visible = true
        Me.INDgvDetailHealthAdministrator.VisibleIndex = 2
        Me.INDgvDetailHealthAdministrator.Width = 112
        '
        'INDgvDetailInvoiceDate
        '
        Me.INDgvDetailInvoiceDate.Caption = "Fecha"
        Me.INDgvDetailInvoiceDate.FieldName = "InvoiceDate"
        Me.INDgvDetailInvoiceDate.Name = "INDgvDetailInvoiceDate"
        Me.INDgvDetailInvoiceDate.Visible = true
        Me.INDgvDetailInvoiceDate.VisibleIndex = 3
        Me.INDgvDetailInvoiceDate.Width = 112
        '
        'INDgvDetailInvoiceValue
        '
        Me.INDgvDetailInvoiceValue.Caption = "Valor"
        Me.INDgvDetailInvoiceValue.DisplayFormat.FormatString = "c0"
        Me.INDgvDetailInvoiceValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDgvDetailInvoiceValue.FieldName = "InvoiceValue"
        Me.INDgvDetailInvoiceValue.Name = "INDgvDetailInvoiceValue"
        Me.INDgvDetailInvoiceValue.Visible = true
        Me.INDgvDetailInvoiceValue.VisibleIndex = 4
        Me.INDgvDetailInvoiceValue.Width = 112
        '
        'INDgvDetailInvoiceCategory
        '
        Me.INDgvDetailInvoiceCategory.Caption = "Categoría"
        Me.INDgvDetailInvoiceCategory.FieldName = "InvoiceCategoryName"
        Me.INDgvDetailInvoiceCategory.Name = "INDgvDetailInvoiceCategory"
        Me.INDgvDetailInvoiceCategory.Visible = true
        Me.INDgvDetailInvoiceCategory.VisibleIndex = 5
        Me.INDgvDetailInvoiceCategory.Width = 135
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbbSelection, Me.INDbbUnselection, Me.INDbbSeeLiquidation})
        Me.BarManager1.MaxItemId = 7
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = false
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Size = New System.Drawing.Size(1264, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = false
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 701)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1264, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = false
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 696)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = false
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1264, 5)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 696)
        '
        'INDbbSelection
        '
        Me.INDbbSelection.Caption = "Seleccionar"
        Me.INDbbSelection.Id = 0
        Me.INDbbSelection.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSelection.ItemAppearance.Disabled.Options.UseFont = true
        Me.INDbbSelection.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSelection.ItemAppearance.Hovered.Options.UseFont = true
        Me.INDbbSelection.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSelection.ItemAppearance.Normal.Options.UseFont = true
        Me.INDbbSelection.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSelection.ItemAppearance.Pressed.Options.UseFont = true
        Me.INDbbSelection.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSelection.ItemInMenuAppearance.Disabled.Options.UseFont = true
        Me.INDbbSelection.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSelection.ItemInMenuAppearance.Hovered.Options.UseFont = true
        Me.INDbbSelection.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSelection.ItemInMenuAppearance.Normal.Options.UseFont = true
        Me.INDbbSelection.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSelection.ItemInMenuAppearance.Pressed.Options.UseFont = true
        Me.INDbbSelection.Name = "INDbbSelection"
        '
        'INDbbUnselection
        '
        Me.INDbbUnselection.Caption = "Deseleccionar"
        Me.INDbbUnselection.Id = 1
        Me.INDbbUnselection.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbUnselection.ItemAppearance.Disabled.Options.UseFont = true
        Me.INDbbUnselection.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbUnselection.ItemAppearance.Hovered.Options.UseFont = true
        Me.INDbbUnselection.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbUnselection.ItemAppearance.Normal.Options.UseFont = true
        Me.INDbbUnselection.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbUnselection.ItemAppearance.Pressed.Options.UseFont = true
        Me.INDbbUnselection.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbUnselection.ItemInMenuAppearance.Disabled.Options.UseFont = true
        Me.INDbbUnselection.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbUnselection.ItemInMenuAppearance.Hovered.Options.UseFont = true
        Me.INDbbUnselection.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbUnselection.ItemInMenuAppearance.Normal.Options.UseFont = true
        Me.INDbbUnselection.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbUnselection.ItemInMenuAppearance.Pressed.Options.UseFont = true
        Me.INDbbUnselection.Name = "INDbbUnselection"
        '
        'INDbbSeeLiquidation
        '
        Me.INDbbSeeLiquidation.Caption = "Ver Liquidación"
        Me.INDbbSeeLiquidation.Id = 6
        Me.INDbbSeeLiquidation.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSeeLiquidation.ItemAppearance.Disabled.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSeeLiquidation.ItemAppearance.Hovered.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSeeLiquidation.ItemAppearance.Normal.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDbbSeeLiquidation.ItemAppearance.Pressed.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Disabled.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Hovered.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Normal.Options.UseFont = true
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbbSeeLiquidation.ItemInMenuAppearance.Pressed.Options.UseFont = true
        Me.INDbbSeeLiquidation.Name = "INDbbSeeLiquidation"
        '
        'INDsleCareGroupId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCareGroupId, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCareGroupId, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCareGroupId, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCareGroupId, false)
        Me.INDsleCareGroupId.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCareGroupId, false)
        Me.INDsleCareGroupId.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCareGroupId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCareGroupId.Name = "INDsleCareGroupId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCareGroupId, true)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCareGroupId, false)
        Me.INDsleCareGroupId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCareGroupId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleCareGroupId.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleCareGroupId.Properties.Appearance.Options.UseFont = true
        Me.INDsleCareGroupId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCareGroupId.Properties.NullText = ""
        Me.INDsleCareGroupId.Properties.PopupSizeable = false
        Me.INDsleCareGroupId.Properties.ReadOnly = true
        Me.INDsleCareGroupId.Properties.ShowFooter = false
        Me.INDsleCareGroupId.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCareGroupId, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCareGroupId, true)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCareGroupId, true)
        Me.INDsleCareGroupId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCareGroupId.StyleController = Me.INDlyInvoiceEntityCapitatedDistribution
        Me.INDsleCareGroupId.TabIndex = 21
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCareGroupId, "985")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCareGroupId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCareGroupId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCareGroupId, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCareGroupId, false)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = true
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = true
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = true
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = true
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = true
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, false)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation, Me.INDlygDetails})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1060, 570)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'INDlygPrincipalInformation
        '
        Me.INDlygPrincipalInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlygPrincipalInformation.AppearanceGroup.Options.UseFont = true
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Options.UseFont = true
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalInformation, false)
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDocumentDate, Me.INDlyItemObservation, Me.INDlyItemInvoiceEntityCapitatedId, Me.INDlyItemInitialDate, Me.INDlyItemEndDate, Me.INDlyItemCareGroupId})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(414, 550)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbeCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = false
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
        Me.INDlyItemDocumentDate.AllowHide = false
        Me.INDlyItemDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDlyItemDocumentDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemDocumentDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.Name = "INDlyItemDocumentDate"
        Me.INDlyItemDocumentDate.ShowInCustomizationForm = false
        Me.INDlyItemDocumentDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocumentDate.Text = "Fecha Documento"
        Me.INDlyItemDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDocumentDate.TextToControlDistance = 5
        '
        'INDlyItemObservation
        '
        Me.INDlyItemObservation.AllowHide = false
        Me.INDlyItemObservation.Control = Me.INDmeObservation
        Me.INDlyItemObservation.Location = New System.Drawing.Point(0, 360)
        Me.INDlyItemObservation.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemObservation.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemObservation.Name = "INDlyItemObservation"
        Me.INDlyItemObservation.ShowInCustomizationForm = false
        Me.INDlyItemObservation.Size = New System.Drawing.Size(390, 131)
        Me.INDlyItemObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservation.Text = "Descripción"
        Me.INDlyItemObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemObservation.TextToControlDistance = 5
        '
        'INDlyItemInvoiceEntityCapitatedId
        '
        Me.INDlyItemInvoiceEntityCapitatedId.AllowHide = false
        Me.INDlyItemInvoiceEntityCapitatedId.Control = Me.INDsleInvoiceEntityCapitatedId
        Me.INDlyItemInvoiceEntityCapitatedId.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemInvoiceEntityCapitatedId.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInvoiceEntityCapitatedId.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInvoiceEntityCapitatedId.Name = "INDlyItemInvoiceEntityCapitatedId"
        Me.INDlyItemInvoiceEntityCapitatedId.ShowInCustomizationForm = false
        Me.INDlyItemInvoiceEntityCapitatedId.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemInvoiceEntityCapitatedId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInvoiceEntityCapitatedId.Text = "Factura Monto Fijo"
        Me.INDlyItemInvoiceEntityCapitatedId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInvoiceEntityCapitatedId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInvoiceEntityCapitatedId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInvoiceEntityCapitatedId.TextToControlDistance = 5
        '
        'INDlyItemInitialDate
        '
        Me.INDlyItemInitialDate.Control = Me.INDdeInitialDate
        Me.INDlyItemInitialDate.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemInitialDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialDate.Name = "INDlyItemInitialDate"
        Me.INDlyItemInitialDate.ShowInCustomizationForm = false
        Me.INDlyItemInitialDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialDate.Text = "Fecha Inicial"
        Me.INDlyItemInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInitialDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInitialDate.TextToControlDistance = 5
        Me.INDlyItemInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemEndDate
        '
        Me.INDlyItemEndDate.Control = Me.INDdeEndDate
        Me.INDlyItemEndDate.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemEndDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndDate.Name = "INDlyItemEndDate"
        Me.INDlyItemEndDate.ShowInCustomizationForm = false
        Me.INDlyItemEndDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndDate.Text = "Fecha Final"
        Me.INDlyItemEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEndDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEndDate.TextToControlDistance = 5
        Me.INDlyItemEndDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemCareGroupId
        '
        Me.INDlyItemCareGroupId.Control = Me.INDsleCareGroupId
        Me.INDlyItemCareGroupId.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemCareGroupId.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCareGroupId.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCareGroupId.Name = "INDlyItemCareGroupId"
        Me.INDlyItemCareGroupId.ShowInCustomizationForm = false
        Me.INDlyItemCareGroupId.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCareGroupId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCareGroupId.Text = "Grupo de Atención"
        Me.INDlyItemCareGroupId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCareGroupId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCareGroupId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCareGroupId.TextToControlDistance = 5
        Me.INDlyItemCareGroupId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlygDetails
        '
        Me.INDlygDetails.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlygDetails.AppearanceGroup.Options.UseFont = true
        Me.INDlygDetails.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlygDetails.AppearanceItemCaption.Options.UseFont = true
        Me.INDlygDetails.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygDetails.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDlygDetails.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlygDetails.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDetails, false)
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemBill})
        Me.INDlygDetails.Location = New System.Drawing.Point(414, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(626, 550)
        Me.INDlygDetails.Text = "Detalles"
        '
        'INDlyItemBill
        '
        Me.INDlyItemBill.Control = Me.INDgcBill
        Me.INDlyItemBill.CustomizationFormText = "Listado de Facturas"
        Me.INDlyItemBill.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemBill.Name = "INDlyItemBill"
        Me.INDlyItemBill.Size = New System.Drawing.Size(602, 491)
        Me.INDlyItemBill.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemBill.TextVisible = false
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbSelection), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbUnselection), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbSeeLiquidation)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'INDgvDetailInvoiceStatus
        '
        Me.INDgvDetailInvoiceStatus.Caption = "Estado"
        Me.INDgvDetailInvoiceStatus.FieldName = "StatusName"
        Me.INDgvDetailInvoiceStatus.Name = "INDgvDetailInvoiceStatus"
        Me.INDgvDetailInvoiceStatus.Visible = true
        Me.INDgvDetailInvoiceStatus.VisibleIndex = 6
        '
        'FrmInvoiceEntityCapitatedDistribution
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 701)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmInvoiceEntityCapitatedDistribution"
        Me.Opacity = 1R
        Me.Tag = "2057"
        Me.Text = "Distribución Ingresos Monto Fijo"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyInvoiceEntityCapitatedDistribution,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlyInvoiceEntityCapitatedDistribution.ResumeLayout(false)
        CType(Me.INDdeEndDate.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDdeEndDate.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDdeInitialDate.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDdeInitialDate.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleInvoiceEntityCapitatedId.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvInvoiceEntityCapitatedId,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDmeObservation.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDdeDocumentDate.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDbeCode.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcBill,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvDetails,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrepCheckSelectOption,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BarManager1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleCareGroupId.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SearchLookUpEdit1View,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlygPrincipalInformation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemDocumentDate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemObservation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemInvoiceEntityCapitatedId,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemInitialDate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemEndDate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemCareGroupId,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlygDetails,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemBill,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLabelControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoDate1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoPopUpContainerEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupMenu1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyInvoiceEntityCapitatedDistribution As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcBill As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemBill As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoCheckEdit1 As Presentation.Controls.IndigoCheckEdit
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDbbSelection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbbUnselection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbeCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleInvoiceEntityCapitatedId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvInvoiceEntityCapitatedId As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemInvoiceEntityCapitatedId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolInvoiceEntityCapitatedCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolInvoiceEntityCapitatedInvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolInvoiceEntityCapitatedValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolInvoiceEntityCapitatedCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDdeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDdeInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCareGroupId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCareGroupId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgvDetailInvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvDetailHealthAdministrator As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvDetailInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvDetailInvoiceValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvDetailInvoiceCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvDetailSelectOption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbbSeeLiquidation As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDgvDetailInvoiceStatus As DevExpress.XtraGrid.Columns.GridColumn
End Class
