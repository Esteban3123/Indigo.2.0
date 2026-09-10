Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrLastCost
    Inherits XtraUserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpceLastCost = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcLastCost = New DevExpress.XtraGrid.GridControl()
        Me.INDgvLastCost = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LastCostValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEditValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.Abbreviaton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliLastCostDatasource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLcLastCostValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblLastCostTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDlcgLastCost = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliLastCostTitle = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceLastCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDgcLastCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvLastCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEditValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliLastCostDatasource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDlcgLastCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliLastCostTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDpceLastCost
        '
        Me.INDpceLastCost.CausesValidation = False
        Me.INDpceLastCost.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceLastCost.EditValue = ""
        Me.INDpceLastCost.Location = New System.Drawing.Point(8, 24)
        Me.INDpceLastCost.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpceLastCost.MaximumSize = New System.Drawing.Size(430, 50)
        Me.INDpceLastCost.MinimumSize = New System.Drawing.Size(430, 50)
        Me.INDpceLastCost.Name = "INDpceLastCost"
        Me.INDpceLastCost.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceLastCost.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDpceLastCost.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceLastCost.Properties.Appearance.Options.UseFont = True
        Me.INDpceLastCost.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceLastCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDpceLastCost.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDpceLastCost.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDpceLastCost.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceLastCost.Properties.Mask.IgnoreMaskBlank = False
        Me.INDpceLastCost.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDpceLastCost.Properties.PopupControl = Me.PopupContainerControl1
        Me.INDpceLastCost.Properties.PopupSizeable = False
        Me.INDpceLastCost.Properties.ShowPopupCloseButton = False
        Me.INDpceLastCost.Properties.ShowPopupShadow = False
        Me.INDpceLastCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.INDpceLastCost.Size = New System.Drawing.Size(430, 50)
        Me.INDpceLastCost.StyleController = Me.LayoutControl1
        Me.INDpceLastCost.TabIndex = 1
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.LayoutControl2)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(0, 174)
        Me.PopupContainerControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.PopupContainerControl1.MaximumSize = New System.Drawing.Size(600, 402)
        Me.PopupContainerControl1.MinimumSize = New System.Drawing.Size(600, 402)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(600, 402)
        Me.PopupContainerControl1.TabIndex = 6
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDgcLastCost)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup1
        Me.LayoutControl2.Size = New System.Drawing.Size(600, 402)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDgcLastCost
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLastCost, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLastCost, Nothing)
        Me.INDgcLastCost.EmbeddedNavigator.Enabled = False
        Me.INDgcLastCost.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLastCost, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLastCost, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLastCost, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLastCost, False)
        Me.INDgcLastCost.Location = New System.Drawing.Point(12, 12)
        Me.INDgcLastCost.MainView = Me.INDgvLastCost
        Me.INDgcLastCost.Margin = New System.Windows.Forms.Padding(4)
        Me.INDgcLastCost.Name = "INDgcLastCost"
        Me.INDgcLastCost.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEditValue})
        Me.INDgcLastCost.Size = New System.Drawing.Size(576, 378)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLastCost, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcLastCost.TabIndex = 0
        Me.INDgcLastCost.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvLastCost, Me.GridView1})
        '
        'INDgvLastCost
        '
        Me.INDgvLastCost.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvLastCost.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvLastCost.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvLastCost.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvLastCost.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvLastCost.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvLastCost.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvLastCost.Appearance.GroupPanel.Options.UseForeColor = True
        Me.INDgvLastCost.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLastCost.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvLastCost.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLastCost.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvLastCost.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvLastCost.Appearance.Row.Options.UseFont = True
        Me.INDgvLastCost.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvLastCost.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvLastCost.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvLastCost.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgvLastCost.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.LastCostValue, Me.Abbreviaton})
        Me.INDgvLastCost.DetailHeight = 512
        Me.INDgvLastCost.FixedLineWidth = 3
        Me.INDgvLastCost.GridControl = Me.INDgcLastCost
        Me.INDgvLastCost.Name = "INDgvLastCost"
        Me.INDgvLastCost.OptionsBehavior.Editable = False
        Me.INDgvLastCost.OptionsFind.AllowFindPanel = False
        Me.INDgvLastCost.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvLastCost.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvLastCost.OptionsView.RowAutoHeight = True
        Me.INDgvLastCost.OptionsView.ShowAutoFilterRow = True
        Me.INDgvLastCost.OptionsView.ShowDetailButtons = False
        Me.INDgvLastCost.OptionsView.ShowFooter = True
        Me.INDgvLastCost.OptionsView.ShowGroupPanel = False
        Me.INDgvLastCost.OptionsView.ShowViewCaption = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvLastCost, False)
        Me.INDgvLastCost.ViewCaption = "Últimos costos"
        '
        'LastCostValue
        '
        Me.LastCostValue.Caption = "Último Costo"
        Me.LastCostValue.ColumnEdit = Me.RepositoryItemTextEditValue
        Me.LastCostValue.FieldName = "Item1"
        Me.LastCostValue.MinWidth = 30
        Me.LastCostValue.Name = "LastCostValue"
        Me.LastCostValue.Visible = True
        Me.LastCostValue.VisibleIndex = 0
        Me.LastCostValue.Width = 112
        '
        'RepositoryItemTextEditValue
        '
        Me.RepositoryItemTextEditValue.AutoHeight = False
        Me.RepositoryItemTextEditValue.Mask.EditMask = "n2"
        Me.RepositoryItemTextEditValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEditValue.Mask.UseMaskAsDisplayFormat = True
        Me.RepositoryItemTextEditValue.Name = "RepositoryItemTextEditValue"
        '
        'Abbreviaton
        '
        Me.Abbreviaton.Caption = "Moneda"
        Me.Abbreviaton.FieldName = "Item2.Abbreviation"
        Me.Abbreviaton.MinWidth = 30
        Me.Abbreviaton.Name = "Abbreviaton"
        Me.Abbreviaton.Visible = True
        Me.Abbreviaton.VisibleIndex = 1
        Me.Abbreviaton.Width = 112
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 512
        Me.GridView1.FixedLineWidth = 3
        Me.GridView1.GridControl = Me.INDgcLastCost
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliLastCostDatasource})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(600, 402)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliLastCostDatasource
        '
        Me.INDliLastCostDatasource.Control = Me.INDgcLastCost
        Me.INDliLastCostDatasource.CustomizationFormText = "LayoutControlItem1"
        Me.INDliLastCostDatasource.Location = New System.Drawing.Point(0, 0)
        Me.INDliLastCostDatasource.Name = "INDliLastCostDatasource"
        Me.INDliLastCostDatasource.Size = New System.Drawing.Size(580, 382)
        Me.INDliLastCostDatasource.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliLastCostDatasource.TextVisible = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDpceLastCost)
        Me.LayoutControl1.Controls.Add(Me.INDLcLastCostValue)
        Me.LayoutControl1.Controls.Add(Me.INDlblLastCostTitle)
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 7)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0, 15, 0, 0)
        Me.LayoutControl1.MaximumSize = New System.Drawing.Size(450, 100)
        Me.LayoutControl1.MinimumSize = New System.Drawing.Size(450, 100)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(339, 5, 650, 400)
        Me.LayoutControl1.Root = Me.INDlcgLastCost
        Me.LayoutControl1.Size = New System.Drawing.Size(450, 100)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLcLastCostValue
        '
        Me.INDLcLastCostValue.Location = New System.Drawing.Point(8, 74)
        Me.INDLcLastCostValue.Margin = New System.Windows.Forms.Padding(4, 9, 4, 4)
        Me.INDLcLastCostValue.Name = "INDLcLastCostValue"
        Me.INDLcLastCostValue.Size = New System.Drawing.Size(440, 19)
        Me.INDLcLastCostValue.StyleController = Me.LayoutControl1
        Me.INDLcLastCostValue.TabIndex = 7
        '
        'INDlblLastCostTitle
        '
        Me.INDlblLastCostTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlblLastCostTitle.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblLastCostTitle.Appearance.Options.UseFont = True
        Me.INDlblLastCostTitle.Appearance.Options.UseForeColor = True
        Me.INDlblLastCostTitle.Appearance.Options.UseTextOptions = True
        Me.INDlblLastCostTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblLastCostTitle.Location = New System.Drawing.Point(7, 1)
        Me.INDlblLastCostTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.INDlblLastCostTitle.MaximumSize = New System.Drawing.Size(432, 0)
        Me.INDlblLastCostTitle.MinimumSize = New System.Drawing.Size(432, 0)
        Me.INDlblLastCostTitle.Name = "INDlblLastCostTitle"
        Me.INDlblLastCostTitle.Size = New System.Drawing.Size(432, 20)
        Me.INDlblLastCostTitle.StyleController = Me.LayoutControl1
        Me.INDlblLastCostTitle.TabIndex = 4
        Me.INDlblLastCostTitle.Text = "Último Costo"
        '
        'INDlcgLastCost
        '
        Me.INDlcgLastCost.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgLastCost.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgLastCost.GroupBordersVisible = False
        Me.INDlcgLastCost.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliLastCostTitle, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDlcgLastCost.Name = "INDlcgLastCost"
        Me.INDlcgLastCost.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 0, 0, 0)
        Me.INDlcgLastCost.Size = New System.Drawing.Size(450, 100)
        Me.INDlcgLastCost.TextVisible = False
        '
        'INDliLastCostTitle
        '
        Me.INDliLastCostTitle.Control = Me.INDlblLastCostTitle
        Me.INDliLastCostTitle.CustomizationFormText = "LayoutControlItem1"
        Me.INDliLastCostTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDliLastCostTitle.MaxSize = New System.Drawing.Size(411, 22)
        Me.INDliLastCostTitle.MinSize = New System.Drawing.Size(411, 22)
        Me.INDliLastCostTitle.Name = "INDliLastCostTitle"
        Me.INDliLastCostTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1)
        Me.INDliLastCostTitle.Size = New System.Drawing.Size(444, 22)
        Me.INDliLastCostTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliLastCostTitle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliLastCostTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliLastCostTitle.TextToControlDistance = 0
        Me.INDliLastCostTitle.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDLcLastCostValue
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 23)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(4, 23)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(444, 28)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDpceLastCost
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 22)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(430, 50)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(430, 50)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(444, 50)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'CtrLastCost
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(438, 100)
        Me.MinimumSize = New System.Drawing.Size(438, 100)
        Me.Name = "CtrLastCost"
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Size = New System.Drawing.Size(438, 100)
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceLastCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDgcLastCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvLastCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEditValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliLastCostDatasource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDlcgLastCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliLastCostTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpceLastCost As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDlblLastCostTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcLastCost As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvLastCost As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliLastCostDatasource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgLastCost As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliLastCostTitle As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEditValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDLcLastCostValue As LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Abbreviaton As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LastCostValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
