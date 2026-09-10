Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRecalculateBalancesIncome
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRecalculateBalancesIncome = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDsleEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcPACControl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRecalculateBalancesIncome, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRecalculateBalancesIncome.SuspendLayout()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRecalculateBalancesIncome)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRecalculateBalancesIncome
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 598)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRecalculateBalancesIncome
        '
        Me.INDlcRecalculateBalancesIncome.Controls.Add(Me.INDsleValidity)
        Me.INDlcRecalculateBalancesIncome.Controls.Add(Me.INDsleEntity)
        Me.INDlcRecalculateBalancesIncome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRecalculateBalancesIncome.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRecalculateBalancesIncome.Name = "INDlcRecalculateBalancesIncome"
        Me.INDlcRecalculateBalancesIncome.Root = Me.LayoutControlGroup1
        Me.INDlcRecalculateBalancesIncome.Size = New System.Drawing.Size(804, 598)
        Me.INDlcRecalculateBalancesIncome.TabIndex = 1
        Me.INDlcRecalculateBalancesIncome.Text = "LayoutControl1"
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 148)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidity.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleValidity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleValidity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleValidity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleValidity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleValidity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleValidity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidity.Properties.DisplayMember = "Year"
        Me.INDsleValidity.Properties.NullText = ""
        Me.INDsleValidity.Properties.PopupSizeable = False
        Me.INDsleValidity.Properties.PopupView = Me.INDgvValidity
        Me.INDsleValidity.Properties.ShowFooter = False
        Me.INDsleValidity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleValidity.StyleController = Me.INDlcRecalculateBalancesIncome
        Me.INDsleValidity.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
        '
        'INDgvValidity
        '
        Me.INDgvValidity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValidity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvValidity.Appearance.Row.Options.UseFont = True
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.INDgcStatusValidity, Me.GridColumn5, Me.GridColumn6, Me.INDgcIncomeMonth, Me.INDgcPACControl})
        Me.INDgvValidity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvValidity.Name = "INDgvValidity"
        Me.INDgvValidity.OptionsCustomization.AllowGroup = False
        Me.INDgvValidity.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvValidity.OptionsDetail.ShowDetailTabs = False
        Me.INDgvValidity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvValidity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValidity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValidity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValidity.OptionsView.ShowDetailButtons = False
        Me.INDgvValidity.OptionsView.ShowGroupPanel = False
        '
        'INDsleEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntity, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Location = New System.Drawing.Point(24, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntity.Name = "INDsleEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleEntity.Properties.NullText = ""
        Me.INDsleEntity.Properties.PopupSizeable = False
        Me.INDsleEntity.Properties.PopupView = Me.GridView1
        Me.INDsleEntity.Properties.ShowFooter = False
        Me.INDsleEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntity, True)
        Me.INDsleEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleEntity.StyleController = Me.INDlcRecalculateBalancesIncome
        Me.INDsleEntity.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntity, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsDetail.ShowDetailTabs = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 598)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntity, Me.INDlciValidity})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(784, 578)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlciEntity
        '
        Me.INDlciEntity.Control = Me.INDsleEntity
        Me.INDlciEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciEntity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciEntity.Name = "INDlciEntity"
        Me.INDlciEntity.Size = New System.Drawing.Size(760, 64)
        Me.INDlciEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntity.Text = "Entidad Presupuestal"
        Me.INDlciEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntity.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciEntity.TextToControlDistance = 5
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(760, 455)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciValidity.TextToControlDistance = 5
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Año"
        Me.GridColumn3.FieldName = "Year"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'INDgcStatusValidity
        '
        Me.INDgcStatusValidity.Caption = "Estado"
        Me.INDgcStatusValidity.FieldName = "StatusText"
        Me.INDgcStatusValidity.Name = "INDgcStatusValidity"
        Me.INDgcStatusValidity.Visible = True
        Me.INDgcStatusValidity.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Resolución"
        Me.GridColumn5.FieldName = "ResolutionNumber"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Valor"
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "ResolutionValue"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        '
        'INDgcIncomeMonth
        '
        Me.INDgcIncomeMonth.Caption = "Mes Ingreso"
        Me.INDgcIncomeMonth.FieldName = "IncomeMonth"
        Me.INDgcIncomeMonth.Name = "INDgcIncomeMonth"
        '
        'INDgcPACControl
        '
        Me.INDgcPACControl.Caption = "P A C"
        Me.INDgcPACControl.FieldName = "PACControl"
        Me.INDgcPACControl.Name = "INDgcPACControl"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'FrmRecalculateBalancesIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmRecalculateBalancesIncome"
        Me.Opacity = 1.0R
        Me.Tag = "2204"
        Me.Text = "Cierre Mensual"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRecalculateBalancesIncome, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRecalculateBalancesIncome.ResumeLayout(False)
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcRecalculateBalancesIncome As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcPACControl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
End Class
