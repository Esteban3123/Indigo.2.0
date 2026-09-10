Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmModifyAmortization
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyModifyAmortization = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcAmortizations = New DevExpress.XtraGrid.GridControl()
        Me.viewAmortizations = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDValueCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDsleAccountPayable = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAccountPayable = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAmortizations = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAmortizations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyModifyAmortization, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyModifyAmortization.SuspendLayout()
        CType(Me.INDgcAmortizations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewAmortizations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountPayable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAmortizations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAmortizations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyModifyAmortization)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1326, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1326, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1326, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyModifyAmortization
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyModifyAmortization
        '
        Me.INDlyModifyAmortization.AllowCustomization = False
        Me.INDlyModifyAmortization.Controls.Add(Me.INDgcAmortizations)
        Me.INDlyModifyAmortization.Controls.Add(Me.INDsleAccountPayable)
        Me.INDlyModifyAmortization.Controls.Add(Me.INDsleSupplier)
        Me.INDlyModifyAmortization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyModifyAmortization, False)
        Me.INDlyModifyAmortization.Location = New System.Drawing.Point(202, 7)
        Me.INDlyModifyAmortization.Name = "INDlyModifyAmortization"
        Me.INDlyModifyAmortization.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(798, 282, 574, 569)
        Me.INDlyModifyAmortization.Root = Me.LayoutControlGroup1
        Me.INDlyModifyAmortization.Size = New System.Drawing.Size(1122, 557)
        Me.INDlyModifyAmortization.TabIndex = 1
        Me.INDlyModifyAmortization.Text = "LayoutControl1"
        '
        'INDgcAmortizations
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAmortizations, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAmortizations, Nothing)
        Me.INDgcAmortizations.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAmortizations, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAmortizations, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAmortizations, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAmortizations, False)
        Me.INDgcAmortizations.Location = New System.Drawing.Point(438, 53)
        Me.INDgcAmortizations.MainView = Me.viewAmortizations
        Me.INDgcAmortizations.Name = "INDgcAmortizations"
        Me.INDgcAmortizations.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue})
        Me.INDgcAmortizations.Size = New System.Drawing.Size(824, 463)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAmortizations, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcAmortizations.TabIndex = 12
        Me.INDgcAmortizations.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewAmortizations})
        '
        'viewAmortizations
        '
        Me.viewAmortizations.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewAmortizations.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewAmortizations.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewAmortizations.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewAmortizations.Appearance.FocusedRow.Options.UseFont = True
        Me.viewAmortizations.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewAmortizations.Appearance.GroupRow.Options.UseFont = True
        Me.viewAmortizations.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewAmortizations.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewAmortizations.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewAmortizations.Appearance.Row.Options.UseFont = True
        Me.viewAmortizations.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewAmortizations.Appearance.ViewCaption.Options.UseFont = True
        Me.viewAmortizations.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn12, Me.GridColumn10, Me.GridColumn11, Me.INDValueCol})
        Me.viewAmortizations.GridControl = Me.INDgcAmortizations
        Me.viewAmortizations.GroupCount = 2
        Me.viewAmortizations.Name = "viewAmortizations"
        Me.viewAmortizations.OptionsBehavior.AutoExpandAllGroups = True
        Me.viewAmortizations.OptionsView.EnableAppearanceEvenRow = True
        Me.viewAmortizations.OptionsView.EnableAppearanceOddRow = True
        Me.viewAmortizations.OptionsView.ShowAutoFilterRow = True
        Me.viewAmortizations.OptionsView.ShowDetailButtons = False
        Me.viewAmortizations.OptionsView.ShowGroupPanel = False
        Me.viewAmortizations.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn3, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn12, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewAmortizations, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Año"
        Me.GridColumn3.FieldName = "PaymentYear"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Mes"
        Me.GridColumn12.FieldName = "PaymentMonth"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Cuenta Contable"
        Me.GridColumn10.FieldName = "MainAccountDescription"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        Me.GridColumn10.Width = 706
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Centro Costo"
        Me.GridColumn11.FieldName = "CostCenterDescription"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 575
        '
        'INDValueCol
        '
        Me.INDValueCol.Caption = "Valor"
        Me.INDValueCol.ColumnEdit = Me.INDrepTxtValue
        Me.INDValueCol.FieldName = "Value"
        Me.INDValueCol.Name = "INDValueCol"
        Me.INDValueCol.Visible = True
        Me.INDValueCol.VisibleIndex = 2
        Me.INDValueCol.Width = 351
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.DisplayFormat.FormatString = "C0"
        Me.INDrepTxtValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDrepTxtValue.Mask.EditMask = "C0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDsleAccountPayable
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleAccountPayable, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleAccountPayable, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleAccountPayable, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleAccountPayable, False)
        Me.INDsleAccountPayable.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleAccountPayable, False)
        Me.INDsleAccountPayable.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountPayable, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountPayable.Name = "INDsleAccountPayable"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleAccountPayable, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleAccountPayable, False)
        Me.INDsleAccountPayable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAccountPayable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountPayable.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleAccountPayable.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountPayable.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountPayable.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAccountPayable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountPayable.Properties.DisplayMember = "IdAccountPayable.BillNumber"
        Me.INDsleAccountPayable.Properties.NullText = ""
        Me.INDsleAccountPayable.Properties.PopupSizeable = False
        Me.INDsleAccountPayable.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleAccountPayable.Properties.ShowFooter = False
        Me.INDsleAccountPayable.Properties.ValueMember = "IdAccountPayable.Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleAccountPayable, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleAccountPayable, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleAccountPayable, True)
        Me.INDsleAccountPayable.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAccountPayable.StyleController = Me.INDlyModifyAmortization
        Me.INDsleAccountPayable.TabIndex = 11
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleAccountPayable, "730")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountPayable, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleAccountPayable, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleAccountPayable, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Código"
        Me.GridColumn4.FieldName = "IdAccountPayable.Code"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "No. Factura"
        Me.GridColumn5.FieldName = "IdAccountPayable.BillNumber"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fecha Documento"
        Me.GridColumn6.FieldName = "IdAccountPayable.DocumentDate"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        '
        'INDsleSupplier
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleSupplier, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleSupplier, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleSupplier, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleSupplier, False)
        Me.INDsleSupplier.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleSupplier, False)
        Me.INDsleSupplier.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSupplier.Name = "INDsleSupplier"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleSupplier, False)
        Me.INDsleSupplier.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleSupplier.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDsleSupplier.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleSupplier.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleSupplier.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSupplier.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleSupplier.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSupplier.Properties.DisplayMember = "CodeName"
        Me.INDsleSupplier.Properties.NullText = ""
        Me.INDsleSupplier.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDsleSupplier.Properties.PopupSizeable = False
        Me.INDsleSupplier.Properties.PopupView = Me.viewSupplier
        Me.INDsleSupplier.Properties.ShowFooter = False
        Me.INDsleSupplier.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleSupplier, True)
        Me.INDsleSupplier.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSupplier.StyleController = Me.INDlyModifyAmortization
        Me.INDsleSupplier.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleSupplier, "558")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSupplier, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleSupplier, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleSupplier, False)
        '
        'viewSupplier
        '
        Me.viewSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.viewSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSupplier.Appearance.Row.Options.UseFont = True
        Me.viewSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn9})
        Me.viewSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSupplier.Name = "viewSupplier"
        Me.viewSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.viewSupplier.OptionsView.ShowAutoFilterRow = True
        Me.viewSupplier.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSupplier, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 280
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Descripcion"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 771
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Nit"
        Me.GridColumn9.FieldName = "IdThirdParty.Nit"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        Me.GridColumn9.Width = 581
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
        Me.LayoutControlGroup1.CustomizationFormText = "Modificación Amortización"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation, Me.INDlygAmortizations})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1286, 540)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.CustomizationFormText = "Información General"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemSupplier, Me.INDlyItemAccountPayable})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 520)
        Me.INDlygGeneralInformation.Text = "Información General"
        '
        'INDlyItemSupplier
        '
        Me.INDlyItemSupplier.Control = Me.INDsleSupplier
        Me.INDlyItemSupplier.CustomizationFormText = "Proveedor"
        Me.INDlyItemSupplier.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemSupplier.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemSupplier.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemSupplier.Name = "INDlyItemSupplier"
        Me.INDlyItemSupplier.ShowInCustomizationForm = False
        Me.INDlyItemSupplier.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSupplier.Text = "Proveedor"
        Me.INDlyItemSupplier.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSupplier.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSupplier.TextToControlDistance = 5
        '
        'INDlyItemAccountPayable
        '
        Me.INDlyItemAccountPayable.Control = Me.INDsleAccountPayable
        Me.INDlyItemAccountPayable.CustomizationFormText = "Cuenta por Pagar"
        Me.INDlyItemAccountPayable.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemAccountPayable.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountPayable.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAccountPayable.Name = "INDlyItemAccountPayable"
        Me.INDlyItemAccountPayable.ShowInCustomizationForm = False
        Me.INDlyItemAccountPayable.Size = New System.Drawing.Size(390, 403)
        Me.INDlyItemAccountPayable.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccountPayable.Text = "Cuenta por Pagar"
        Me.INDlyItemAccountPayable.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAccountPayable.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAccountPayable.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAccountPayable.TextToControlDistance = 5
        '
        'INDlygAmortizations
        '
        Me.INDlygAmortizations.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAmortizations.AppearanceGroup.Options.UseFont = True
        Me.INDlygAmortizations.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAmortizations.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAmortizations.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortizations.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAmortizations.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAmortizations.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAmortizations.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortizations.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAmortizations.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortizations.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAmortizations.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortizations.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAmortizations, False)
        Me.INDlygAmortizations.CustomizationFormText = "Amortizaciones"
        Me.INDlygAmortizations.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAmortizations})
        Me.INDlygAmortizations.Location = New System.Drawing.Point(414, 0)
        Me.INDlygAmortizations.Name = "INDlygAmortizations"
        Me.INDlygAmortizations.Size = New System.Drawing.Size(852, 520)
        Me.INDlygAmortizations.Text = "Amortizaciones"
        '
        'INDlyItemAmortizations
        '
        Me.INDlyItemAmortizations.Control = Me.INDgcAmortizations
        Me.INDlyItemAmortizations.CustomizationFormText = "Amortizaciones"
        Me.INDlyItemAmortizations.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAmortizations.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemAmortizations.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemAmortizations.Name = "INDlyItemAmortizations"
        Me.INDlyItemAmortizations.Size = New System.Drawing.Size(828, 467)
        Me.INDlyItemAmortizations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAmortizations.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAmortizations.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmModifyAmortization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmModifyAmortization"
        Me.Opacity = 1.0R
        Me.Tag = "735"
        Me.Text = "Modificación Amortizaciones"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyModifyAmortization, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyModifyAmortization.ResumeLayout(False)
        CType(Me.INDgcAmortizations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewAmortizations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountPayable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAmortizations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAmortizations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyModifyAmortization As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDsleAccountPayable As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemAccountPayable As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcAmortizations As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewAmortizations As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygAmortizations As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemAmortizations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDValueCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
