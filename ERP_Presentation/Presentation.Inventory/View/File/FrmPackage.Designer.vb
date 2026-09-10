Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPackage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPackage))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.PopupContainerEdit1 = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgProductList = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PopupContainerEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgProductList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1294, 540)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1294, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1294, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 531)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PopupContainerEdit1)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.INDmeDetail)
        Me.LayoutControl1.Controls.Add(Me.INDtxtName)
        Me.LayoutControl1.Controls.Add(Me.INDbteCode)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1090, 531)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PopupContainerEdit1
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.PopupContainerEdit1, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.PopupContainerEdit1, Nothing)
        Me.PopupContainerEdit1.Location = New System.Drawing.Point(438, 59)
        Me.PopupContainerEdit1.MinimumSize = New System.Drawing.Size(628, 32)
        Me.PopupContainerEdit1.Name = "PopupContainerEdit1"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.PopupContainerEdit1, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.PopupContainerEdit1, False)
        Me.PopupContainerEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseFont = True
        Me.PopupContainerEdit1.Properties.AutoHeight = False
        Me.PopupContainerEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupContainerEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("PopupContainerEdit1.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.PopupContainerEdit1.Properties.PopupSizeable = False
        Me.PopupContainerEdit1.Properties.ShowPopupCloseButton = False
        Me.PopupContainerEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.PopupContainerEdit1.Size = New System.Drawing.Size(628, 32)
        Me.PopupContainerEdit1.StyleController = Me.LayoutControl1
        Me.PopupContainerEdit1.TabIndex = 9
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.PopupContainerEdit1, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.PopupContainerEdit1, Nothing)
        '
        'GridControl1
        '
        Me.IndigoGridControl1.SetAddActions(Me.GridControl1, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.GridControl1, Nothing)
        Me.GridControl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridControl1.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.GridControl1, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GridControl1, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GridControl1, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GridControl1, False)
        Me.GridControl1.Location = New System.Drawing.Point(438, 95)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(628, 412)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GridControl1, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.GridControl1, New System.Drawing.Size(632, 0))
        Me.GridControl1.TabIndex = 8
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Item"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 58
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 72
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Producto"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 280
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Lote/Serial"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 112
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Cantidad"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 88
        '
        'INDmeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDetail, False)
        Me.INDmeDetail.Location = New System.Drawing.Point(164, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDetail.Name = "INDmeDetail"
        Me.INDmeDetail.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDmeDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDetail.Properties.Appearance.Options.UseFont = True
        Me.INDmeDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDetail.Size = New System.Drawing.Size(246, 146)
        Me.INDmeDetail.StyleController = Me.LayoutControl1
        Me.INDmeDetail.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDetail, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.Location = New System.Drawing.Point(164, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Size = New System.Drawing.Size(246, 28)
        Me.INDtxtName.StyleController = Me.LayoutControl1
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, False)
        Me.INDbteCode.Location = New System.Drawing.Point(164, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDbteCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.INDbteCode.Size = New System.Drawing.Size(246, 28)
        Me.INDbteCode.StyleController = Me.LayoutControl1
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgProductList})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1090, 531)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliName, Me.INDliDetail})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 511)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDliCode
        '
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "LayoutControlItem1"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.Size = New System.Drawing.Size(390, 36)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDliName
        '
        Me.INDliName.Control = Me.INDtxtName
        Me.INDliName.CustomizationFormText = "LayoutControlItem2"
        Me.INDliName.Location = New System.Drawing.Point(0, 36)
        Me.INDliName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliName.Name = "INDliName"
        Me.INDliName.Size = New System.Drawing.Size(390, 36)
        Me.INDliName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliName.Text = "Nombre"
        Me.INDliName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliName.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliName.TextToControlDistance = 5
        '
        'INDliDetail
        '
        Me.INDliDetail.AppearanceItemCaption.Options.UseTextOptions = True
        Me.INDliDetail.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDliDetail.Control = Me.INDmeDetail
        Me.INDliDetail.CustomizationFormText = "LayoutControlItem4"
        Me.INDliDetail.Location = New System.Drawing.Point(0, 72)
        Me.INDliDetail.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDliDetail.MinSize = New System.Drawing.Size(390, 150)
        Me.INDliDetail.Name = "INDliDetail"
        Me.INDliDetail.Size = New System.Drawing.Size(390, 380)
        Me.INDliDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDetail.Text = "Detalle"
        Me.INDliDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDetail.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliDetail.TextToControlDistance = 5
        '
        'INDlcgProductList
        '
        Me.INDlcgProductList.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgProductList.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgProductList.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProductList.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgProductList.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductList.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgProductList.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgProductList.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgProductList.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductList.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgProductList.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgProductList.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgProductList.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductList.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgProductList, False)
        Me.INDlcgProductList.CustomizationFormText = "Listado de Productos"
        Me.INDlcgProductList.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.INDliAddProduct})
        Me.INDlcgProductList.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgProductList.Name = "INDlcgProductList"
        Me.INDlcgProductList.Size = New System.Drawing.Size(656, 511)
        Me.INDlcgProductList.Text = "Listado de Productos"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GridControl1
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(632, 416)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDliAddProduct
        '
        Me.INDliAddProduct.Control = Me.PopupContainerEdit1
        Me.INDliAddProduct.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAddProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDliAddProduct.Name = "INDliAddProduct"
        Me.INDliAddProduct.Size = New System.Drawing.Size(632, 36)
        Me.INDliAddProduct.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddProduct.TextVisible = False
        '
        'FrmPackage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1294, 658)
        Me.Name = "FrmPackage"
        Me.Opacity = 1.0R
        Me.Tag = "308"
        Me.Text = "Paquete"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PopupContainerEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgProductList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDmeDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgProductList As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents PopupContainerEdit1 As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDliAddProduct As DevExpress.XtraLayout.LayoutControlItem
End Class
