Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLevelCategory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLevelCategory))
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyFinancialSource = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopupButtonAddLevel = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyAddLevel = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtLength = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsbAddLevel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtLevel = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrAddLevel = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLevel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLength = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcLevelCategory = New DevExpress.XtraGrid.GridControl()
        Me.INDgvLevelCategory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNivel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLength = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLycFundingSource = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLygFundingSource = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLevelsCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddLevelCat = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl2 = New Presentation.Controls.IndigoGridControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyFinancialSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyFinancialSource.SuspendLayout()
        CType(Me.INDPopupButtonAddLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.INDlyAddLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyAddLevel.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtLength.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrAddLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcLevelCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvLevelCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycFundingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygFundingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLevelsCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddLevelCat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyFinancialSource)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyFinancialSource
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyFinancialSource
        '
        Me.INDlyFinancialSource.Controls.Add(Me.INDPopupButtonAddLevel)
        Me.INDlyFinancialSource.Controls.Add(Me.INDgcLevelCategory)
        resources.ApplyResources(Me.INDlyFinancialSource, "INDlyFinancialSource")
        Me.INDlyFinancialSource.Name = "INDlyFinancialSource"
        Me.INDlyFinancialSource.Root = Me.INDLycFundingSource
        '
        'INDPopupButtonAddLevel
        '
        resources.ApplyResources(Me.INDPopupButtonAddLevel, "INDPopupButtonAddLevel")
        Me.INDPopupButtonAddLevel.MenuManager = Me.BarManager1
        Me.INDPopupButtonAddLevel.Name = "INDPopupButtonAddLevel"
        Me.INDPopupButtonAddLevel.Properties.Appearance.BackColor = CType(resources.GetObject("INDPopupButtonAddLevel.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDPopupButtonAddLevel.Properties.Appearance.Font = CType(resources.GetObject("INDPopupButtonAddLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupButtonAddLevel.Properties.Appearance.ForeColor = CType(resources.GetObject("INDPopupButtonAddLevel.Properties.Appearance.ForeColor"), System.Drawing.Color)
        Me.INDPopupButtonAddLevel.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopupButtonAddLevel.Properties.Appearance.Options.UseFont = True
        Me.INDPopupButtonAddLevel.Properties.Appearance.Options.UseForeColor = True
        Me.INDPopupButtonAddLevel.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopupButtonAddLevel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDPopupButtonAddLevel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPopupButtonAddLevel.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPopupButtonAddLevel.Properties.PopupControl = Me.PopupContainerControl1
        Me.INDPopupButtonAddLevel.StyleController = Me.INDlyFinancialSource
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 0
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.INDlyAddLevel)
        resources.ApplyResources(Me.PopupContainerControl1, "PopupContainerControl1")
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        '
        'INDlyAddLevel
        '
        Me.INDlyAddLevel.Controls.Add(Me.INDtxtName)
        Me.INDlyAddLevel.Controls.Add(Me.INDtxtLength)
        Me.INDlyAddLevel.Controls.Add(Me.INDsbAddLevel)
        Me.INDlyAddLevel.Controls.Add(Me.INDtxtLevel)
        resources.ApplyResources(Me.INDlyAddLevel, "INDlyAddLevel")
        Me.INDlyAddLevel.Name = "INDlyAddLevel"
        Me.INDlyAddLevel.Root = Me.LayoutControlGroup1
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.MenuManager = Me.BarManager1
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.StyleController = Me.INDlyAddLevel
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDtxtLength
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtLength, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtLength, False)
        resources.ApplyResources(Me.INDtxtLength, "INDtxtLength")
        Me.INDtxtLength.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtLength, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtLength.MenuManager = Me.BarManager1
        Me.INDtxtLength.Name = "INDtxtLength"
        Me.INDtxtLength.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtLength.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtLength.Properties.Appearance.Font = CType(resources.GetObject("INDtxtLength.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtLength.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLength.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLength.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtLength.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtLength.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtLength.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtLength.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtLength.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtLength.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtLength.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtLength.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLength.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDtxtLength.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDtxtLength.Properties.MaxLength = 1
        Me.INDtxtLength.StyleController = Me.INDlyAddLevel
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtLength, 0)
        '
        'INDsbAddLevel
        '
        resources.ApplyResources(Me.INDsbAddLevel, "INDsbAddLevel")
        Me.INDsbAddLevel.Name = "INDsbAddLevel"
        Me.INDsbAddLevel.StyleController = Me.INDlyAddLevel
        '
        'INDtxtLevel
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtLevel, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtLevel, False)
        resources.ApplyResources(Me.INDtxtLevel, "INDtxtLevel")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtLevel, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtLevel.MenuManager = Me.BarManager1
        Me.INDtxtLevel.Name = "INDtxtLevel"
        Me.INDtxtLevel.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtLevel.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtLevel.Properties.Appearance.Font = CType(resources.GetObject("INDtxtLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtLevel.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLevel.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLevel.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtLevel.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtLevel.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtLevel.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtLevel.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtLevel.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtLevel.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtLevel.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtLevel.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLevel.Properties.ReadOnly = True
        Me.INDtxtLevel.StyleController = Me.INDlyAddLevel
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtLevel, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrAddLevel})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(455, 228)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDGrAddLevel
        '
        Me.INDGrAddLevel.AppearanceGroup.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDGrAddLevel.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDGrAddLevel.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGrAddLevel.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrAddLevel.AppearanceItemCaption.Options.UseFont = True
        Me.INDGrAddLevel.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDGrAddLevel.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDGrAddLevel.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDGrAddLevel.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGrAddLevel.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDGrAddLevel.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDGrAddLevel.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDGrAddLevel.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGrAddLevel.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDGrAddLevel.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDGrAddLevel.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDGrAddLevel, False)
        resources.ApplyResources(Me.INDGrAddLevel, "INDGrAddLevel")
        Me.INDGrAddLevel.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemLevel, Me.LayoutControlItem1, Me.INDlyItemLength, Me.INDlyItemName})
        Me.INDGrAddLevel.Location = New System.Drawing.Point(0, 0)
        Me.INDGrAddLevel.Name = "INDGrAddLevel"
        Me.INDGrAddLevel.Size = New System.Drawing.Size(435, 208)
        '
        'INDlyItemLevel
        '
        Me.INDlyItemLevel.Control = Me.INDtxtLevel
        resources.ApplyResources(Me.INDlyItemLevel, "INDlyItemLevel")
        Me.INDlyItemLevel.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLevel.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemLevel.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemLevel.Name = "INDlyItemLevel"
        Me.INDlyItemLevel.Size = New System.Drawing.Size(411, 36)
        Me.INDlyItemLevel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLevel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLevel.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemLevel.TextToControlDistance = 12
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsbAddLevel
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 108)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(411, 41)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlyItemLength
        '
        Me.INDlyItemLength.Control = Me.INDtxtLength
        resources.ApplyResources(Me.INDlyItemLength, "INDlyItemLength")
        Me.INDlyItemLength.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemLength.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemLength.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemLength.Name = "INDlyItemLength"
        Me.INDlyItemLength.Size = New System.Drawing.Size(411, 36)
        Me.INDlyItemLength.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLength.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLength.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemLength.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(411, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDgcLevelCategory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLevelCategory, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcLevelCategory, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcLevelCategory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLevelCategory, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLevelCategory, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcLevelCategory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLevelCategory, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcLevelCategory, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcLevelCategory, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLevelCategory, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcLevelCategory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLevelCategory, False)
        resources.ApplyResources(Me.INDgcLevelCategory, "INDgcLevelCategory")
        Me.INDgcLevelCategory.MainView = Me.INDgvLevelCategory
        Me.INDgcLevelCategory.Name = "INDgcLevelCategory"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLevelCategory, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcLevelCategory, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl2.SetSizeLayoutItem(Me.INDgcLevelCategory, New System.Drawing.Size(709, 0))
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcLevelCategory, New System.Drawing.Size(709, 0))
        Me.INDgcLevelCategory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvLevelCategory})
        '
        'INDgvLevelCategory
        '
        'Cadena reemplazada... CType(resources.GetObject("INDgvLevelCategory.Appearance.FocusedRow.BackColor"), System.Drawing.Color)
        'Cadena reemplazada... CType(resources.GetObject("INDgvLevelCategory.Appearance.FocusedRow.BackColor2"), System.Drawing.Color)
        Me.INDgvLevelCategory.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvLevelCategory.Appearance.GroupRow.Font = CType(resources.GetObject("INDgvLevelCategory.Appearance.GroupRow.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDgvLevelCategory.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.INDgvLevelCategory.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvLevelCategory.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgvLevelCategory.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDgvLevelCategory.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.INDgvLevelCategory.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvLevelCategory.Appearance.Row.Font = CType(resources.GetObject("INDgvLevelCategory.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgvLevelCategory.Appearance.Row.Options.UseFont = True
        Me.INDgvLevelCategory.Appearance.ViewCaption.Font = CType(resources.GetObject("INDgvLevelCategory.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDgvLevelCategory.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvLevelCategory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNivel, Me.INDColName, Me.INDColLength})
        Me.INDgvLevelCategory.GridControl = Me.INDgcLevelCategory
        Me.INDgvLevelCategory.Name = "INDgvLevelCategory"
        Me.INDgvLevelCategory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvLevelCategory.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvLevelCategory.OptionsView.ShowAutoFilterRow = True
        Me.INDgvLevelCategory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvLevelCategory, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvLevelCategory, False)
        '
        'INDColNivel
        '
        resources.ApplyResources(Me.INDColNivel, "INDColNivel")
        Me.INDColNivel.FieldName = "Level"
        Me.INDColNivel.Name = "INDColNivel"
        '
        'INDColName
        '
        resources.ApplyResources(Me.INDColName, "INDColName")
        Me.INDColName.FieldName = "Name"
        Me.INDColName.Name = "INDColName"
        '
        'INDColLength
        '
        resources.ApplyResources(Me.INDColLength, "INDColLength")
        Me.INDColLength.FieldName = "Length"
        Me.INDColLength.Name = "INDColLength"
        '
        'INDLycFundingSource
        '
        Me.INDLycFundingSource.AppearanceGroup.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLycFundingSource.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDLycFundingSource.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLycFundingSource.AppearanceItemCaption.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceItemCaption.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDLycFundingSource.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLycFundingSource.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLycFundingSource.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLycFundingSource, False)
        resources.ApplyResources(Me.INDLycFundingSource, "INDLycFundingSource")
        Me.INDLycFundingSource.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLycFundingSource.GroupBordersVisible = False
        Me.INDLycFundingSource.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLygFundingSource})
        Me.INDLycFundingSource.Location = New System.Drawing.Point(0, 0)
        Me.INDLycFundingSource.Name = "INDLycFundingSource"
        Me.INDLycFundingSource.Size = New System.Drawing.Size(753, 431)
        Me.INDLycFundingSource.TextVisible = False
        '
        'INDLygFundingSource
        '
        Me.INDLygFundingSource.AppearanceGroup.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLygFundingSource.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDLygFundingSource.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLygFundingSource.AppearanceItemCaption.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDLygFundingSource.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLygFundingSource.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLygFundingSource.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygFundingSource, False)
        resources.ApplyResources(Me.INDLygFundingSource, "INDLygFundingSource")
        Me.INDLygFundingSource.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemLevelsCategory, Me.INDlyItemAddLevelCat})
        Me.INDLygFundingSource.Location = New System.Drawing.Point(0, 0)
        Me.INDLygFundingSource.Name = "INDLygFundingSource"
        Me.INDLygFundingSource.Size = New System.Drawing.Size(733, 411)
        '
        'INDlyItemLevelsCategory
        '
        Me.INDlyItemLevelsCategory.Control = Me.INDgcLevelCategory
        resources.ApplyResources(Me.INDlyItemLevelsCategory, "INDlyItemLevelsCategory")
        Me.INDlyItemLevelsCategory.Location = New System.Drawing.Point(0, 28)
        Me.INDlyItemLevelsCategory.MinSize = New System.Drawing.Size(274, 25)
        Me.INDlyItemLevelsCategory.Name = "INDlyItemLevelsCategory"
        Me.INDlyItemLevelsCategory.Size = New System.Drawing.Size(709, 324)
        Me.INDlyItemLevelsCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLevelsCategory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemLevelsCategory.TextVisible = False
        '
        'INDlyItemAddLevelCat
        '
        Me.INDlyItemAddLevelCat.Control = Me.INDPopupButtonAddLevel
        resources.ApplyResources(Me.INDlyItemAddLevelCat, "INDlyItemAddLevelCat")
        Me.INDlyItemAddLevelCat.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddLevelCat.MaxSize = New System.Drawing.Size(455, 28)
        Me.INDlyItemAddLevelCat.MinSize = New System.Drawing.Size(455, 28)
        Me.INDlyItemAddLevelCat.Name = "INDlyItemAddLevelCat"
        Me.INDlyItemAddLevelCat.Size = New System.Drawing.Size(709, 28)
        Me.INDlyItemAddLevelCat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddLevelCat.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddLevelCat.TextVisible = False
        '
        'IndigoGridView1
        '
        '
        'FrmLevelCategory
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PopupContainerControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmLevelCategory"
        Me.Opacity = 1.0R
        Me.Tag = "241"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.PopupContainerControl1, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyFinancialSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyFinancialSource.ResumeLayout(False)
        CType(Me.INDPopupButtonAddLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.INDlyAddLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyAddLevel.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtLength.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrAddLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcLevelCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvLevelCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycFundingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygFundingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLevelsCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddLevelCat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyFinancialSource As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLycFundingSource As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDLygFundingSource As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcLevelCategory As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl2 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDgvLevelCategory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView2 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlyItemLevelsCategory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColNivel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLength As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyAddLevel As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtLevel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrAddLevel As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemLevel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAddLevel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtLength As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemLength As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupButtonAddLevel As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyItemAddLevelCat As DevExpress.XtraLayout.LayoutControlItem
End Class
