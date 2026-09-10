<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostProductionCenter
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleCostProductionCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCostProductionCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCostProductionCenterCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCostProductionCenterName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCostProductionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDsleCostProductionCenter.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvCostProductionCenter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciCostProductionCenter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Controls.Add(Me.INDsleCostProductionCenter)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(411, 120)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsbAdd
        '
        Me.INDsbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDsbAdd.Appearance.Options.UseFont = true
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 74)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDsleCostProductionCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCostProductionCenter, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCostProductionCenter, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.INDsleCostProductionCenter.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.INDsleCostProductionCenter.Location = New System.Drawing.Point(12, 38)
        Me.INDsleCostProductionCenter.Name = "INDsleCostProductionCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.INDsleCostProductionCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCostProductionCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleCostProductionCenter.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleCostProductionCenter.Properties.Appearance.Options.UseFont = true
        Me.INDsleCostProductionCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCostProductionCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleCostProductionCenter.Properties.NullText = ""
        Me.INDsleCostProductionCenter.Properties.PopupSizeable = false
        Me.INDsleCostProductionCenter.Properties.ShowFooter = false
        Me.INDsleCostProductionCenter.Properties.ValueMember = "Id"
        Me.INDsleCostProductionCenter.Properties.View = Me.INDgvCostProductionCenter
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCostProductionCenter, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCostProductionCenter, true)
        Me.INDsleCostProductionCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCostProductionCenter.StyleController = Me.INDlcRoot
        Me.INDsleCostProductionCenter.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCostProductionCenter, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCostProductionCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCostProductionCenter, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCostProductionCenter, false)
        '
        'INDgvCostProductionCenter
        '
        Me.INDgvCostProductionCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvCostProductionCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvCostProductionCenter.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvCostProductionCenter.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvCostProductionCenter.Appearance.FocusedRow.Options.UseForeColor = true
        Me.INDgvCostProductionCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvCostProductionCenter.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvCostProductionCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvCostProductionCenter.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvCostProductionCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCostProductionCenter.Appearance.Row.Options.UseFont = true
        Me.INDgvCostProductionCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolCostProductionCenterCode, Me.INDcolCostProductionCenterName})
        Me.INDgvCostProductionCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvCostProductionCenter.Name = "INDgvCostProductionCenter"
        Me.INDgvCostProductionCenter.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDgvCostProductionCenter.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvCostProductionCenter.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvCostProductionCenter.OptionsView.ShowAutoFilterRow = true
        Me.INDgvCostProductionCenter.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvCostProductionCenter, false)
        '
        'INDcolCostProductionCenterCode
        '
        Me.INDcolCostProductionCenterCode.Caption = "Código"
        Me.INDcolCostProductionCenterCode.FieldName = "Code"
        Me.INDcolCostProductionCenterCode.Name = "INDcolCostProductionCenterCode"
        Me.INDcolCostProductionCenterCode.OptionsColumn.AllowEdit = false
        Me.INDcolCostProductionCenterCode.OptionsColumn.AllowFocus = false
        Me.INDcolCostProductionCenterCode.Visible = true
        Me.INDcolCostProductionCenterCode.VisibleIndex = 0
        Me.INDcolCostProductionCenterCode.Width = 218
        '
        'INDcolCostProductionCenterName
        '
        Me.INDcolCostProductionCenterName.Caption = "Nombre"
        Me.INDcolCostProductionCenterName.FieldName = "Name"
        Me.INDcolCostProductionCenterName.Name = "INDcolCostProductionCenterName"
        Me.INDcolCostProductionCenterName.OptionsColumn.AllowEdit = false
        Me.INDcolCostProductionCenterName.OptionsColumn.AllowFocus = false
        Me.INDcolCostProductionCenterName.Visible = true
        Me.INDcolCostProductionCenterName.VisibleIndex = 1
        Me.INDcolCostProductionCenterName.Width = 338
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = true
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, false)
        Me.INDlcgRoot.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = false
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCostProductionCenter, Me.INDlciAdd})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(411, 120)
        Me.INDlcgRoot.TextVisible = false
        '
        'INDlciCostProductionCenter
        '
        Me.INDlciCostProductionCenter.AllowHide = false
        Me.INDlciCostProductionCenter.Control = Me.INDsleCostProductionCenter
        Me.INDlciCostProductionCenter.CustomizationFormText = "Centro de Producción"
        Me.INDlciCostProductionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCostProductionCenter.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostProductionCenter.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciCostProductionCenter.Name = "INDlciCostProductionCenter"
        Me.INDlciCostProductionCenter.ShowInCustomizationForm = false
        Me.INDlciCostProductionCenter.Size = New System.Drawing.Size(391, 62)
        Me.INDlciCostProductionCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCostProductionCenter.Text = "Centro de Producción"
        Me.INDlciCostProductionCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCostProductionCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCostProductionCenter.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlciCostProductionCenter.TextToControlDistance = 5
        '
        'INDlciAdd
        '
        Me.INDlciAdd.Control = Me.INDsbAdd
        Me.INDlciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciAdd.Location = New System.Drawing.Point(0, 62)
        Me.INDlciAdd.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.Name = "INDlciAdd"
        Me.INDlciAdd.Size = New System.Drawing.Size(391, 38)
        Me.INDlciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAdd.TextToControlDistance = 0
        Me.INDlciAdd.TextVisible = false
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'CtrCostProductionCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlcRoot)
        Me.Name = "CtrCostProductionCenter"
        Me.Size = New System.Drawing.Size(411, 120)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(false)
        CType(Me.INDsleCostProductionCenter.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvCostProductionCenter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciCostProductionCenter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDsleCostProductionCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCostProductionCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostProductionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolCostProductionCenterCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCostProductionCenterName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlciAdd As DevExpress.XtraLayout.LayoutControlItem

End Class
