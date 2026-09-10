Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardRequestMixingStationDetail
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
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolAdministrationRoute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1081, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1081, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1081, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcDetail)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1127, 451, 650, 400)
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1077, 557)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDgcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetail, False)
        Me.INDgcDetail.Location = New System.Drawing.Point(24, 53)
        Me.INDgcDetail.MainView = Me.INDviewDetail
        Me.INDgcDetail.Name = "INDgcDetail"
        Me.INDgcDetail.Size = New System.Drawing.Size(1029, 480)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetail.TabIndex = 3
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDetail})
        '
        'INDviewDetail
        '
        Me.INDviewDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDetail.Appearance.Row.Options.UseFont = True
        Me.INDviewDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn2, Me.GridColumn6, Me.GridColumn7, Me.INDcolBed, Me.INDcolAdministrationRoute, Me.INDcolPatient, Me.GridColumn1})
        Me.INDviewDetail.GridControl = Me.INDgcDetail
        Me.INDviewDetail.GroupCount = 1
        Me.INDviewDetail.Name = "INDviewDetail"
        Me.INDviewDetail.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewDetail.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDviewDetail.OptionsSelection.MultiSelect = True
        Me.INDviewDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDetail.OptionsView.ShowDetailButtons = False
        Me.INDviewDetail.OptionsView.ShowGroupPanel = False
        Me.INDviewDetail.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDetail, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Unidad funcional"
        Me.GridColumn3.FieldName = "FunctionalUnitCodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 5
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo"
        Me.GridColumn2.FieldName = "ItemTypeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Item"
        Me.GridColumn6.FieldName = "ItemCodeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 161
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo dosis unitaria"
        Me.GridColumn7.FieldName = "UnitDoseTypeCodeName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 161
        '
        'INDcolBed
        '
        Me.INDcolBed.Caption = "Cama"
        Me.INDcolBed.FieldName = "Bed"
        Me.INDcolBed.Name = "INDcolBed"
        Me.INDcolBed.OptionsColumn.AllowEdit = False
        Me.INDcolBed.OptionsColumn.AllowFocus = False
        Me.INDcolBed.Width = 161
        '
        'INDcolAdministrationRoute
        '
        Me.INDcolAdministrationRoute.Caption = "Vía admon."
        Me.INDcolAdministrationRoute.FieldName = "AdministrationRouteCodeName"
        Me.INDcolAdministrationRoute.Name = "INDcolAdministrationRoute"
        Me.INDcolAdministrationRoute.OptionsColumn.AllowEdit = False
        Me.INDcolAdministrationRoute.OptionsColumn.AllowFocus = False
        '
        'INDcolPatient
        '
        Me.INDcolPatient.Caption = "Paciente"
        Me.INDcolPatient.FieldName = "PatientCodeName"
        Me.INDcolPatient.Name = "INDcolPatient"
        Me.INDcolPatient.OptionsColumn.AllowEdit = False
        Me.INDcolPatient.OptionsColumn.AllowFocus = False
        Me.INDcolPatient.Width = 235
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Cantidad"
        Me.GridColumn1.FieldName = "Quantity"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 4
        Me.GridColumn1.Width = 88
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1077, 557)
        Me.Root.TextVisible = False
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
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDetail})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(1057, 537)
        Me.INDlygPrincipalInformation.Text = "solicitud"
        '
        'INDlyItemDetail
        '
        Me.INDlyItemDetail.Control = Me.INDgcDetail
        Me.INDlyItemDetail.CustomizationFormText = "Rejilla Detalle"
        Me.INDlyItemDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDetail.MinSize = New System.Drawing.Size(209, 24)
        Me.INDlyItemDetail.Name = "INDlyItemDetail"
        Me.INDlyItemDetail.Size = New System.Drawing.Size(1033, 484)
        Me.INDlyItemDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDetail.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmDashboardRequestMixingStationDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1081, 701)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDashboardRequestMixingStationDetail"
        Me.Opacity = 1.0R
        Me.Tag = "2224"
        Me.Text = "Detalle"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolAdministrationRoute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
