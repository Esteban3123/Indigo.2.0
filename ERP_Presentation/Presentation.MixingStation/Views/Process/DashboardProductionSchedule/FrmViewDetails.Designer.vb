<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmViewDetails
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDgcDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemProgress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.INDlyProgress = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDpanelProgressbar = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyProgress.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelProgressbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelProgressbar.SuspendLayout()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDgcDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetails, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetails, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetails, False)
        Me.INDgcDetails.Location = New System.Drawing.Point(24, 53)
        Me.INDgcDetails.MainView = Me.INDviewDetails
        Me.INDgcDetails.Name = "INDgcDetails"
        Me.INDgcDetails.Size = New System.Drawing.Size(946, 576)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetails, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetails.TabIndex = 4
        Me.INDgcDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDetails})
        '
        'INDviewDetails
        '
        Me.INDviewDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDetails.Appearance.Row.Options.UseFont = True
        Me.INDviewDetails.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDetails.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3, Me.GridColumn2, Me.GridColumn5, Me.GridColumn4, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn6, Me.GridColumn12, Me.GridColumn11})
        Me.INDviewDetails.GridControl = Me.INDgcDetails
        Me.INDviewDetails.Name = "INDviewDetails"
        Me.INDviewDetails.OptionsSelection.MultiSelect = True
        Me.INDviewDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDetails.OptionsView.ShowDetailButtons = False
        Me.INDviewDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDetails, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Solicitud"
        Me.GridColumn1.FieldName = "RequestCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 56
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tipo Solicitud"
        Me.GridColumn3.FieldName = "RequestTypeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 96
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha Solicitud"
        Me.GridColumn2.FieldName = "RequestDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 93
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Item"
        Me.GridColumn5.FieldName = "ItemCodeName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 279
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cant. Solicitada"
        Me.GridColumn4.FieldName = "RequestQuantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.ToolTip = "Cantidad solicitada"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 105
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "PP"
        Me.GridColumn7.FieldName = "StatusPP"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.FixedWidth = True
        Me.GridColumn7.ToolTip = "Producto en producción"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        Me.GridColumn7.Width = 35
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "PT"
        Me.GridColumn8.FieldName = "StatusPT"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.FixedWidth = True
        Me.GridColumn8.ToolTip = "Producto terminado"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 6
        Me.GridColumn8.Width = 35
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "PL"
        Me.GridColumn9.FieldName = "StatusPL"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.OptionsColumn.FixedWidth = True
        Me.GridColumn9.ToolTip = "Producto liberado"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 7
        Me.GridColumn9.Width = 35
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "PPR"
        Me.GridColumn10.FieldName = "StatusPPR"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.FixedWidth = True
        Me.GridColumn10.ToolTip = "Producto reproceso"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 8
        Me.GridColumn10.Width = 35
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "PR"
        Me.GridColumn6.FieldName = "StatusPR"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.FixedWidth = True
        Me.GridColumn6.ToolTip = "Producto rechazado"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 9
        Me.GridColumn6.Width = 35
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "PA"
        Me.GridColumn12.FieldName = "StatusPA"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.OptionsColumn.FixedWidth = True
        Me.GridColumn12.ToolTip = "Producto anulado"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 10
        Me.GridColumn12.Width = 35
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Tipo"
        Me.GridColumn11.FieldName = "ItemTypeName"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 11
        Me.GridColumn11.Width = 82
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemProgress})
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.Root.Size = New System.Drawing.Size(990, 18)
        Me.Root.TextVisible = False
        '
        'INDlyItemProgress
        '
        Me.INDlyItemProgress.Control = Me.MarqueeProgressBarControl1
        Me.INDlyItemProgress.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemProgress.MaxSize = New System.Drawing.Size(0, 10)
        Me.INDlyItemProgress.MinSize = New System.Drawing.Size(1, 10)
        Me.INDlyItemProgress.Name = "INDlyItemProgress"
        Me.INDlyItemProgress.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlyItemProgress.Size = New System.Drawing.Size(990, 18)
        Me.INDlyItemProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemProgress.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemProgress.TextVisible = False
        Me.INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(990, 10)
        Me.MarqueeProgressBarControl1.StyleController = Me.INDlyProgress
        Me.MarqueeProgressBarControl1.TabIndex = 4
        '
        'INDlyProgress
        '
        Me.INDlyProgress.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.INDlyProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyProgress.Location = New System.Drawing.Point(2, 2)
        Me.INDlyProgress.Name = "INDlyProgress"
        Me.INDlyProgress.Root = Me.Root
        Me.INDlyProgress.Size = New System.Drawing.Size(990, 18)
        Me.INDlyProgress.TabIndex = 0
        Me.INDlyProgress.Text = "LayoutControl1"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygDetails})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(994, 653)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygDetails
        '
        Me.INDlygDetails.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceGroup.Options.UseFont = True
        Me.INDlygDetails.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDetails, False)
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDetails})
        Me.INDlygDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(974, 633)
        Me.INDlygDetails.Text = "Detalles"
        '
        'INDlyItemDetails
        '
        Me.INDlyItemDetails.Control = Me.INDgcDetails
        Me.INDlyItemDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDetails.Name = "INDlyItemDetails"
        Me.INDlyItemDetails.Size = New System.Drawing.Size(950, 580)
        Me.INDlyItemDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDetails.TextVisible = False
        '
        'INDpanelProgressbar
        '
        Me.INDpanelProgressbar.Controls.Add(Me.INDlyProgress)
        Me.INDpanelProgressbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpanelProgressbar.Location = New System.Drawing.Point(0, 0)
        Me.INDpanelProgressbar.Name = "INDpanelProgressbar"
        Me.INDpanelProgressbar.Size = New System.Drawing.Size(994, 22)
        Me.INDpanelProgressbar.TabIndex = 0
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcDetails)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(0, 22)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(994, 653)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'FrmViewDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 675)
        Me.Controls.Add(Me.INDlyRoot)
        Me.Controls.Add(Me.INDpanelProgressbar)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmViewDetails"
        Me.Text = "Campaña # 0"
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyProgress.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelProgressbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelProgressbar.ResumeLayout(False)
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents INDpanelProgressbar As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyProgress As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemProgress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
