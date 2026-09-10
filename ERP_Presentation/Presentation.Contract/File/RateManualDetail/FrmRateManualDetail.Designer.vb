Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRateManualDetail
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRateManualDetail = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAddServices = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcServices = New DevExpress.XtraGrid.GridControl()
        Me.viewGridRateManualDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtDiscount = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleRateManual = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSearchRateManual = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRateManual = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygService = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridServices = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddServices = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRateManualDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRateManualDetail.SuspendLayout()
        CType(Me.INDgcServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewGridRateManualDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleRateManual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSearchRateManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRateManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRateManualDetail)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1318, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1318, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1318, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyRateManualDetail
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyRateManualDetail
        '
        Me.INDlyRateManualDetail.AllowCustomization = False
        Me.INDlyRateManualDetail.Controls.Add(Me.INDbtnAddServices)
        Me.INDlyRateManualDetail.Controls.Add(Me.INDgcServices)
        Me.INDlyRateManualDetail.Controls.Add(Me.INDsleRateManual)
        Me.INDlyRateManualDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRateManualDetail, False)
        Me.INDlyRateManualDetail.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRateManualDetail.Name = "INDlyRateManualDetail"
        Me.INDlyRateManualDetail.Root = Me.LayoutControlGroup1
        Me.INDlyRateManualDetail.Size = New System.Drawing.Size(1114, 574)
        Me.INDlyRateManualDetail.TabIndex = 1
        Me.INDlyRateManualDetail.Text = "LayoutControl1"
        '
        'INDbtnAddServices
        '
        Me.INDbtnAddServices.Location = New System.Drawing.Point(438, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddServices, False)
        Me.INDbtnAddServices.Name = "INDbtnAddServices"
        Me.INDbtnAddServices.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddServices.StyleController = Me.INDlyRateManualDetail
        Me.INDbtnAddServices.TabIndex = 7
        Me.INDbtnAddServices.Text = "Servicios"
        '
        'INDgcServices
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcServices, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcServices, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcServices, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcServices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcServices, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcServices, False)
        Me.INDgcServices.Location = New System.Drawing.Point(438, 95)
        Me.INDgcServices.MainView = Me.viewGridRateManualDetail
        Me.INDgcServices.Name = "INDgcServices"
        Me.INDgcServices.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtDiscount})
        Me.INDgcServices.Size = New System.Drawing.Size(824, 438)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcServices, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcServices, New System.Drawing.Size(830, 0))
        Me.INDgcServices.TabIndex = 6
        Me.INDgcServices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewGridRateManualDetail})
        '
        'viewGridRateManualDetail
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.viewGridRateManualDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewGridRateManualDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewGridRateManualDetail.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewGridRateManualDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewGridRateManualDetail.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewGridRateManualDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewGridRateManualDetail.Appearance.Row.Options.UseFont = True
        Me.viewGridRateManualDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewGridRateManualDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.viewGridRateManualDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn8})
        Me.viewGridRateManualDetail.GridControl = Me.INDgcServices
        Me.viewGridRateManualDetail.Name = "viewGridRateManualDetail"
        Me.viewGridRateManualDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.viewGridRateManualDetail.OptionsView.EnableAppearanceOddRow = True
        Me.viewGridRateManualDetail.OptionsView.ShowAutoFilterRow = True
        Me.viewGridRateManualDetail.OptionsView.ShowDetailButtons = False
        Me.viewGridRateManualDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewGridRateManualDetail, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Servicio IPS"
        Me.GridColumn1.FieldName = "IPSServiceDescription"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 264
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "% Descuento"
        Me.GridColumn2.ColumnEdit = Me.INDrepTxtDiscount
        Me.GridColumn2.FieldName = "DiscountPercentage"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 116
        '
        'INDrepTxtDiscount
        '
        Me.INDrepTxtDiscount.AutoHeight = False
        Me.INDrepTxtDiscount.Mask.EditMask = "P"
        Me.INDrepTxtDiscount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtDiscount.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtDiscount.Name = "INDrepTxtDiscount"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "L. I. Ambulatorios"
        Me.GridColumn3.FieldName = "OutPatientRecoveryFeeTypeDescription"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 213
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "L. I. Hospitalarios"
        Me.GridColumn8.FieldName = "InPatientRecoveryFeeTypeDescription"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 218
        '
        'INDsleRateManual
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleRateManual, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleRateManual, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleRateManual, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleRateManual, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleRateManual, False)
        Me.INDsleRateManual.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleRateManual, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleRateManual.Name = "INDsleRateManual"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleRateManual, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleRateManual, False)
        Me.INDsleRateManual.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleRateManual.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleRateManual.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleRateManual.Properties.Appearance.Options.UseFont = True
        Me.INDsleRateManual.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleRateManual.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleRateManual.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleRateManual.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleRateManual.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleRateManual.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleRateManual.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleRateManual.Properties.DisplayMember = "CodeName"
        Me.INDsleRateManual.Properties.NullText = ""
        Me.INDsleRateManual.Properties.PopupSizeable = False
        Me.INDsleRateManual.Properties.ShowFooter = False
        Me.INDsleRateManual.Properties.ValueMember = "Id"
        Me.INDsleRateManual.Properties.View = Me.viewSearchRateManual
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleRateManual, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleRateManual, True)
        Me.INDsleRateManual.Size = New System.Drawing.Size(386, 28)
        Me.INDsleRateManual.StyleController = Me.INDlyRateManualDetail
        Me.INDsleRateManual.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleRateManual, "980")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleRateManual, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleRateManual, "{0} - {1}")
        Me.INDsleRateManual.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleRateManual, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleRateManual, False)
        '
        'viewSearchRateManual
        '
        Me.viewSearchRateManual.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewSearchRateManual.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewSearchRateManual.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewSearchRateManual.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewSearchRateManual.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSearchRateManual.Appearance.Row.Options.UseFont = True
        Me.viewSearchRateManual.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.viewSearchRateManual.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSearchRateManual.Name = "viewSearchRateManual"
        Me.viewSearchRateManual.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSearchRateManual.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchRateManual.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchRateManual.OptionsView.ShowAutoFilterRow = True
        Me.viewSearchRateManual.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSearchRateManual, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Código"
        Me.GridColumn4.FieldName = "Code"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 260
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nombre"
        Me.GridColumn5.FieldName = "Name"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 585
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tipo"
        Me.GridColumn6.FieldName = "TypeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 391
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Redondeo"
        Me.GridColumn7.FieldName = "RoundServiceName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 396
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation, Me.INDlygService})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1286, 557)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.CustomizationFormText = "LayoutControlGroup2"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemRateManual})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 537)
        Me.INDlygGeneralInformation.Text = "Información General"
        '
        'INDlyItemRateManual
        '
        Me.INDlyItemRateManual.Control = Me.INDsleRateManual
        Me.INDlyItemRateManual.CustomizationFormText = "Manual"
        Me.INDlyItemRateManual.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRateManual.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemRateManual.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemRateManual.Name = "INDlyItemRateManual"
        Me.INDlyItemRateManual.Size = New System.Drawing.Size(390, 478)
        Me.INDlyItemRateManual.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRateManual.Text = "Manual Tarifario"
        Me.INDlyItemRateManual.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRateManual.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRateManual.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRateManual.TextToControlDistance = 5
        '
        'INDlygService
        '
        Me.INDlygService.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygService.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygService.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygService.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygService.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygService.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygService.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygService.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygService.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygService.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygService.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygService.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygService.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygService.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygService, False)
        Me.INDlygService.CustomizationFormText = "Servicios"
        Me.INDlygService.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridServices, Me.INDlyItemAddServices})
        Me.INDlygService.Location = New System.Drawing.Point(414, 0)
        Me.INDlygService.Name = "INDlygService"
        Me.INDlygService.Size = New System.Drawing.Size(852, 537)
        Me.INDlygService.Text = "Servicios"
        '
        'INDlyItemGridServices
        '
        Me.INDlyItemGridServices.Control = Me.INDgcServices
        Me.INDlyItemGridServices.CustomizationFormText = "INDlyItemGridServicesFees"
        Me.INDlyItemGridServices.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemGridServices.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemGridServices.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemGridServices.Name = "INDlyItemGridServices"
        Me.INDlyItemGridServices.Size = New System.Drawing.Size(828, 442)
        Me.INDlyItemGridServices.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridServices.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridServices.TextVisible = False
        '
        'INDlyItemAddServices
        '
        Me.INDlyItemAddServices.Control = Me.INDbtnAddServices
        Me.INDlyItemAddServices.CustomizationFormText = "INDlyItemAddService"
        Me.INDlyItemAddServices.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddServices.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddServices.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddServices.Name = "INDlyItemAddServices"
        Me.INDlyItemAddServices.Size = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddServices.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddServices.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddServices.TextVisible = False
        '
        'IndigoGridView1
        '
        '
        'FrmRateManualDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1318, 701)
        Me.Name = "FrmRateManualDetail"
        Me.Opacity = 1.0R
        Me.Tag = "981"
        Me.Text = "Manual de Servicios"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRateManualDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRateManualDetail.ResumeLayout(False)
        CType(Me.INDgcServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewGridRateManualDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleRateManual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSearchRateManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRateManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyRateManualDetail As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleRateManual As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSearchRateManual As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemRateManual As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDgcServices As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewGridRateManualDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemGridServices As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygService As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnAddServices As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddServices As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtDiscount As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
