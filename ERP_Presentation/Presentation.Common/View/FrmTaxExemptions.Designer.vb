Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTaxExemptions
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyEconomicActivity = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleApplicationType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtInternalCode = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciApplicationType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInternalCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyEconomicActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyEconomicActivity.SuspendLayout()
        CType(Me.INDSleApplicationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtInternalCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciApplicationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInternalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyEconomicActivity)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 140)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(6)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1608, 773)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 10)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(6)
        Me.ToolBars.Size = New System.Drawing.Size(1608, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(9)
        Me.BarraBotones.Size = New System.Drawing.Size(1608, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyEconomicActivity
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 12)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 759)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyEconomicActivity
        '
        Me.INDlyEconomicActivity.AllowCustomization = False
        Me.INDlyEconomicActivity.Controls.Add(Me.INDSleApplicationType)
        Me.INDlyEconomicActivity.Controls.Add(Me.INDbtnCode)
        Me.INDlyEconomicActivity.Controls.Add(Me.INDtxtDescription)
        Me.INDlyEconomicActivity.Controls.Add(Me.INDTxtInternalCode)
        Me.INDlyEconomicActivity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyEconomicActivity, False)
        Me.INDlyEconomicActivity.Location = New System.Drawing.Point(202, 12)
        Me.INDlyEconomicActivity.Margin = New System.Windows.Forms.Padding(4)
        Me.INDlyEconomicActivity.Name = "INDlyEconomicActivity"
        Me.INDlyEconomicActivity.Root = Me.LayoutControlGroup1
        Me.INDlyEconomicActivity.Size = New System.Drawing.Size(1404, 759)
        Me.INDlyEconomicActivity.TabIndex = 1
        Me.INDlyEconomicActivity.Text = "LayoutControl1"
        '
        'INDSleApplicationType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleApplicationType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleApplicationType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleApplicationType, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleApplicationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleApplicationType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleApplicationType, False)
        Me.INDSleApplicationType.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleApplicationType, False)
        Me.INDSleApplicationType.Location = New System.Drawing.Point(24, 382)
        Me.INDSleApplicationType.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleApplicationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleApplicationType.MaximumSize = New System.Drawing.Size(585, 88)
        Me.INDSleApplicationType.MinimumSize = New System.Drawing.Size(585, 41)
        Me.INDSleApplicationType.Name = "INDSleApplicationType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleApplicationType, False)
        Me.INDSleApplicationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleApplicationType.Properties.Appearance.Options.UseFont = True
        Me.INDSleApplicationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleApplicationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleApplicationType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleApplicationType.Properties.DisplayMember = "Item2"
        Me.INDSleApplicationType.Properties.NullText = ""
        Me.INDSleApplicationType.Properties.PopupSizeable = False
        Me.INDSleApplicationType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleApplicationType.Properties.ShowFooter = False
        Me.INDSleApplicationType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleApplicationType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleApplicationType, True)
        Me.INDSleApplicationType.Size = New System.Drawing.Size(585, 41)
        Me.INDSleApplicationType.StyleController = Me.INDlyEconomicActivity
        Me.INDSleApplicationType.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleApplicationType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleApplicationType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleApplicationType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleApplicationType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleApplicationType, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDescription})
        Me.SearchLookUpEdit1View.DetailHeight = 512
        Me.SearchLookUpEdit1View.FixedLineWidth = 3
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Selección"
        Me.INDColDescription.FieldName = "Item2"
        Me.INDColDescription.MinWidth = 30
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.OptionsColumn.AllowEdit = False
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 0
        Me.INDColDescription.Width = 112
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 104)
        Me.INDbtnCode.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.MaximumSize = New System.Drawing.Size(585, 0)
        Me.INDbtnCode.MinimumSize = New System.Drawing.Size(585, 0)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Common.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(585, 38)
        Me.INDbtnCode.StyleController = Me.INDlyEconomicActivity
        Me.INDbtnCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDescription, False)
        Me.INDtxtDescription.Location = New System.Drawing.Point(24, 192)
        Me.INDtxtDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDescription.MaximumSize = New System.Drawing.Size(585, 150)
        Me.INDtxtDescription.MinimumSize = New System.Drawing.Size(585, 150)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDescription.Properties.MaxLength = 200
        Me.INDtxtDescription.Size = New System.Drawing.Size(585, 150)
        Me.INDtxtDescription.StyleController = Me.INDlyEconomicActivity
        Me.INDtxtDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDescription, 0)
        '
        'INDTxtInternalCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtInternalCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtInternalCode, False)
        Me.INDTxtInternalCode.EditValue = ""
        Me.INDTxtInternalCode.Location = New System.Drawing.Point(24, 470)
        Me.INDTxtInternalCode.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtInternalCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtInternalCode.MaximumSize = New System.Drawing.Size(585, 88)
        Me.INDTxtInternalCode.MinimumSize = New System.Drawing.Size(585, 41)
        Me.INDTxtInternalCode.Name = "INDTxtInternalCode"
        Me.INDTxtInternalCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTxtInternalCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtInternalCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInternalCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtInternalCode.Properties.MaxLength = 20
        Me.INDTxtInternalCode.Size = New System.Drawing.Size(585, 41)
        Me.INDTxtInternalCode.StyleController = Me.INDlyEconomicActivity
        Me.INDTxtInternalCode.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtInternalCode, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "Actividad Económica"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1404, 759)
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
        Me.INDlygGeneralInformation.CustomizationFormText = "Información Principal"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDescription, Me.INDLciApplicationType, Me.INDLciInternalCode})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(1384, 739)
        Me.INDlygGeneralInformation.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(591, 88)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(591, 88)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(1360, 88)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(202, 31)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.AllowHide = False
        Me.INDlyItemDescription.Control = Me.INDtxtDescription
        Me.INDlyItemDescription.CustomizationFormText = "Descripcion"
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 88)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.ShowInCustomizationForm = False
        Me.INDlyItemDescription.Size = New System.Drawing.Size(1360, 190)
        Me.INDlyItemDescription.Text = "Descripcion"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(202, 31)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'INDLciApplicationType
        '
        Me.INDLciApplicationType.Control = Me.INDSleApplicationType
        Me.INDLciApplicationType.CustomizationFormText = "Tipo de aplicacion"
        Me.INDLciApplicationType.Location = New System.Drawing.Point(0, 278)
        Me.INDLciApplicationType.MaxSize = New System.Drawing.Size(591, 88)
        Me.INDLciApplicationType.MinSize = New System.Drawing.Size(591, 88)
        Me.INDLciApplicationType.Name = "INDLciApplicationType"
        Me.INDLciApplicationType.ShowInCustomizationForm = False
        Me.INDLciApplicationType.Size = New System.Drawing.Size(1360, 88)
        Me.INDLciApplicationType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciApplicationType.Text = "Tipo de aplicacion"
        Me.INDLciApplicationType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciApplicationType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciApplicationType.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciApplicationType.TextToControlDistance = 5
        '
        'INDLciInternalCode
        '
        Me.INDLciInternalCode.Control = Me.INDTxtInternalCode
        Me.INDLciInternalCode.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciInternalCode.CustomizationFormText = "Actividad propia generadora de ingreso"
        Me.INDLciInternalCode.Location = New System.Drawing.Point(0, 366)
        Me.INDLciInternalCode.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLciInternalCode.Name = "INDLciInternalCode"
        Me.INDLciInternalCode.ShowInCustomizationForm = False
        Me.INDLciInternalCode.Size = New System.Drawing.Size(1360, 305)
        Me.INDLciInternalCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInternalCode.Text = "Codigo interno"
        Me.INDLciInternalCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInternalCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciInternalCode.TextSize = New System.Drawing.Size(202, 31)
        Me.INDLciInternalCode.TextToControlDistance = 5
        '
        'FrmTaxExemptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1608, 913)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmTaxExemptions"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Tag = "1845"
        Me.Text = "Actividad Económica"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyEconomicActivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyEconomicActivity.ResumeLayout(False)
        CType(Me.INDSleApplicationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtInternalCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciApplicationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInternalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyEconomicActivity As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciApplicationType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDSleApplicationType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciInternalCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtInternalCode As DevExpress.XtraEditors.TextEdit
End Class
