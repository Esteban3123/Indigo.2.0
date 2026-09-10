Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopUpAvailabilityExtension
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDsleAvailability = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcBasePopUp = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeDateExpiredEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDspnDaysExpired = New DevExpress.XtraEditors.SpinEdit()
        Me.INDdeDateExpiredInitial = New DevExpress.XtraEditors.DateEdit()
        Me.INDlciDateExpiredInitial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDaysExpired = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDateExpiredEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDsbAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAvailability.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcBasePopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcBasePopUp.SuspendLayout()
        CType(Me.INDdeDateExpiredEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateExpiredEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnDaysExpired.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateExpiredInitial.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateExpiredInitial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDateExpiredInitial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDaysExpired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDateExpiredEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcBasePopUp)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(880, 418)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(880, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(880, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcBasePopUp
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 409)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(676, 373)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciAvailability, Me.INDlciDateExpiredInitial, Me.INDlciDaysExpired, Me.INDlciDateExpiredEnd})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(656, 353)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlciAvailability
        '
        Me.INDlciAvailability.Control = Me.INDsleAvailability
        Me.INDlciAvailability.Location = New System.Drawing.Point(0, 0)
        Me.INDlciAvailability.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciAvailability.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciAvailability.Name = "INDlciAvailability"
        Me.INDlciAvailability.Size = New System.Drawing.Size(632, 64)
        Me.INDlciAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAvailability.Text = "Disponibilidad"
        Me.INDlciAvailability.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAvailability.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciAvailability.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciAvailability.TextToControlDistance = 5
        '
        'INDsleAvailability
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleAvailability, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleAvailability, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAvailability, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.Location = New System.Drawing.Point(24, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAvailability, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAvailability.Name = "INDsleAvailability"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleAvailability, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleAvailability.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAvailability.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAvailability.Properties.Appearance.Options.UseFont = True
        Me.INDsleAvailability.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAvailability.Properties.DisplayMember = "Code"
        Me.INDsleAvailability.Properties.NullText = ""
        Me.INDsleAvailability.Properties.PopupSizeable = False
        Me.INDsleAvailability.Properties.ShowFooter = False
        Me.INDsleAvailability.Properties.ValueMember = "Id"
        Me.INDsleAvailability.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleAvailability, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleAvailability, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleAvailability, True)
        Me.INDsleAvailability.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAvailability.StyleController = Me.INDlcBasePopUp
        Me.INDsleAvailability.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleAvailability, "228")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAvailability, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleAvailability, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleAvailability, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3, Me.GridColumn4, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit1View.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit1View.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit1View.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit1View.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
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
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fecha Documento"
        Me.GridColumn3.FieldName = "AvailabilityDate"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Fecha Vencimiento"
        Me.GridColumn4.FieldName = "ExpirationDate"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Estado"
        Me.GridColumn2.FieldName = "StatusName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'INDlcBasePopUp
        '
        Me.INDlcBasePopUp.Controls.Add(Me.INDdeDateExpiredEnd)
        Me.INDlcBasePopUp.Controls.Add(Me.INDspnDaysExpired)
        Me.INDlcBasePopUp.Controls.Add(Me.INDdeDateExpiredInitial)
        Me.INDlcBasePopUp.Controls.Add(Me.INDsleAvailability)
        Me.INDlcBasePopUp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcBasePopUp.Location = New System.Drawing.Point(202, 7)
        Me.INDlcBasePopUp.Name = "INDlcBasePopUp"
        Me.INDlcBasePopUp.Root = Me.LayoutControlGroup1
        Me.INDlcBasePopUp.Size = New System.Drawing.Size(676, 373)
        Me.INDlcBasePopUp.TabIndex = 1
        Me.INDlcBasePopUp.Text = "LayoutControl1"
        '
        'INDdeDateExpiredEnd
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDateExpiredEnd, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDateExpiredEnd, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDateExpiredEnd, False)
        Me.INDdeDateExpiredEnd.EditValue = Nothing
        Me.INDdeDateExpiredEnd.EnterMoveNextControl = True
        Me.INDdeDateExpiredEnd.Location = New System.Drawing.Point(24, 276)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDateExpiredEnd, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDateExpiredEnd, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDateExpiredEnd.Name = "INDdeDateExpiredEnd"
        Me.INDdeDateExpiredEnd.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDdeDateExpiredEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateExpiredEnd.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDateExpiredEnd.Properties.Appearance.Options.UseFont = True
        Me.INDdeDateExpiredEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDateExpiredEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDateExpiredEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateExpiredEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDateExpiredEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDateExpiredEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDateExpiredEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateExpiredEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateExpiredEnd.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeDateExpiredEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDateExpiredEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDateExpiredEnd.Properties.ReadOnly = True
        Me.INDdeDateExpiredEnd.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDateExpiredEnd.StyleController = Me.INDlcBasePopUp
        Me.INDdeDateExpiredEnd.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDateExpiredEnd, 0)
        '
        'INDspnDaysExpired
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnDaysExpired, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnDaysExpired, False)
        Me.INDspnDaysExpired.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnDaysExpired.EnterMoveNextControl = True
        Me.INDspnDaysExpired.Location = New System.Drawing.Point(24, 212)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnDaysExpired, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspnDaysExpired.Name = "INDspnDaysExpired"
        Me.INDspnDaysExpired.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspnDaysExpired.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnDaysExpired.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnDaysExpired.Properties.Appearance.Options.UseFont = True
        Me.INDspnDaysExpired.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnDaysExpired.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnDaysExpired.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnDaysExpired.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnDaysExpired.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnDaysExpired.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnDaysExpired.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnDaysExpired.Size = New System.Drawing.Size(386, 28)
        Me.INDspnDaysExpired.StyleController = Me.INDlcBasePopUp
        Me.INDspnDaysExpired.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnDaysExpired, 0)
        '
        'INDdeDateExpiredInitial
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDateExpiredInitial, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDateExpiredInitial, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDateExpiredInitial, False)
        Me.INDdeDateExpiredInitial.EditValue = Nothing
        Me.INDdeDateExpiredInitial.EnterMoveNextControl = True
        Me.INDdeDateExpiredInitial.Location = New System.Drawing.Point(24, 148)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDateExpiredInitial, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDateExpiredInitial, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDateExpiredInitial.Name = "INDdeDateExpiredInitial"
        Me.INDdeDateExpiredInitial.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDdeDateExpiredInitial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateExpiredInitial.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDateExpiredInitial.Properties.Appearance.Options.UseFont = True
        Me.INDdeDateExpiredInitial.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDateExpiredInitial.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDateExpiredInitial.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateExpiredInitial.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDateExpiredInitial.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDateExpiredInitial.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDateExpiredInitial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateExpiredInitial.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateExpiredInitial.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeDateExpiredInitial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDateExpiredInitial.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDateExpiredInitial.Properties.ReadOnly = True
        Me.INDdeDateExpiredInitial.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDateExpiredInitial.StyleController = Me.INDlcBasePopUp
        Me.INDdeDateExpiredInitial.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDateExpiredInitial, 0)
        '
        'INDlciDateExpiredInitial
        '
        Me.INDlciDateExpiredInitial.Control = Me.INDdeDateExpiredInitial
        Me.INDlciDateExpiredInitial.Location = New System.Drawing.Point(0, 64)
        Me.INDlciDateExpiredInitial.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDateExpiredInitial.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDateExpiredInitial.Name = "INDlciDateExpiredInitial"
        Me.INDlciDateExpiredInitial.Size = New System.Drawing.Size(632, 64)
        Me.INDlciDateExpiredInitial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDateExpiredInitial.Text = "Fecha de Vencimiento Actual"
        Me.INDlciDateExpiredInitial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDateExpiredInitial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDateExpiredInitial.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDateExpiredInitial.TextToControlDistance = 5
        '
        'INDlciDaysExpired
        '
        Me.INDlciDaysExpired.Control = Me.INDspnDaysExpired
        Me.INDlciDaysExpired.Location = New System.Drawing.Point(0, 128)
        Me.INDlciDaysExpired.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDaysExpired.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDaysExpired.Name = "INDlciDaysExpired"
        Me.INDlciDaysExpired.Size = New System.Drawing.Size(632, 64)
        Me.INDlciDaysExpired.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDaysExpired.Text = "Dias de Prorroga"
        Me.INDlciDaysExpired.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDaysExpired.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDaysExpired.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDaysExpired.TextToControlDistance = 5
        '
        'INDlciDateExpiredEnd
        '
        Me.INDlciDateExpiredEnd.Control = Me.INDdeDateExpiredEnd
        Me.INDlciDateExpiredEnd.Location = New System.Drawing.Point(0, 192)
        Me.INDlciDateExpiredEnd.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDateExpiredEnd.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDateExpiredEnd.Name = "INDlciDateExpiredEnd"
        Me.INDlciDateExpiredEnd.Size = New System.Drawing.Size(632, 102)
        Me.INDlciDateExpiredEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDateExpiredEnd.Text = "Fecha de Vencimiento Final"
        Me.INDlciDateExpiredEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDateExpiredEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDateExpiredEnd.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDateExpiredEnd.TextToControlDistance = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDsbAccept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 380)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(676, 36)
        Me.PanelControl1.TabIndex = 2
        '
        'INDsbAccept
        '
        Me.INDsbAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDsbAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAccept, False)
        Me.INDsbAccept.Name = "INDsbAccept"
        Me.INDsbAccept.Size = New System.Drawing.Size(672, 32)
        Me.INDsbAccept.TabIndex = 5
        Me.INDsbAccept.Text = "Agregar"
        '
        'FrmPopUpAvailabilityExtension
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 536)
        Me.Name = "FrmPopUpAvailabilityExtension"
        Me.Opacity = 1.0R
        Me.Text = "Disponibilidades"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAvailability.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcBasePopUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcBasePopUp.ResumeLayout(False)
        CType(Me.INDdeDateExpiredEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateExpiredEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnDaysExpired.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateExpiredInitial.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateExpiredInitial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDateExpiredInitial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDaysExpired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDateExpiredEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcBasePopUp As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDsleAvailability As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlciAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDdeDateExpiredInitial As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDateExpiredInitial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspnDaysExpired As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciDaysExpired As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeDateExpiredEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDateExpiredEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDsbAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
