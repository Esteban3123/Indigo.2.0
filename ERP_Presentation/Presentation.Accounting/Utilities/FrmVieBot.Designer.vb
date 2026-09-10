Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVieBot
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyVieBot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpopupLegalBook = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyPopupLegalBook = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcLegalBook = New DevExpress.XtraGrid.GridControl()
        Me.INDviewLegalBook = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckAllow = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepHiperLink = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLegalBook = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcForms = New DevExpress.XtraGrid.GridControl()
        Me.INDViewForms = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepPceLegalBook = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemForms = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyVieBot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyVieBot.SuspendLayout()
        CType(Me.INDpopupLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupLegalBook.SuspendLayout()
        CType(Me.INDlyPopupLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPopupLegalBook.SuspendLayout()
        CType(Me.INDgcLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckAllow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepHiperLink, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcForms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDViewForms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepPceLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemForms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyVieBot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1086, 589)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1086, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1086, 98)
        '
        'INDlyVieBot
        '
        Me.INDlyVieBot.Controls.Add(Me.INDpopupLegalBook)
        Me.INDlyVieBot.Controls.Add(Me.INDgcForms)
        Me.INDlyVieBot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyVieBot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyVieBot.Name = "INDlyVieBot"
        Me.INDlyVieBot.Root = Me.LayoutControlGroup1
        Me.INDlyVieBot.Size = New System.Drawing.Size(882, 580)
        Me.INDlyVieBot.TabIndex = 1
        Me.INDlyVieBot.Text = "LayoutControl1"
        '
        'INDpopupLegalBook
        '
        Me.INDpopupLegalBook.Controls.Add(Me.INDlyPopupLegalBook)
        Me.INDpopupLegalBook.Location = New System.Drawing.Point(162, 186)
        Me.INDpopupLegalBook.Name = "INDpopupLegalBook"
        Me.INDpopupLegalBook.Size = New System.Drawing.Size(419, 191)
        Me.INDpopupLegalBook.TabIndex = 5
        '
        'INDlyPopupLegalBook
        '
        Me.INDlyPopupLegalBook.Controls.Add(Me.INDgcLegalBook)
        Me.INDlyPopupLegalBook.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPopupLegalBook.Location = New System.Drawing.Point(0, 0)
        Me.INDlyPopupLegalBook.Name = "INDlyPopupLegalBook"
        Me.INDlyPopupLegalBook.Root = Me.LayoutControlGroup2
        Me.INDlyPopupLegalBook.Size = New System.Drawing.Size(419, 191)
        Me.INDlyPopupLegalBook.TabIndex = 0
        Me.INDlyPopupLegalBook.Text = "LayoutControl1"
        '
        'INDgcLegalBook
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLegalBook, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLegalBook, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLegalBook, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLegalBook, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLegalBook, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLegalBook, False)
        Me.INDgcLegalBook.Location = New System.Drawing.Point(12, 12)
        Me.INDgcLegalBook.MainView = Me.INDviewLegalBook
        Me.INDgcLegalBook.Name = "INDgcLegalBook"
        Me.INDgcLegalBook.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckAllow, Me.INDrepHiperLink})
        Me.INDgcLegalBook.Size = New System.Drawing.Size(395, 167)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLegalBook, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcLegalBook.TabIndex = 4
        Me.INDgcLegalBook.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewLegalBook})
        '
        'INDviewLegalBook
        '
        Me.INDviewLegalBook.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewLegalBook.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewLegalBook.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewLegalBook.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewLegalBook.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewLegalBook.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewLegalBook.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewLegalBook.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewLegalBook.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewLegalBook.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewLegalBook.Appearance.Row.Options.UseFont = True
        Me.INDviewLegalBook.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewLegalBook.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewLegalBook.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewLegalBook.GridControl = Me.INDgcLegalBook
        Me.INDviewLegalBook.Name = "INDviewLegalBook"
        Me.INDviewLegalBook.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewLegalBook.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewLegalBook.OptionsView.ShowAutoFilterRow = True
        Me.INDviewLegalBook.OptionsView.ShowDetailButtons = False
        Me.INDviewLegalBook.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewLegalBook, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Libro Oficial"
        Me.GridColumn4.FieldName = "CodeNameLegalBook"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 201
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Permite"
        Me.GridColumn5.ColumnEdit = Me.INDrepCheckAllow
        Me.GridColumn5.FieldName = "Allow"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 68
        '
        'INDrepCheckAllow
        '
        Me.INDrepCheckAllow.AutoHeight = False
        Me.INDrepCheckAllow.Name = "INDrepCheckAllow"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "Reglas Cont."
        Me.GridColumn6.ColumnEdit = Me.INDrepHiperLink
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 108
        '
        'INDrepHiperLink
        '
        Me.INDrepHiperLink.AutoHeight = False
        Me.INDrepHiperLink.Caption = "Reglas"
        Me.INDrepHiperLink.Name = "INDrepHiperLink"
        Me.INDrepHiperLink.NullText = "Reglas"
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemLegalBook})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(419, 191)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemLegalBook
        '
        Me.INDlyItemLegalBook.Control = Me.INDgcLegalBook
        Me.INDlyItemLegalBook.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLegalBook.MinSize = New System.Drawing.Size(104, 24)
        Me.INDlyItemLegalBook.Name = "INDlyItemLegalBook"
        Me.INDlyItemLegalBook.Size = New System.Drawing.Size(399, 171)
        Me.INDlyItemLegalBook.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLegalBook.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemLegalBook.TextVisible = False
        '
        'INDgcForms
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcForms, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcForms, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcForms, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcForms, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcForms, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcForms, False)
        Me.INDgcForms.Location = New System.Drawing.Point(24, 59)
        Me.INDgcForms.MainView = Me.INDViewForms
        Me.INDgcForms.Name = "INDgcForms"
        Me.INDgcForms.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepPceLegalBook})
        Me.INDgcForms.Size = New System.Drawing.Size(824, 497)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcForms, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcForms.TabIndex = 4
        Me.INDgcForms.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDViewForms})
        '
        'INDViewForms
        '
        Me.INDViewForms.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDViewForms.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDViewForms.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDViewForms.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDViewForms.Appearance.FocusedRow.Options.UseFont = True
        Me.INDViewForms.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDViewForms.Appearance.GroupRow.Options.UseFont = True
        Me.INDViewForms.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDViewForms.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDViewForms.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDViewForms.Appearance.Row.Options.UseFont = True
        Me.INDViewForms.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDViewForms.Appearance.ViewCaption.Options.UseFont = True
        Me.INDViewForms.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.INDViewForms.GridControl = Me.INDgcForms
        Me.INDViewForms.GroupCount = 1
        Me.INDViewForms.Name = "INDViewForms"
        Me.INDViewForms.OptionsView.EnableAppearanceEvenRow = True
        Me.INDViewForms.OptionsView.EnableAppearanceOddRow = True
        Me.INDViewForms.OptionsView.ShowAutoFilterRow = True
        Me.INDViewForms.OptionsView.ShowDetailButtons = False
        Me.INDViewForms.OptionsView.ShowGroupPanel = False
        Me.INDViewForms.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDViewForms, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Módulo"
        Me.GridColumn1.FieldName = "ModuleName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre Form"
        Me.GridColumn2.FieldName = "Description"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Libros"
        Me.GridColumn3.ColumnEdit = Me.INDrepPceLegalBook
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'INDrepPceLegalBook
        '
        Me.INDrepPceLegalBook.AutoHeight = False
        SerializableAppearanceObject1.Options.UseTextOptions = True
        SerializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepPceLegalBook.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Accounting.My.Resources.Resources.Add_16x16_blue, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDrepPceLegalBook.Name = "INDrepPceLegalBook"
        Me.INDrepPceLegalBook.PopupControl = Me.INDpopupLegalBook
        Me.INDrepPceLegalBook.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(882, 580)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPrincipalInformation
        '
        Me.INDlygPrincipalInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalInformation, False)
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemForms})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(862, 560)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDlyItemForms
        '
        Me.INDlyItemForms.Control = Me.INDgcForms
        Me.INDlyItemForms.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemForms.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemForms.MinSize = New System.Drawing.Size(828, 1)
        Me.INDlyItemForms.Name = "INDlyItemForms"
        Me.INDlyItemForms.Size = New System.Drawing.Size(838, 501)
        Me.INDlyItemForms.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemForms.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemForms.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemForms.TextToControlDistance = 0
        Me.INDlyItemForms.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyVieBot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 580)
        Me.CtrNavigationControlPanel1.TabIndex = 2
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmVieBot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1086, 711)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVieBot"
        Me.Opacity = 1.0R
        Me.Tag = "1689"
        Me.Text = "Configuración VieBot"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyVieBot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyVieBot.ResumeLayout(False)
        CType(Me.INDpopupLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupLegalBook.ResumeLayout(False)
        CType(Me.INDlyPopupLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPopupLegalBook.ResumeLayout(False)
        CType(Me.INDgcLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckAllow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepHiperLink, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcForms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDViewForms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepPceLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemForms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyVieBot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDgcForms As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDViewForms As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemForms As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDrepPceLegalBook As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDpopupLegalBook As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyPopupLegalBook As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcLegalBook As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewLegalBook As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemLegalBook As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckAllow As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepHiperLink As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
End Class
